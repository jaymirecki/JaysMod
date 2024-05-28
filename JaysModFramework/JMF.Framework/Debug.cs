using JMF.Menus;
using JMF.Native;
using System;
using System.IO;

namespace JMF
{
    public enum DebugSeverity
    {
        Info = 1,
        Warning = 2,
        Error = 3,
    }
    public static class Debug
    {
        private const string _logFilePath = ".\\Plugins\\JaysModFramework.log";
        private static readonly FileStream _logStream = new FileStream(_logFilePath, FileMode.Create);
        private static readonly StreamWriter _logWriter = new StreamWriter(_logStream);
        public static bool DEBUG = false;
        private static Menu _menu;
        private static void Notify(string value, bool overrideDebugFlag = false)
        {
            if (DEBUG || overrideDebugFlag)
            {
                Native.Function.Call(Native.Hash.BeginTextCommandTheFeedPost, "STRING");
                Native.Function.Call(Native.Hash.AddTextComponentSubstringPlayerName, value);
                Native.Function.Call(Native.Hash.EndTextCommandThefeedPostTicker, true, true);
            }
        }
        public static void Notify<T>(T value, bool overrideDebugFlag = false)
        {
            if (value == null)
            {
                Notify("NULL", overrideDebugFlag);
            }
            else
            {
                Notify(value.ToString(), overrideDebugFlag);
            }
        }
        public static void Notify<T>(string label, T value, bool overrideDebugFlag = false)
        {
            Notify(label + ": " + value.ToString(), overrideDebugFlag);
        }
        public static void Log(DebugSeverity severity, string message)
        {
            string logMessage =
                "[" + DateTime.Now.ToShortTimeString() + "] " +
                "[" + severity + "]: " +
                message;
            _logWriter.WriteLine(logMessage);
            _logWriter.Flush();
            if (severity > DebugSeverity.Info)
            {
                Notify(logMessage, true);
            }
            else
            {
                Notify(logMessage);
            }
        }
        public static Menu Menu()
        {
            _menu = new Menu("Debug", "Debug", "JMF Framework Debug Options", Framework.ObjectPool);

            MenuItem position = new MenuItem("Current Position");
            position.Activated += (sender, args) =>
            {
                Notify("Current Position: " + Game.Player.Character.Position.ToString(), true);
            };
            MenuItem heading = new MenuItem("Current Heading");
            heading.Activated += (sender, args) =>
            {
                Notify("Current Heading: " + Game.Player.Character.Heading.ToString(), true);
            };
            MenuItem interiorId = new MenuItem("Current InteriorId");
            interiorId.Activated += (sender, args) =>
            {
                Vector3 curPos = Game.Player.Character.Position;
                Notify("Current InteriorId: " + Function.Call<int>(Hash.GetInteriorAtCoords, curPos.X, curPos.Y, curPos.Z), true);
            };

            _menu.Add(position);
            _menu.Add(heading);
            _menu.Add(interiorId);

            return _menu;
        }
    }
}
