using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.UI;
using System.ComponentModel;
using GTA.Native;

namespace JaysMod
{
    [ScriptAttributes(NoDefaultInstance = true)]
    class HUD : Script
    {
        public HUD()
        {
            Tick += onTick;
        }

        public void onTick(object sender, EventArgs e)
        {
            Ped player = Game.Player.Character;
            float width = 100;
            float height = 100;
            float textLineSpacing = 20;
            float textScale = 0.5f;
            Color textColor = Color.DodgerBlue;
            GTA.UI.Font textFont = GTA.UI.Font.ChaletLondon;
            Alignment textAlignment = Alignment.Right;
            ContainerElement HUD = new ContainerElement(new PointF(215, 580), new SizeF(height, width), Color.FromArgb(50, Color.Black)); ;
            TextElement Speedometer = new TextElement(Math.Round(Drive.ToMPH(player.Speed), 0).ToString() + " MPH",
                                                      new PointF(width, 0),
                                                      textScale,
                                                      textColor,
                                                      textFont,
                                                      textAlignment);
            float heightAboveGround;
            if (player.IsInVehicle())
                heightAboveGround = player.CurrentVehicle.HeightAboveGround;
            else
                heightAboveGround = player.HeightAboveGround;
            TextElement Altitude = new TextElement(Math.Round(player.Position.Z, 0) + " (" + Math.Round(heightAboveGround, 0) + ") m",
                                                   new PointF(width, textLineSpacing * 1),
                                                   textScale,
                                                   textColor,
                                                   textFont,
                                                   textAlignment);
            TextElement Clock = new TextElement(World.CurrentTimeOfDay.ToString(@"hh\:mm"),
                                                new PointF(width, textLineSpacing * 2),
                                                textScale,
                                                textColor,
                                                textFont,
                                                textAlignment);
            HUD.Items.Add(Speedometer);
            HUD.Items.Add(Clock);
            HUD.Items.Add(Altitude);
            HUD.Draw();
        }
    }
}
