using System;
using System.Windows.Forms;

namespace JaysModFramework.Managers
{
    internal interface IManager
    {
        void OnKeyDown(object sender, KeyEventArgs e);
        void OnTick(object sender, EventArgs e);
        //void Activate();
        //void Deactivate();
        //bool Toggle();
    }
}
