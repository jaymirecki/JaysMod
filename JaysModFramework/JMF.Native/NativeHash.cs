﻿namespace JMF
{
    namespace Native
    {
        internal enum Hash : ulong
        {
            GetEntityHeading = 0xE83D4F9BA2A38914,
            SetEntityHeading = 0x8E2530AA8ADA980E,
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
            PauseClock = 0x4055E40BD2DBEC1D,
            GetClockYear = 0x961777E64BDAF717,
            GetClockMonth = 0xBBC72712E80257A1,
            GetClockDayOfMonth = 0x3D10BC92A4DB1D35,
            GetClockHours = 0x25223CA6B4D20B7F,
            GetClockMinutes = 0x13D2B8ADD79640F2,
            GetClockSeconds = 0x494E97C2EF27C470,
            SetClockDate = 0xB096419DF0D06CE7,
            SetClockTime = 0x47C3B5848C3E45D8,
            EndTextCommandDisplayHelp = 0x238FFE5C7B0498A6,
            BeginTextCommandDisplayHelp = 0x8509B634FBE7DA11,
            SetPlayerModel = 0x00A1CADD00108836,
            RequestModel = 0x963D27A58DF860AC,
            HasModelLoaded = 0x98A4EB5D89A0C952,
            SetModelAsNoLongerNeeded = 0xE532F5D78798DAAB,
            IsModelValid = 0xC0296A2EDF545E92,
            SetPedDefaultComponentVariation = 0x45EEE61580806D63,
            TerminateAllScriptsWithThisName = 0x9DC711BC69C548DF,
            NetworkRequestControlOfEntity = 0xB69317BF5E782347,
            NetworkResurrectLocalPlayer = 0xEA23C49EAA83ACFB,
            IgnoreNextRestart = 0x21FFB63D8C615361,
            ResetPlayerArrestState = 0x2D03E13C460760D6,
            DisplayHud = 0xA6294919E56FF02A,
            DisplayRadar = 0xA0EBB943C300E693,
            GetPlayerInvincible = 0xB721981B2B939E07,
            SetPlayerInvincible = 0x239528EACDC3E7DE,
            SetEntityInvincible = 0x3882114BDE571AD4,
            AnimpostfxStop = 0x068E835A1D0DC0E3,
            AnimpostfxStopAll = 0xB4EDDC19532BFB85,
            SetPedToRagdoll = 0xAE99FB955581844A,
            DoScreenFadeOut = 0x891B5B39AC6302AF,
            IsScreenFadedOut = 0xB16FCE9DDC7BA182,
            DoScreenFadeIn = 0xD4E8E24955024033,
            IsScreenFadedIn = 0x5A859503B0C08678,
            SetTimeScale = 0x1D408577D440E81E,
            IsPlayerDead = 0x424D4687FA1E5652,
            IsEntityDead = 0x5F9532F3B5CC2551,
            GetEntityHealth = 0xEEF059FAD016D209,
            SetEntityHealth = 0x6B76DC1F3AE6E6A3,
            SetFadeOutAfterDeath = 0x4A18E01DF2C87B86,
            SetEntityCoordsNoOffset = 0x239A3351AC1DA385,
            EnableSpecialAbility = 0x181EC197DAEFE121,
            IsSpecialAbilityEnabled = 0xB1D200FE26AEF3CB,
            GetEntityHeightAboveGround = 0x1DD55701034110E5,
            RequestIpl = 0x41B4893843BBDB74,
            OnEnterMp = 0x0888C3502DBBEEF5,
            RemoveIpl = 0xEE6C5AD3ECE0A82D,
            SetBlipCategory = 0x234CDD44D996FD9A,
            SetBlipColour = 0x03D7FB09E75D6B7E,
            SetBlipDisplay = 0x9029B2F3DA924928,
            SetBlipSprite = 0xDF735600A4696DAF,
            BeginTextCommandSetBlipName = 0xF9113A30DE5C6670,
            EndTextCommandSetBlipName = 0xBC38B49BCB83BC9B,
            AddBlipForCoord = 0x5A039BB0BCA604B6,
            AddBlipForEntity = 0x5CDE92C702A8FCE7,
            GetInteriorAtCoords = 0xB0F7F8663821D9C3,
            DisableInterior = 0x6170941419D7D8EC,
            RefreshInterior = 0x41F37C3427C75AE0,
            ActivateInteriorEntitySet = 0x55E86AF2712B36A1,
            SetInteriorEntitySetColor = 0xC1F1920BAF281317,
            DeactivateInteriorEntitySet = 0x420BD37289EEE162,
            IsIplActive = 0x88A741E44A2B3495,
        }
    }
}
