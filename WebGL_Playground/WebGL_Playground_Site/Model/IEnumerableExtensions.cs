using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace WebGL_Playground_Site {
    public static class EnumerableExtensions {
        public static IEnumerable<TSource> Subsequence<TSource>(this IEnumerable<TSource> arr, int startIndex, int count) {
            return arr.Skip(startIndex).Take(count);
        }

        public static IEnumerable<TResult> SelectTwo<TSource, TResult>(this IEnumerable<TSource> arr, [NotNull] Func<TSource, TSource, TResult> selector) {
            using var en = arr.GetEnumerator();
            while (en.MoveNext()) {
                var first = en.Current;
                en.MoveNext();
                yield return selector(first, en.Current);
            }
        }
    }
}