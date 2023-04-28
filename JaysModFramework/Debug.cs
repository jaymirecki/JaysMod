using GTA;
using JaysModFramework.Clothing;
using JaysModFramework.Menus;
using LemonUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaysModFramework
{
    public static class Debug
    {
        public static bool DEBUG = false;
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
        public static Menu Menu(ObjectPool pool)
        {
            Menu menu = new Menu("Debug", "Debug", "JMF Framework Debug Options", pool);
            AddCommonMenu(menu, pool);
            if (Framework.Initialized)
            {
                AddInitializedMenu(menu, pool);
            }
            else
            {
                AddUninitializedMenu(menu, pool);
            }
            return menu;
        }
        private static void AddCommonMenu(Menu menu, ObjectPool pool)
        {

        }
        private static void AddInitializedMenu(Menu menu, ObjectPool pool)
        {
            menu.Add(new Closet().Menu(Game.Player.Character, pool));
        }
        private static void AddUninitializedMenu(Menu menu, ObjectPool pool)
        {
            MenuItem initButton = new MenuItem("Initialize Framework", "Initialize JMF components");
            initButton.Activated += (sender, args) =>
            {
                Framework.Initialize();
                pool.HideAll();
            };
            menu.Add(initButton);
        }
    }
}
