using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WebGL_Playground_Site {
    public static class EnumerableExtensions {
        public static IEnumerable<T> Subsequence<T>(this IEnumerable<T> arr, int startIndex, int count) {
            return arr.Skip(startIndex).Take(count);
        }
    }
}