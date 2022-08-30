using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace JaysModFramework
{
    using GVector3 = GTA.Math.Vector3;
    public struct Vector3: IXmlSerializable
    {
        public GVector3 BaseVector;
        #region BaseProperties
        public float X
        {
            get { return BaseVector.X; }
            set { BaseVector.X = value; }
        }
        public float Y
        {
            get { return BaseVector.Y; }
            set { BaseVector.Y = value; }
        }
        public float Z
        {
            get { return BaseVector.Z; }
            set { BaseVector.Z = value; }
        }
        #endregion
        #region BaseMethods
        public float DistanceTo2D(Vector3 otherVector)
        {
            return BaseVector.DistanceTo2D(otherVector.BaseVector);
        }
        public float DistanceTo(Vector3 otherVector)
        {
            return BaseVector.DistanceTo(otherVector.BaseVector);
        }
        #endregion
        #region Operators
        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.BaseVector + b.BaseVector);
        }
        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.BaseVector - b.BaseVector);
        }
        public static Vector3 operator /(Vector3 a, int b)
        {
            return new Vector3(a.BaseVector / b);
        }
        public static Vector3 operator *(Vector3 a, float b)
        {
            return new Vector3(a.BaseVector * b);
        }
        #endregion
        #region ExtensionProperties
        #endregion
        #region ExtensionMethods
        #endregion
        #region Constructors
        public Vector3(GVector3 baseVector)
        {
            BaseVector = baseVector;
        }
        public Vector3(float X, float Y, float Z)
        {
            BaseVector = new GVector3(X, Y, Z);
        }
        #endregion
        #region XmlSerialization
        public void WriteXml(XmlWriter writer)
        {
            XmlSerialization.WriteElement(writer, "X", X);
            XmlSerialization.WriteElement(writer, "Y", Y);
            XmlSerialization.WriteElement(writer, "Z", Z);
        }
        public void ReadXml(XmlReader reader)
        {
            X = XmlSerialization.ReadElement<float>(reader, "X");
            Y = XmlSerialization.ReadElement<float>(reader, "Y");
            Z = XmlSerialization.ReadElement<float>(reader, "Z");
        }
        #endregion
    }
}
