using JMF.Native;

namespace JMF
{
    public class Model
    {
        public uint Hash { get; }
        public bool IsLoaded
        {
            get { return Function.Call<bool>(Native.Hash.HasModelLoaded, Hash); }
        }
        public Model(uint hash)
        {
            Hash = hash;
        }
        public bool Request()
        {
            if (Function.Call<bool>(Native.Hash.IsModelValid, Hash))
            {
                Function.Call(Native.Hash.RequestModel, Hash);
                while (!IsLoaded)
                {
                    Thread.Sleep(100);
                    Function.Call(Native.Hash.RequestModel, Hash);
                }
                return true;
            }
            Debug.Log(DebugSeverity.Warning, "Model " + Hash + " is not valid");
            return false;
        }
    }
}
