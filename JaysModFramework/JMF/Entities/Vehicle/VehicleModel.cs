using JMF.Native;
using OOD.Collections;
using System.Globalization;

namespace JMF
{
    public class VehicleModel: IXMLDatabaseItem<string>
    {
        public uint Hash = 0;
        private string _modelName;
        public string ModelName
        {
            get { return _modelName; }
            set
            {
                _modelName = value;
                Hash = Function.GetHashKey(_modelName);
            }
        }
        public string ID
        {
            get { return ModelName; }
            set { ModelName = value; }
        }
        public VehicleClass Class = VehicleClass.Sedans;
        public string Subclass = "";
        public string GTAMake
        {
            get
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                string make = Function.Call<string>(Native.Hash.GetMakeNameFromVehicleModel, Hash);
                return textInfo.ToTitleCase(make.ToLower());
            }
            set { }
        }
        public string GTAModel = "";
        public string RWMake = "";
        public string RWModel = "";
        public float MaxSpeed
        {
            get { return Function.Call<float>(Native.Hash.GetVehicleModelEstimatedMaxSpeed, Hash); }
            set { }
        }
        public float MaxKnots
        {
            get { return Function.Call<float>(Native.Hash.GetVehicleModelMaxKnots, Hash); }
            set { }
        }
        public int NumberOfSeats
        {
            get { return Function.Call<int>(Native.Hash.GetVehicleModelNumberOfSeats, Hash); }
            set { }
        }

        public ValidationState Validate()
        {
            throw new System.NotImplementedException();
        }
    }
}
