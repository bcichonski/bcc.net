.assembly extern mscorlib {}                                
.assembly wielkwad { }                                
.module wielkwad.exe                                
.method static int32 main() cil managed                                
{                                       
        .entrypoint                     
        .locals init (int32)            //Int32 a
        .locals init (int32)            //Int32 b
        .locals init (int32)            //Int32 c
        .locals init (int32)            //Int32 x
        .locals init (int32)            //Int32 wx
        ldc.i4.s 97                     //constant 'a'
        .locals init (char)             //Char ca
        stloc 5                         //ca=
        ldc.i4.s 98                     //constant 'b'
        .locals init (char)             //Char cb
        stloc 6                         //cb=
        ldc.i4.s 99                     //constant 'c'
        .locals init (char)             //Char cc
        stloc 7                         //cc=
        ldc.i4.s 120                    //constant 'x'
        .locals init (char)             //Char cx
        stloc 8                         //cx=
        ldc.i4.s 61                     //constant '='
        .locals init (char)             //Char ceq
        stloc 9                         //ceq=
        ldc.i4.s 87                     //constant 'W'
        .locals init (char)             //Char cW
        stloc 10                        //cW=
        ldc.i4.s 40                     //constant '('
        .locals init (char)             //Char cbr1
        stloc 11                        //cbr1=
        ldc.i4.s 41                     //constant ')'
        .locals init (char)             //Char cbr2
        stloc 12                        //cbr2=
        ldloc 5                         //variable value ca
        call void [mscorlib]System.Console::Write(char)
        ldloc 9                         //variable value ceq
        call void [mscorlib]System.Console::Write(char)
        call string [mscorlib]System.Console::ReadLine()
        call int32 [mscorlib]System.Int32::Parse(string)
        stloc 0                         //a=
        ldloc 6                         //variable value cb
        call void [mscorlib]System.Console::Write(char)
        ldloc 9                         //variable value ceq
        call void [mscorlib]System.Console::Write(char)
        call string [mscorlib]System.Console::ReadLine()
        call int32 [mscorlib]System.Int32::Parse(string)
        stloc 1                         //b=
        ldloc 7                         //variable value cc
        call void [mscorlib]System.Console::Write(char)
        ldloc 9                         //variable value ceq
        call void [mscorlib]System.Console::Write(char)
        call string [mscorlib]System.Console::ReadLine()
        call int32 [mscorlib]System.Int32::Parse(string)
        stloc 2                         //c=
        ldloc 8                         //variable value cx
        call void [mscorlib]System.Console::Write(char)
        ldloc 9                         //variable value ceq
        call void [mscorlib]System.Console::Write(char)
        call string [mscorlib]System.Console::ReadLine()
        call int32 [mscorlib]System.Int32::Parse(string)
        stloc 3                         //x=
        ldloc 0                         //variable value a
        ldloc 3                         //variable value x
        mul                             
        ldloc 1                         //variable value b
        add                             
        ldloc 3                         //variable value x
        mul                             
        ldloc 2                         //variable value c
        add                             
        stloc 4                         //wx=
        ldloc 10                        //variable value cW
        call void [mscorlib]System.Console::Write(char)
        ldloc 11                        //variable value cbr1
        call void [mscorlib]System.Console::Write(char)
        ldloc 3                         //variable value x
        call void [mscorlib]System.Console::Write(int32)
        ldloc 12                        //variable value cbr2
        call void [mscorlib]System.Console::Write(char)
        ldloc 9                         //variable value ceq
        call void [mscorlib]System.Console::Write(char)
        ldloc 4                         //variable value wx
        call void [mscorlib]System.Console::Write(int32)
        ldc.i4 0                        //constant 0
        ret                             
}                                       
