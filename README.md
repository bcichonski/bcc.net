# Bcc.net

Bcc.net is a compiler for a simple, C-like language, for the .NET platform, that was born out of void.
You can declare variables, arrays, loops and interact a litte with the input and output streams.

[Full grammar](/bcc.net/grammar.txt)

## Installation

1. Clone
2. Find where do you have ilasm and peverify tools
3. Modify bcc.bat accordingly
4. Build

## Usage

```
PS > cd somewhere inside /bin where binaries are
PS > .\bcc.bat ..\..\..\Samples\hello.bc
```

Should output
```
*********************************** COMPILATION ***********************************
*********************************** ASSEMBLY **************************************

Microsoft (R) .NET Framework IL Assembler.  Version 4.8.4084.0
Copyright (c) Microsoft Corporation.  All rights reserved.
Assembling '\workspace\bcc.net\Samples\hello.il'  to EXE --> '\workspace\bcc.net\Samples\hello.exe'
Source file is ANSI

Assembled global method main
Creating PE file

Emitting classes:

Emitting fields and methods:
Global  Methods: 1;

Emitting events and properties:
Global
Writing PE file
Operation completed successfully
*********************************** VERIFICATION ***********************************

Microsoft (R) .NET Framework PE Verifier.  Version  4.0.30319.0
Copyright (c) Microsoft Corporation.  All rights reserved.

All Classes and Methods in \workspace\bcc.net\Samples\hello.exe Verified.
*********************************** EXECUTION *************************************

Hello World!
```

Try other files from [Samples](/Samples) folder.

## Contributing
Pull requests would be such a suprise that I may forgot what to do with them.

## License
[MIT](https://choosealicense.com/licenses/mit/)