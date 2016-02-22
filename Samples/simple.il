.assembly simple { }                                
.module simple.exe                                
.method static int32 main() cil managed                                
{                                       
        .entrypoint                     
        ldc.i4 0                        //constant 0
        ldc.i4 1                        //constant 1
        ldc.i4 2                        //constant 2
        ldc.i4 3                        //constant 3
        sub                             
        mul                             
        add                             
        .locals init (int32)            //Int32 i
        stloc 0                         //i=
        ldc.i4.s 65                     //constant 'A'
        .locals init (char)             //Char c
        stloc 1                         //c=
        ldloc 0                         //variable value i
        ldc.i4 0                        //constant 0
        ceq                             
        brfalse Label1                  //if then
        ldc.i4 1                        //constant 1
        stloc 0                         //i=
        br Label2                       
Label1: nop                             //else
        ldc.i4 2                        //constant 2
        stloc 0                         //i=
Label2: nop                             //end if
        ldloc 1                         //variable value c
        call void [mscorlib]System.Console::Write(char)
        ldc.i4 0                        //constant 0
        ret                             
}                                       
