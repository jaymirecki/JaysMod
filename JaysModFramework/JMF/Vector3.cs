using System.Xml.Serialization;

namespace JMF
{
    public struct Vector3
    {
        ///////////////////////////////////////////////////////////////////////
        //                             Properties                            //
        ///////////////////////////////////////////////////////////////////////
        #region Properties
        [XmlAttribute]
        public float X { get; set; }
        [XmlAttribute]
        public float Y { get; set; }
        [XmlAttribute]
        public float Z { get; set; }
        #endregion Properties
        ///////////////////////////////////////////////////////////////////////
        //                            Constructors                           //
        ///////////////////////////////////////////////////////////////////////
        #region Constructors
        public Vector3(float x = 403, float y = 2024, float z = 113)
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
        public float HeadingTo(Vector3 destination)
        {
            if (destination.X == X && destination.Y > Y)
            {
                if (destination.Y > Y)
                {
                    return 0;
                }
                else
                {
                    return 180;
                }
            }

            float theta = (float)System.Math.Atan(System.Math.Abs((destination.Y - Y) / (destination.X - X)));
            return GetHeadingFromTheta(theta, this, destination);
        }
        public Vector3 Offset(float distance, float heading)
        {
            float theta = GetThetaFromHeading(heading);
            float x = (float)(distance * System.Math.Cos(theta)) * (heading < 180 ? -1 : 1);
            float y = (float)(distance * System.Math.Sin(theta)) * ((heading < 270) && (heading > 90) ? -1 : 1);
            return new Vector3(X + x, Y + y, Z);
        }
        private static float GetThetaFromHeading(float heading)
        {
            float theta;
            // Calculate angle between heading and X-axis
            if (heading <= 90)
            {
                theta = 90 - heading;
            }
            else if (heading <= 180)
            {
                theta = heading - 90;
            }
            else if (heading <= 270)
            {
                theta = 270 - heading;
            }
            else
            {
                theta = heading - 270;
            }
            // Convert to radians and return
            return (float)(theta * (System.Math.PI / 180));
        }
        private static float GetHeadingFromTheta(float theta, Vector3 origin, Vector3 destination)
        {
            float thetaDegrees = (float)((theta * 180) / System.Math.PI);
            Debug.Log(DebugSeverity.Error, thetaDegrees);
            float heading;
            if ((destination.X >= origin.X) && (destination.Y >= origin.Y))
            {
                heading = 270 + thetaDegrees;
            }
            else if ((destination.X >= origin.X) && (destination.Y < origin.Y))
            {
                heading = 270 - thetaDegrees;
            }
            else if ((destination.X < origin.X) && (destination.Y < origin.Y))
            {
                heading = 90 + thetaDegrees;
            }
            else
            {
                heading = 90 - thetaDegrees;
            }
            return heading;
        }
        public Vector3 Offset(Vector3 offset)
        {
            return new Vector3(X + offset.X, Y + offset.Y, Z + offset.Z);
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
        public override string ToString()
        {
            return X + "," + Y + "," + Z;
        }
        public static explicit operator Rage.Vector3(Vector3 v) => new Rage.Vector3(v.X, v.Y, v.Z);
        public static explicit operator Vector3(Rage.Vector3 v) => new Vector3(v.X, v.Y, v.Z);
    }
}
