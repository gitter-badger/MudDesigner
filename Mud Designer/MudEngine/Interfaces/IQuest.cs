using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudDesigner.MudEngine.Objects;
namespace MudDesigner.MudEngine.Interfaces
{
    interface IQuest
    {
        string Title { get; set; }
        string Description { get; set; }
        int ExperianceAward { get; set; }
        int MinimumLevel { get; set; }
        int MaximumLevel { get; set; }

        //This or int? i'm not sure
        Currency CurrencyAward { get; set; }
        void AcceptQuest();
        void CompleteQuest();
        void TrashQuest();
    }
}
