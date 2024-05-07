using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;

using InputArgument = Rage.Native.NativeArgument;

namespace JaysModFramework
{
    namespace Native
    {
        public static class Function
        {
            public static T Call<T>(Hash hash)
            {
                return (T)Rage.Native.NativeFunction.Call((ulong)hash, typeof(T));
            }
            public static T Call<T>(Hash hash, InputArgument p0)
            {
                return (T)Rage.Native.NativeFunction.Call
                    ((ulong)hash,
                    typeof(T),
                    p0
                    );
            }
            public static T Call<T>(Hash hash, 
                InputArgument p0, 
                InputArgument p1
                )
            {
                return (T)Rage.Native.NativeFunction.Call
                    ((ulong)hash,
                    typeof(T),
                    p0,
                    p1
                    );
            }
            public static T Call<T>(Hash hash,
                InputArgument p0,
                InputArgument p1,
                InputArgument p2
                )
            {
                return (T)Rage.Native.NativeFunction.Call
                    ((ulong)hash,
                    typeof(T),
                    p0,
                    p1,
                    p2
                    );
            }
            public static T Call<T>(Hash hash,
                InputArgument p0,
                InputArgument p1,
                InputArgument p2,
                InputArgument p3
                )
            {
                return (T)Rage.Native.NativeFunction.Call
                    ((ulong)hash,
                    typeof(T),
                    p0,
                    p1,
                    p2,
                    p3
                    );
            }
            public static T Call<T>(Hash hash,
                InputArgument p0,
                InputArgument p1,
                InputArgument p2,
                InputArgument p3,
                InputArgument p4
                )
            {
                return (T)Rage.Native.NativeFunction.Call
                    ((ulong)hash,
                    typeof(T),
                    p0,
                    p1,
                    p2,
                    p3,
                    p4
                    );
            }
            public static void Call(Hash hash)
            {
                Rage.Native.NativeFunction.Call((ulong)hash, typeof(void));
            }
            public static void Call(Hash hash, InputArgument p0)
            {
                Rage.Native.NativeFunction.CallByHash<uint>(
                    (ulong)hash,
                    p0
                    );
            }
            public static void Call(Hash hash,
                InputArgument p0,
                InputArgument p1
                )
            {
                Rage.Native.NativeFunction.CallByHash<uint>(
                    (ulong)hash,
                    p0,
                    p1
                    );
            }
            public static void Call(Hash hash,
                InputArgument p0,
                InputArgument p1,
                InputArgument p2
                )
            {
                Rage.Native.NativeFunction.CallByHash<uint>(
                    (ulong)hash,
                    p0,
                    p1,
                    p2
                    );
            }
            public static void Call(Hash hash,
                InputArgument p0,
                InputArgument p1,
                InputArgument p2,
                InputArgument p3
                )
            {
                Rage.Native.NativeFunction.CallByHash<uint>(
                    (ulong)hash,
                    p0,
                    p1,
                    p2,
                    p3
                    );
            }
            public static void Call(Hash hash,
                InputArgument p0,
                InputArgument p1,
                InputArgument p2,
                InputArgument p3,
                InputArgument p4
                )
            {
                Rage.Native.NativeFunction.CallByHash<uint>(
                    (ulong)hash,
                    p0,
                    p1,
                    p2,
                    p3,
                    p4
                    );
            }
        }
    }
}
