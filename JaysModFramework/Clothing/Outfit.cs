using GTA;
using JaysModFramework.Clothing.Components;
using OOD.Collections;

namespace JaysModFramework.Clothing
{
    public class Outfit: IXMLDatabaseItem<string>
    {
        public string Name;
        public Torso Torso;
        public string TorsoID
        {
            get { return Torso.ID; }
        }
        public string ID
        {
            get { return Name; }
        }
        public Outfit()
        {
            Name = "Default Name";
            Torso = new Torso();
        }
        public Outfit(string name, Torso torso)
        {
            Name = name;
            Torso = torso;
        }
        public void ToPed(Ped ped)
        {
            Torso.SetToPed(ped);
        }
    }
}
