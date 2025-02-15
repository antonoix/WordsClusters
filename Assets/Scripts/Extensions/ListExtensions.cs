using System;
using System.Collections.Generic;

namespace Extensions
{
    public static class ListExtensions
    {
        public static void Shuffle<T>(this IList<T> list)  
        {  
            var rng = new Random();  
            int n = list.Count;  
            while (n > 1) 
            {  
                n--;  
                int k = rng.Next(n + 1);  
                T value = list[k];  
                list[k] = list[n];  
                list[n] = value;  
            }  
        }
    }
}
