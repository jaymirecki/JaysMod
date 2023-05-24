using GTA;
using JaysModFramework.Clothing;
using JaysModFramework.Menus;
using LemonUI;
using System;
using System.IO;

namespace JaysModFramework
{
    public enum DebugSeverity
    {
        Info = 1,
        Warning = 2,
        Error = 3,
    }
    public static class Debug
    {
        private const string _logFilePath = ".\\scripts\\JaysModFramework.log";
        private static readonly FileStream _logStream = new FileStream(_logFilePath, FileMode.OpenOrCreate);
        private static readonly StreamWriter _logWriter = new StreamWriter(_logStream);
        public static bool DEBUG = false;
        private static Menu _menu;
        private static MenuItem _initButton;
        private static SubmenuItem _closetMenuItem;
        private static void Notify(string value, bool overrideDebugFlag = false)
        {
            if (DEBUG || overrideDebugFlag)
            {
                GTA.UI.Notification.Show(value);
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
        public static Menu Menu(ObjectPool pool)
        {
            _menu = new Menu("Debug", "Debug", "JMF Framework Debug Options", pool);

            _menu.Add(InitButton(pool));
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
            _initButton.Enabled = !Framework.Initialized;
            if (Framework.Initialized)
            {
                _closetMenuItem.Enabled = true;
            }
        }

        private static MenuItem InitButton(ObjectPool pool)
        {
            _initButton = new MenuItem("Initialize Framework", "Initialize JMF components");
            _initButton.Activated += (sender, args) =>
            {
                Framework.Initialize();
                AddClosetMenu(pool);
                CheckInitialization();
            };
            return _initButton;
        }
        private static void AddClosetMenu(ObjectPool pool)
        {
            Closet closet = new Closet();
            Menu closetMenu = closet.Menu(Game.Player.Character, pool);
            _closetMenuItem = _menu.Add(closetMenu);
        }
    }
}
