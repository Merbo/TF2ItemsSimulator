using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF2ItemsSimulator
{
    public enum Team
    {
        Red,
        Blu,
        Spectator
    }

    class PlayerClass
    {
        public enum Class
        {
            Scout,
            Soldier,
            Pyro,
            Demoman,
            Heavy,
            Engineer,
            Medic,
            Sniper,
            Spy
        }

        public Class CurrentClass;

        public PlayerClass(Class c)
        {
            CurrentClass = c;
        }

    }
}
