using JMF.Native;
using System;

namespace JMF
{
    public class Clock
    {
        ///////////////////////////////////////////////////////////////////////
        //                             Properties                            //
        ///////////////////////////////////////////////////////////////////////
        #region Properties
        public DateTime Date
        {
            get
            {
                int year = Function.Call<int>(Hash.GetClockYear);
                int month = Function.Call<int>(Hash.GetClockMonth);
                int day = Function.Call<int>(Hash.GetClockDayOfMonth);
                int hour = Function.Call<int>(Hash.GetClockHours);
                int minute = Function.Call<int>(Hash.GetClockMinutes);
                int second = Function.Call<int>(Hash.GetClockSeconds);
                return new DateTime(year, month, day, hour, minute, second);
            }
            set
            {
                Function.Call(Hash.SetClockDate, value.Day, value.Month, value.Year);
                Function.Call(Hash.SetClockTime, value.Hour, value.Minute, value.Second);
            }
        }
        #endregion Properties
        ///////////////////////////////////////////////////////////////////////
        //                            Constructors                           //
        ///////////////////////////////////////////////////////////////////////
        #region Constructors
        public Clock()
        {
        }
        #endregion Constructors
        ///////////////////////////////////////////////////////////////////////
        //                              Methods                              //
        ///////////////////////////////////////////////////////////////////////
        #region Methods
        public void Pause(bool toggle)
        {
            Function.Call(Hash.PauseClock, toggle);
        }
        #endregion Methods
    }
}
