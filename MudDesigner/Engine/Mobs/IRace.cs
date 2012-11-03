using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudDesigner.Engine.Mobs
{
    public interface IRace
    {
        string Name { get; set; }
        Dictionary<string, IStat> Stats { get; set; }

        List<IGender> GenderRestrictions { get; set; }
        Dictionary<string, IFaction> RacialFactions { get; set; }

        Dictionary<string, IAppearanceAttribute> Appearance { get; set; }

        void AddAppearanceItem(IAppearanceAttribute appearanceItem);
        void AddFaction(IFaction faction);
        void AddStat(IStat stat);
        IStat[] GetStats();
        IFaction[] GetFactions();
    }
}
