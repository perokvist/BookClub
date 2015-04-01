using System;
using System.Collections.Generic;

namespace BookClub.Core
{
    public static class EnumerableExtansions
    {
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (T item in list)
            {
                action(item);
            }
        }
    }
}
