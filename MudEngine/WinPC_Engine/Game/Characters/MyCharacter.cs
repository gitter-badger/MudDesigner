using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudEngine.Game;

namespace MudEngine.Game.Characters
{
    class MyCharacter : StandardCharacter
    {
        public int Age { get; set; }

        public MyCharacter(StandardGame game, String name, String desc)
            : base(game, name, desc)
        {
        }

        public override bool Save(string filename)
        {
            base.Save(filename, true);

            this.SaveData.AddSaveData("Age", Age.ToString());
            return this.SaveData.Save(filename);
        }
    }
}
