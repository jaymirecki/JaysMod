using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaysMod
{
    static class MaleCloset
    {
        private struct Shirt
        {
            int Upper, AccOne, ShirtOverlay, Neck;
            public Shirt(int upper, int accOne, int shirtOverlay, int neck)
            {
                Upper = upper;
                AccOne = accOne;
                ShirtOverlay = shirtOverlay;
                Neck = neck;
            }
        }
        enum MaleShirts { VNeck_TShirt, Crew_TShirt, Naked, Tuxedo_Open, Tuxedo_Tie };

        static Shirt[] MaleShirtsArray;

        public static void SetupShirts()
        {
            MaleShirtsArray = new Shirt[Enum.GetValues(typeof(MaleShirts)).Length];
            MaleShirtsArray[(int)MaleShirts.VNeck_TShirt] = new Shirt(0, 15, 16, 0);
            MaleShirtsArray[(int)MaleShirts.Crew_TShirt] = new Shirt(0, 15, 0, 0);
            MaleShirtsArray[(int)MaleShirts.Naked] = new Shirt(15, 15, 252, 0);
            MaleShirtsArray[(int)MaleShirts.Tuxedo_Open] = new Shirt(1, 32, 30, 0);
            MaleShirtsArray[(int)MaleShirts.Tuxedo_Tie] = new Shirt(1, 31, 30, 27);
        }
    }
}
