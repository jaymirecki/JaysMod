namespace JMF.Math
{
    public struct Vector3
    {
        ///////////////////////////////////////////////////////////////////////
        //                             Properties                            //
        ///////////////////////////////////////////////////////////////////////
        #region Properties
        public float X { get; }
        public float Y { get; }
        public float Z { get; }
        #endregion Properties
        ///////////////////////////////////////////////////////////////////////
        //                            Constructors                           //
        ///////////////////////////////////////////////////////////////////////
        #region Constructors
        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        #endregion Constructors
        ///////////////////////////////////////////////////////////////////////
        //                              Methods                              //
        ///////////////////////////////////////////////////////////////////////
        #region Methods
        public float DistanceTo(Vector3 destination)
        {
            return Sqrt(Pow(X - destination.X, 2) + Pow(Y - destination.Y, 2) + Pow(Z - destination.Z, 2));
        }
        public float DistanceTo2D(Vector3 destination)
        {
            return Sqrt(Pow(X - destination.X, 2) + Pow(Y - destination.Y, 2));
        }
        #endregion Methods
        #region Math Helpers
        public static float Sqrt(float a)
        {
            return (float)System.Math.Sqrt(a);
        }
        public static float Pow(float a, float b)
        {
            return (float)System.Math.Pow(a, b);
        }
        #endregion Math Helpers
    }
}
