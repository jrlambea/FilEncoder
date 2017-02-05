using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FilEncoder {
    class Program {
        static void PrintHelp() {
            string HelpText = "Use: \n" +
                "filencoder.exe --[anal|help] -in inputfile -[out|-self] -enc encoding [-v]\n" +
                "    --in -i     \t\tSpecify input file.\n" +
                "    --out -o    \t\tSpecify output file.\n" +
                "    --enc -e    \t\tSpecify the encoding output.\n" +
                "    --self -s   \t\tOverwrite the input file with the new encoding.\n" +
                "    --anal -a   \t\tAnalyze the encoding of the input file (empiric, not available yet).\n\n" +
                "    --help -h   \t\tPrints this text.\n" +
                "    --about     \t\tAbout this program.\n";
            Console.WriteLine(HelpText);
        }

        static void Main(string[] args) {
            if (args.Length == 0) {
                PrintHelp();
                Environment.Exit(1);
            }

            string InputFile = null;
            string OutputFile = null;
            bool self = false;
            Encoding CodePage = null;

            try {
                for (int i = 0; i < args.Length; i++) {
                    switch (args[i]) {
                        case "--help":
                        case "-h":
                            PrintHelp();
                            break;
                        case "--in":
                        case "-i":
                            InputFile = args[++i];
                            Console.WriteLine("[i] File " + InputFile + " loaded.");
                            break;
                        case "--out":
                        case "-o":
                            if (self) {
                                Console.WriteLine("[!] --out parameter is incompatible with --self.\n");
                                PrintHelp();
                                Environment.Exit(8);
                            }
                            if (File.Exists(args[++i])) {
                                Console.WriteLine("[!] The destination file already exist!");
                                Environment.Exit(16);
                            }
                            OutputFile = args[i];
                            Console.WriteLine("[i] File " + OutputFile + " opened for write.");
                            break;
                        case "--enc":
                        case "-e":
                            CodePage = Encoding.GetEncoding(args[++i]);
                            Console.WriteLine("[i] Output encoding fixed to " + CodePage.EncodingName + ".");
                            break;
                        case "--anal":
                        case "-a":
                            Console.WriteLine("[i] Analysis not implemented yet.");
                            break;
                        case "--self":
                        case "-s":
                            if (!(OutputFile == null)) {
                                Console.WriteLine("[!] --out parameter is incompatible with --self.");
                                PrintHelp();
                                Environment.Exit(8);
                            }
                            self = true;
                            OutputFile = Environment.GetEnvironmentVariable("TEMP") + "\\" + Guid.NewGuid().GetHashCode() + ".tmp";
                            Console.WriteLine("[i] The input file will be overwritten.");
                            Console.WriteLine("[i] Using the temp file " + OutputFile + ".");
                            break;
                        case "--about":
                            Console.WriteLine("FilEncoder has been written by @jr_lambea.\n\n" +
                                "This tool is open source and the code can be reviewed in my github repository: https://github.com/jrlambea/FilEncoder.\n" +
                                "For any suggestion, bug or question send me an email at jr_lambea [at] yahoo.com, thank you for using this software! :D");
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("[!] Unknown parameter " + args[i] + ".\n");
                            PrintHelp();
                            Environment.Exit(4);
                            break;
                    }
                }
            } catch (Exception Ex) {
                Console.WriteLine("[!] Error parsing arguments.\n" + Ex.Message + "\n");
                PrintHelp();
                Environment.Exit(255);
            }

            if (InputFile != null && OutputFile != null && CodePage != null) {
                Console.WriteLine("[i] Encoding...");
                // Workaround for set the output code to UTF-8 w/o BOM in case of utf-8 is selected
                if (CodePage.BodyName == "utf-8") { CodePage = new UTF8Encoding(false); }
                File.WriteAllText(OutputFile, File.ReadAllText(InputFile), CodePage);
            }
            else {
                Console.WriteLine("[!] Some parameter is missing...");
                PrintHelp();
                Environment.Exit(1);
            }

            if (self) {
                Console.WriteLine("[i] Replacing original...");
                File.Replace(OutputFile, InputFile, null);
            }

            Console.WriteLine("Done.");

            Environment.Exit(0);
        }
    }
}
