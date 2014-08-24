using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Engine
{
    public static class ListExtensions
    {/// <summary>
        /// return a random element of the list or default if list is empty
        /// </summary>
        /// <param name="e"></param>
        /// <param name="weightSelector">
        /// return chances to be picked for the element. A weigh of 0 or less means 0 chance to be picked.
        /// If all elements have weight of 0 or less they all have equal chances to be picked.
        /// </param>
        /// <returns></returns>
        public static T AnyOrDefault<T>(this ICollection<T> e, Func<T, double> weightSelector)
        {
            if (e.Count < 1)
            {
                return default(T);
            }
            else if (e.Count == 1)
            {
                return e.ElementAtOrDefault(0);
            }

            var weights = e.Select(item => Math.Max(weightSelector(item), 0)).ToArray();
            var sum = weights.Sum(d => d);

            var rnd = new Random().NextDouble();
            for (int i = 0; i < weights.Length; i++)
            {
                //Normalize weight
                var w = sum == 0
                    ? 1 / (double)e.Count
                    : weights.ElementAtOrDefault(i) / sum;
                if (rnd < w)
                    return e.ElementAtOrDefault(i);
                rnd -= w;
            }

            throw new Exception("Should not happen");
        }
    }
}
