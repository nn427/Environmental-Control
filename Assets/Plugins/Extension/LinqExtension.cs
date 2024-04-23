using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LinqExtension {

    public static string Join<TItem>(this IEnumerable<TItem> enumerable, string separator) {
        return string.Join(separator, enumerable);
    }

}
