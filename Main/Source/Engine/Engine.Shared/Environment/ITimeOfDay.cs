using System;
namespace Mud.Engine.Shared.Environment
{
    public interface ITimeOfDay
    {
        void DecrementByHour(int hours);
        void DecrementByMinute(int minutes);
        int Hour { get; set; }
        int HoursPerDay { get; set; }
        void IncrementByHour(int hours);
        void IncrementByMinute(int minutes);
        int Minute { get; set; }
    }
}
