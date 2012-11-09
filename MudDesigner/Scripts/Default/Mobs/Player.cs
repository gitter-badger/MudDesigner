using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Mobs;
using MudDesigner.Engine.Core;

using MudDesigner.Scripts.Default.Mobs.Appearances;
using MudDesigner.Scripts.Default.Mobs.Stats;

namespace MudDesigner.Scripts.Mobs
{
    public class Player : BasePlayer
    {
        public Player()
        {
            AddAppearanceDescription(new Head());
            AddAppearanceDescription(new Hair());
            Health hp = new Health();
            hp.Amount = 100;

            AddStat(hp);

            CanTalk = true;
            MaxInventorySize = 12;
        }

        public override void Attack(IMob target)
        {
            //Stub
        }

        public override void Attack(IMob[] targets)
        {
            foreach (IMob mob in targets)
                Attack(mob);
        }

        public override void Damage(IGameObject dealer, int amount)
        {
            //Stub
        }

        public override void Heal(IGameObject dealer, int amount)
        {
            //Stub
        }

        public override void RestoreMana(IGameObject dealer, int amount)
        {
            //Stub
        }

        public override void ConsumeMana(int amount)
        {
            //Stub
        }
    }
}
