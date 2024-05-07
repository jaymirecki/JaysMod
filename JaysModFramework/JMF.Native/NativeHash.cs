using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMF
{
    namespace Native
    {
        public enum Hash : ulong
        {
            GetEntityHeading = 0xE83D4F9BA2A38914,
            SetEntityHeading = 0x8E2530AA8ADA980,
            GetEntityModel = 0x9F47B058362C84B5,
            GetEntityCoords = 0x3FEF770D40960D5A,
            SetEntityCoords = 0x06843DA7060A026B,
            IsControlEnabled = 0x1CEA6BFDF248E5D9,
            IsControlJustPressed = 0x580417101DDB492F,
            IsControlJustReleased = 0x50F940259D3841E6,
            IsControlPressed = 0xF3A21BCD95725A4A,
            BeginTextCommandTheFeedPost = 0x202709F4C58A0424,
            EndTextCommandThefeedPostTicker = 0x2ED7843F8F801023,
            AddTextComponentSubstringPlayerName = 0x6C188BE134E074AA,
            GetPlayerPed = 0x43A66C31C68491C0,
            PlayerId = 0x4F8644AF03D0E0D6,
            IsVehicleSirenOn = 0x4C9BF537BE2634B2,
            IsVehicleSirenAudioOn = 0xB5CC40FBCB586380,
            SetVehicleSiren = 0xF4924635A19EB37D,
            SetVehicleHasMutedSirens = 0xD8050E0EB60CF274,
            GetVehiclePedIsIn = 0x9A9112A0FE9A4713,
            IsPedInVehicle = 0xA3EE4A07279BB9DB,
            IsPedInAnyVehicle = 0x997ABD671D25CA0B,
            //IsBigmapActive = 0xFFF65C63,
            SetBigmapActive = 0x231C8F89D0539D8F,
        }
    }
}
