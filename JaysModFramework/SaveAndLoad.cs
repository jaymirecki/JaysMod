using GTA;
using GTA.Math;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaysModFramework
{
    public class SaveAndLoad
    {
        private ScriptSettings Ini;
        private string IniFile;
        public SaveAndLoad(string ini)
        {
            IniFile = ini;
            Ini = ScriptSettings.Load(ini);
        }

        public static string CombinePrefix(string firstPrefix, string secondPrefix)
        {
            if (firstPrefix != "")
            {
                return firstPrefix + "_" + secondPrefix;
            }
            return secondPrefix;
        }
        public List<string> AllSaves()
        {
            StreamReader file = new StreamReader("./" + IniFile);
            List<string> saves = new List<string>();
            string currentLine = file.ReadLine();
            while (currentLine != null)
            {
                if (currentLine.Contains("[SAVE -"))
                {
                    saves.Add(currentLine.Split(new string[] { " - ", "]" }, StringSplitOptions.None)[1]);
                }
                currentLine = file.ReadLine();
            }
            file.Close();
            return saves;
        }
        public void Save<T>(string saveId, string item, T value)
        {
            Ini.SetValue("Save - " + saveId, item, value);
        }
        public void Save()
        {
            Ini.Save();
        }

        public void SaveVector(string saveId, string positionId, Vector3 vector)
        {
            Save(saveId, positionId + "_positionx", vector.X);
            Save(saveId, positionId + "_positiony", vector.Y);
            Save(saveId, positionId + "_positionz", vector.Z);
        }
        public Vector3 LoadVector(string saveId, string positionId)
        {
            Vector3 vector = new Vector3();
            vector.X = Load(saveId, positionId + "_positionx", 0);
            vector.Y = Load(saveId, positionId + "_positiony", 0);
            vector.Z = Load(saveId, positionId + "_positionz", 72);
            return vector;
        }

        public T Load<T>(string saveId, string item)
        {
            return Load(saveId, item, default(T));
        }
        public T Load<T>(string saveId, string item, T defaultValue)
        {
            return Ini.GetValue("Save - " + saveId, item, defaultValue);
        }
    }
}
