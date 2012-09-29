using System;
using System.Collections.Generic;
using System.Collections;

using WinPC
namespace WinPC.Engine.Abstract.Core
{
    public interface IRoom
    {
        public IZone Zone { get; private set; }
        public bool Safe { get; set; }
        public Dictionary<string, IPlayer> Occupants { get; private set; }


    }
}