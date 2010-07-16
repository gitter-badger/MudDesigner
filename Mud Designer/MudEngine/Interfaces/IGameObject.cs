using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudDesigner.Engine.Interfaces
{
    public interface IGameObject
    {
        string Name { get; set; }

        string Description { get; set; }

        string Script { get; set; }

        string Filename { get; }

        void OnCreate();
        void OnDestroy();
        void OnEnter();
        void OnExit();
        void OnEquip();
        void OnUnequip();
        void OnMount();
        void OnDismount();
    }
}
