# FilEncoder

## Descripción
Pequeña herramienta para transformar la codificación de un fichero al que se seleccione, se utilizan las funciones de .net para realizar esta acción por lo que es compatible con todas las [siguientes]("https://msdn.microsoft.com/en-us/library/windows/desktop/dd317756(v=vs.85).aspx") fuentes de codificación.

## Requisitos
Programado para ser compatible con Microsoft Windows a partir de 2003/XP, utiliza las librerías de .NET Framework 3.5.

## Uso
Cambiar la codificación de un fichero de entrada (codificación del sistema) a utf-8 (sin BOM):
```
filencoder.exe --in ASC2ESNOP.txt --out UTF2ESNOP.txt --enc utf-8
```

Cambiar la codificación a EBCDIC de un fichero y sobreescribirlo:
```
filencoder.exe --in ASC2ESNOP.txt --enc IBM500 --self
```

Uso de FilEncode:
```
> filencoder.exe --help
Use:
filencoder.exe --[anal|help] -in inputfile -[out|-self] -enc encoding [-v]
    --in -i                     Specify input file.
    --out -o                    Specify output file.
    --enc -e                    Specify the encoding output.
    --self -s                   Overwrite the input file with the new encoding.
    --anal -a                   Analyze the encoding of the input file (empiric, not available yet).

    --help -h                   Prints this text.
    --about                     About this program.
```
Todas las opciones que acepta el parámetro `--enc` son las listadas en la columna `.NET` Name del siguiente listado de [Microsoft]("https://msdn.microsoft.com/en-us/library/windows/desktop/dd317756(v=vs.85).aspx"), en el caso de UTF-8 se ha realizado para que genere el fichero sin la marca (BOM).
