using JMF.Menus;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace JMF
{
    public class State
    {
        #region Properties
        public DateTime Date
        {
            get { return Game.Clock.Date; }
            set { Game.Clock.Date = value; }
        }
        public WeatherType Weather
        {
            get { return World.Weather; }
            set { World.Weather = value; }
        }
        private string _worldspace = "";
        public string Worldspace
        {
            get
            {
                if (Universe.Truth.CurrentWorldspace != null)
                {

                    _worldspace = Universe.Truth.CurrentWorldspace.ID;
                }
                return _worldspace;
            }
            set
            {
                _worldspace = value;
                if (_worldspace != "")
                {
                    Universe.Truth.ChangeWorldspace(_worldspace, _map);
                }
            }
        }
        private string _map = "";
        public string Map
        {
            get
            {
                if (Universe.Truth.CurrentWorldspace != null)
                {
                    _map = Universe.Truth.CurrentWorldspace.CurrentMap;
                }
                return _map;
            }
            set
            {
                _map = value;
                if (_worldspace != "")
                {
                    Universe.Truth.ChangeWorldspace(_worldspace, _map);
                }
            }
        }
        #endregion
        #region Constructors
        public State()
        {
        }
        #endregion
    }
}
