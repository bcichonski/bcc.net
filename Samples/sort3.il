.assembly extern mscorlib {}                                
.assembly sort3 { }                                
.module sort3.exe                                
.method static int32 main() cil managed                                
{                                       
        .entrypoint                     
        .locals init (int32)            //Int32 a
        .locals init (int32)            //Int32 b
        .locals init (int32)            //Int32 c
        ldc.i4.s 97                     //constant 'a'
        .locals init (char)             //Char ca
        stloc 3                         //ca=
        ldc.i4.s 98                     //constant 'b'
        .locals init (char)             //Char cb
        stloc 4                         //cb=
        ldc.i4.s 99                     //constant 'c'
        .locals init (char)             //Char cc
        stloc 5                         //cc=
        ldc.i4.s 60                     //constant '<'
        .locals init (char)             //Char cgt
        stloc 6                         //cgt=
        ldc.i4.s 61                     //constant '='
        .locals init (char)             //Char ceq
        stloc 7                         //ceq=
        ldloc 3                         //variable value ca
        call void [mscorlib]System.Console::Write(char)
        ldloc 7                         //variable value ceq
        call void [mscorlib]System.Console::Write(char)
        call string [mscorlib]System.Console::ReadLine()
        call int32 [mscorlib]System.Int32::Parse(string)
        stloc 0                         //a=
        ldloc 4                         //variable value cb
        call void [mscorlib]System.Console::Write(char)
        ldloc 7                         //variable value ceq
        call void [mscorlib]System.Console::Write(char)
        call string [mscorlib]System.Console::ReadLine()
        call int32 [mscorlib]System.Int32::Parse(string)
        stloc 1                         //b=
        ldloc 5                         //variable value cc
        call void [mscorlib]System.Console::Write(char)
        ldloc 7                         //variable value ceq
        call void [mscorlib]System.Console::Write(char)
        call string [mscorlib]System.Console::ReadLine()
        call int32 [mscorlib]System.Int32::Parse(string)
        stloc 2                         //c=
        ldloc 0                         //variable value a
        ldloc 1                         //variable value b
        clt                             
        brfalse Label0                  //if then
        ldloc 2                         //variable value c
        ldloc 0                         //variable value a
        clt                             
        brfalse Label1                  //if then
        ldloc 2                         //variable value c
        call void [mscorlib]System.Console::Write(int32)
        ldloc 6                         //variable value cgt
        call void [mscorlib]System.Console::Write(char)
        ldloc 7                         //variable value ceq
        call void [mscorlib]System.Console::Write(char)
        ldloc 0                         //variable value a
        call void [mscorlib]System.Console::Write(int32)
        ldloc 6                         //variable value cgt
        call void [mscorlib]System.Console::Write(char)
        ldloc 7                         //variable value ceq
        call void [mscorlib]System.Console::Write(char)
        ldloc 1                         //variable value b
        call void [mscorlib]System.Console::Write(int32)
        br Label2                       
Label1: nop                             //else
        ldloc 1                         //variable value b
        ldloc 2                         //variable value c
        clt                             
        brfalse Label3                  //if then
        ldloc 0                         //variable value a
        call void [mscorlib]System.Console::Write(int32)
        ldloc 6                         //variable value cgt
        call void [mscorlib]System.Console::Write(char)
        ldloc 7                         //variable value ceq
        call void [mscorlib]System.Console::Write(char)
        ldloc 1                         //variable value b
        call void [mscorlib]System.Console::Write(int32)
        ldloc 6                         //variable value cgt
        call void [mscorlib]System.Console::Write(char)
        ldloc 7                         //variable value ceq
        call void [mscorlib]System.Console::Write(char)
        ldloc 2                         //variable value c
        call void [mscorlib]System.Console::Write(int32)
        br Label4                       
Label3: nop                             //else
        ldloc 0                         //variable value a
        call void [mscorlib]System.Console::Write(int32)
        ldloc 6                         //variable value cgt
        call void [mscorlib]System.Console::Write(char)
        ldloc 7                         //variable value ceq
        call void [mscorlib]System.Console::Write(char)
        ldloc 2                         //variable value c
        call void [mscorlib]System.Console::Write(int32)
        ldloc 6                         //variable value cgt
        call void [mscorlib]System.Console::Write(char)
        ldloc 7                         //variable value ceq
        call void [mscorlib]System.Console::Write(char)
        ldloc 1                         //variable value b
        call void [mscorlib]System.Console::Write(int32)
Label4: nop                             //end if
Label2: nop                             //end if
        br Label5                       
Label0: nop                             //else
        ldloc 2                         //variable value c
        ldloc 1                         //variable value b
        clt                             
        brfalse Label6                  //if then
        ldloc 2                         //variable value c
        call void [mscorlib]System.Console::Write(int32)
        ldloc 6                         //variable value cgt
        call void [mscorlib]System.Console::Write(char)
        ldloc 7                         //variable value ceq
        call void [mscorlib]System.Console::Write(char)
        ldloc 1                         //variable value b
        call void [mscorlib]System.Console::Write(int32)
        ldloc 6                         //variable value cgt
        call void [mscorlib]System.Console::Write(char)
        ldloc 7                         //variable value ceq
        call void [mscorlib]System.Console::Write(char)
        ldloc 0                         //variable value a
        call void [mscorlib]System.Console::Write(int32)
        br Label7                       
Label6: nop                             //else
        ldloc 2                         //variable value c
        ldloc 0                         //variable value a
        clt                             
        brfalse Label8                  //if then
        ldloc 1                         //variable value b
        call void [mscorlib]System.Console::Write(int32)
        ldloc 6                         //variable value cgt
        call void [mscorlib]System.Console::Write(char)
        ldloc 7                         //variable value ceq
        call void [mscorlib]System.Console::Write(char)
        ldloc 2                         //variable value c
        call void [mscorlib]System.Console::Write(int32)
        ldloc 6                         //variable value cgt
        call void [mscorlib]System.Console::Write(char)
        ldloc 7                         //variable value ceq
        call void [mscorlib]System.Console::Write(char)
        ldloc 0                         //variable value a
        call void [mscorlib]System.Console::Write(int32)
        br Label9                       
Label8: nop                             //else
        ldloc 1                         //variable value b
        call void [mscorlib]System.Console::Write(int32)
        ldloc 6                         //variable value cgt
        call void [mscorlib]System.Console::Write(char)
        ldloc 7                         //variable value ceq
        call void [mscorlib]System.Console::Write(char)
        ldloc 0                         //variable value a
        call void [mscorlib]System.Console::Write(int32)
        ldloc 6                         //variable value cgt
        call void [mscorlib]System.Console::Write(char)
        ldloc 7                         //variable value ceq
        call void [mscorlib]System.Console::Write(char)
        ldloc 2                         //variable value c
        call void [mscorlib]System.Console::Write(int32)
Label9: nop                             //end if
Label7: nop                             //end if
Label5: nop                             //end if
        ldc.i4 0                        //constant 0
        ret                             
}                                       
