using JMF.Native;
using OOD.Collections;

namespace JMF
{
    public class VehicleModel: IXMLDatabaseItem<string>
    {
        public int Hash = 0;
        public string ID { get; set; } = "";
        public VehicleClass Class = VehicleClass.Sedans;
        public string Subclass = "";
        public string GTAMake
        {
            get { return Function.Call<string>(Native.Hash.GetMakeNameFromVehicleModel, Hash); }
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
