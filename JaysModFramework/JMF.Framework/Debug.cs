using JMF.Menus;
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
        private static MenuItem _initButton;
        private static SubmenuItem _closetMenuItem;
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
            _menu = new Menu("Debug", "Debug", "JMF Framework Debug Options", Global.ObjectPool);

            _menu.Add(InitButton(Global.ObjectPool));
            //AddClosetMenu(pool);

            _menu.Opening += Menu_Opening;
            return _menu;
        }

        private static void Menu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CheckInitialization();
        }
        private static void CheckInitialization()
        {
            _initButton.Enabled = !NativeHash.Initialized;
            if (NativeHash.Initialized)
            {
                _closetMenuItem.Enabled = true;
            }
        }

        private static MenuItem InitButton(ObjectPool pool)
        {
            _initButton = new MenuItem("Initialize Framework", "Initialize JMF components");
            _initButton.Activated += (sender, args) =>
            {
                NativeHash.Initialize();
                //AddClosetMenu(pool);
                CheckInitialization();
            };
            return _initButton;
        }
        //private static void AddClosetMenu(ObjectPool pool)
        //{
        //    Closet closet = new Closet();
        //    Menu closetMenu = closet.Menu(Game.Player.Character, pool);
        //    _closetMenuItem = _menu.Add(closetMenu);
        //}
    }
}
