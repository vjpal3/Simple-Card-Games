using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonEntities
{
    class RandomExtensions
    {
        public static void Shuffle<T>(Random rand, List<T> list)
        {
            var n = list.Count;
            while (n > 1)
            {
                int k = rand.Next(n--);
                (list[k], list[n]) = (list[n], list[k]);

            }
        }
    }
}
