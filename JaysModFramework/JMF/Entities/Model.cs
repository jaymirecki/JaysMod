using JMF.Native;
using System;
using System.Xml.Serialization;

namespace JMF
{
    public class Model
    {
        [XmlAttribute]
        public uint Hash { get; set; }
        public bool IsLoaded
        {
            get { return Function.Call<bool>(Native.Hash.HasModelLoaded, Hash); }
        }
        [Obsolete(
            "Paramterless constructor is strictly for XML serialization, " +
            "please use parameterized constructor instead.",
            true)]
        public Model() : this(1) { }
        public Model(uint hash)
        {
            Hash = hash;
        }
        public Model(string name)
        {
            Hash = Function.GetHashKey(name);
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
        public void NotNeeded()
        {
            Function.Call(Native.Hash.SetModelAsNoLongerNeeded, Hash);
        }
    }
}
