using JMF.Math;
using InputArgument = Rage.Native.NativeArgument;

namespace JMF
{
    namespace Native
    {
        internal static class Function
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
            public static T Call<T>(Hash hash,
                InputArgument p0,
                InputArgument p1,
                InputArgument p2,
                InputArgument p3,
                InputArgument p4,
                InputArgument p5
                )
            {
                return (T)Rage.Native.NativeFunction.Call
                    ((ulong)hash,
                    typeof(T),
                    p0,
                    p1,
                    p2,
                    p3,
                    p4,
                    p5
                    );
            }
            public static T Call<T>(Hash hash,
                InputArgument p0,
                InputArgument p1,
                InputArgument p2,
                InputArgument p3,
                InputArgument p4,
                InputArgument p5,
                InputArgument p6
                )
            {
                return (T)Rage.Native.NativeFunction.Call
                    ((ulong)hash,
                    typeof(T),
                    p0,
                    p1,
                    p2,
                    p3,
                    p4,
                    p5,
                    p6
                    );
            }
            public static T Call<T>(Hash hash,
                InputArgument p0,
                InputArgument p1,
                InputArgument p2,
                InputArgument p3,
                InputArgument p4,
                InputArgument p5,
                InputArgument p6,
                InputArgument p7,
                InputArgument p8,
                InputArgument p9,
                InputArgument p10,
                InputArgument p11,
                InputArgument p12,
                InputArgument p13
                )
            {
                return (T)Rage.Native.NativeFunction.Call
                    ((ulong)hash,
                    typeof(T),
                    p0,
                    p1,
                    p2,
                    p3,
                    p4,
                    p5,
                    p6,
                    p7,
                    p8,
                    p9,
                    p10,
                    p11,
                    p12,
                    p13
                    );
            }
            public static void Call(Hash hash)
            {
                Rage.Native.NativeFunction.CallByHash<uint>((ulong)hash);
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
            public static void Call(Hash hash,
                InputArgument p0,
                InputArgument p1,
                InputArgument p2,
                InputArgument p3,
                InputArgument p4,
                InputArgument p5
                )
            {
                Rage.Native.NativeFunction.CallByHash<uint>(
                    (ulong)hash,
                    p0,
                    p1,
                    p2,
                    p3,
                    p4,
                    p5
                    );
            }
            public static void Call(Hash hash,
                InputArgument p0,
                InputArgument p1,
                InputArgument p2,
                InputArgument p3,
                InputArgument p4,
                InputArgument p5,
                InputArgument p6
                )
            {
                Rage.Native.NativeFunction.CallByHash<uint>(
                    (ulong)hash,
                    p0,
                    p1,
                    p2,
                    p3,
                    p4,
                    p5,
                    p6
                    );
            }
            public static void Call(Hash hash,
                InputArgument p0,
                InputArgument p1,
                InputArgument p2,
                InputArgument p3,
                InputArgument p4,
                InputArgument p5,
                InputArgument p6,
                InputArgument p7,
                InputArgument p8,
                InputArgument p9,
                InputArgument p10,
                InputArgument p11,
                InputArgument p12,
                InputArgument p13
                )
            {
                Rage.Native.NativeFunction.CallByHash<uint>(
                    (ulong)hash,
                    p0,
                    p1,
                    p2,
                    p3,
                    p4,
                    p5,
                    p6,
                    p7,
                    p8,
                    p9,
                    p10,
                    p11,
                    p12,
                    p13
                    );
            }
            public static void Call(Hash hash,
                InputArgument p0,
                InputArgument p1,
                InputArgument p2,
                InputArgument p3,
                InputArgument p4,
                InputArgument p5,
                InputArgument p6,
                InputArgument p7,
                InputArgument p8,
                InputArgument p9,
                InputArgument p10,
                InputArgument p11,
                InputArgument p12,
                InputArgument p13,
                InputArgument p14
                )
            {
                Rage.Native.NativeFunction.CallByHash<uint>(
                    (ulong)hash,
                    p0,
                    p1,
                    p2,
                    p3,
                    p4,
                    p5,
                    p6,
                    p7,
                    p8,
                    p9,
                    p10,
                    p11,
                    p12,
                    p13,
                    p14
                    );
            }
            public static void Call(Hash hash,
                InputArgument p0,
                InputArgument p1,
                InputArgument p2,
                InputArgument p3,
                InputArgument p4,
                InputArgument p5,
                InputArgument p6,
                InputArgument p7,
                InputArgument p8,
                InputArgument p9,
                InputArgument p10,
                InputArgument p11,
                InputArgument p12,
                InputArgument p13,
                InputArgument p14,
                InputArgument p15,
                InputArgument p16,
                InputArgument p17,
                InputArgument p18,
                InputArgument p19,
                InputArgument p20,
                InputArgument p21,
                InputArgument p22,
                InputArgument p23
                )
            {
                Rage.Native.NativeFunction.CallByHash<uint>(
                    (ulong)hash,
                    p0,
                    p1,
                    p2,
                    p3,
                    p4,
                    p5,
                    p6,
                    p7,
                    p8,
                    p9,
                    p10,
                    p11,
                    p12,
                    p13,
                    p14,
                    p15,
                    p16,
                    p17,
                    p18,
                    p19,
                    p20,
                    p21,
                    p22,
                    p23
                    );
            }
        }
    }
}
