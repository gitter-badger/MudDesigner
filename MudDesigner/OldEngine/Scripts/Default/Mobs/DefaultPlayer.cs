using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Mobs;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Environment;
using MudDesigner.Scripts.Default.Mobs.Appearances;
using MudDesigner.Scripts.Default.Mobs.Stats;
using MudDesigner.Scripts.Default.Objects;

namespace MudDesigner.Scripts.Default.Mobs
{
    public class DefaultPlayer : BasePlayer
    {
        public DefaultPlayer()
        {
            AddAppearanceDescription(new Head("The head of the body", "The head is normal sized without any outstanding features."));
            AddAppearanceDescription(new Hair("The hair on the head.", "The hair is combed nicely, parted down the middle."));
            Health hp = new Health();
            Mana mp = new Mana();

            hp.Amount = 100;
            mp.Amount = 50;

            AddStat(hp);
            AddStat(mp);

            CanTalk = true;
            MaxInventorySize = 12;

            StarterBag bag = new StarterBag();
            Items.Add(bag);

            OnEnterEvent += new OnEnterHandler(OnEnterRoom);
        }

        public void OnEnterRoom(IRoom departingRoom)
        {
            SendMessage(Location.Description);
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
