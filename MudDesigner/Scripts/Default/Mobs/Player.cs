using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Mobs;
namespace MudDesigner.Scripts.Mobs
{
    public class Player : BasePlayer
    {
        public override void Attack(IMob target)
        {
            throw new NotImplementedException();
        }

        public override void Attack(IMob[] targets)
        {
            throw new NotImplementedException();
        }

        public override void Damage(Engine.Core.IGameObject dealer, int amount)
        {
            throw new NotImplementedException();
        }

        public override void Heal(Engine.Core.IGameObject dealer, int amount)
        {
            throw new NotImplementedException();
        }

        public override void RestoreMana(Engine.Core.IGameObject dealer, int amount)
        {
            throw new NotImplementedException();
        }

        public override void ConsumeMana(int amount)
        {
            throw new NotImplementedException();
        }
    }
}
