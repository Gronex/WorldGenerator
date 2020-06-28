using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGenerator.Core
{
    public static class ArrayExtensions
    {
        public static T GetWrapedValue<T>(this T[] array, int index)
        {
            while(array.Length <= index)
            {
                index -= array.Length;
            }

            while(index < 0)
            {
                index += array.Length;
            }
            return array[index];
        }
    }
}
