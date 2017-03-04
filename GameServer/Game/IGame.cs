using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    interface IGame
    {
        bool IsTurn(string name);
        RequestObject Move(object message);
        bool IsOver();
        string Result { get; set; }
    }
}
