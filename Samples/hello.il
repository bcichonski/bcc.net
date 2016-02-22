.assembly hello { }                                
.module hello.exe                                
.method static int32 main() cil managed                                
{                                       
        .entrypoint                     
        ldc.i4.s 72                     //constant 'H'
        .locals init (char)             //Char H
        stloc 0                         //H=
        ldc.i4.s 101                    //constant 'e'
        .locals init (char)             //Char e
        stloc 1                         //e=
        ldc.i4.s 108                    //constant 'l'
        .locals init (char)             //Char l
        stloc 2                         //l=
        ldc.i4.s 111                    //constant 'o'
        .locals init (char)             //Char o
        stloc 3                         //o=
        ldc.i4.s 32                     //constant ' '
        .locals init (char)             //Char spc
        stloc 4                         //spc=
        ldc.i4.s 87                     //constant 'W'
        .locals init (char)             //Char W
        stloc 5                         //W=
        ldc.i4.s 114                    //constant 'r'
        .locals init (char)             //Char r
        stloc 6                         //r=
        ldc.i4.s 100                    //constant 'd'
        .locals init (char)             //Char d
        stloc 7                         //d=
        ldc.i4.s 33                     //constant '!'
        .locals init (char)             //Char ex
        stloc 8                         //ex=
        ldloc 0                         //variable value H
        call void [mscorlib]System.Console::Write(char)
        ldloc 1                         //variable value e
        call void [mscorlib]System.Console::Write(char)
        ldloc 2                         //variable value l
        call void [mscorlib]System.Console::Write(char)
        ldloc 2                         //variable value l
        call void [mscorlib]System.Console::Write(char)
        ldloc 3                         //variable value o
        call void [mscorlib]System.Console::Write(char)
        ldloc 4                         //variable value spc
        call void [mscorlib]System.Console::Write(char)
        ldloc 5                         //variable value W
        call void [mscorlib]System.Console::Write(char)
        ldloc 3                         //variable value o
        call void [mscorlib]System.Console::Write(char)
        ldloc 6                         //variable value r
        call void [mscorlib]System.Console::Write(char)
        ldloc 2                         //variable value l
        call void [mscorlib]System.Console::Write(char)
        ldloc 7                         //variable value d
        call void [mscorlib]System.Console::Write(char)
        ldloc 8                         //variable value ex
        call void [mscorlib]System.Console::Write(char)
        ldc.i4 0                        //constant 0
        ret                             
}                                       
