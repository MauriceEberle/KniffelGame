using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Kniffel.ViewModels.Extensions
{
    public static class ObservableCollectionExtensions
    {
        public static ObservableCollection<T> ToObservable<T>(this IEnumerable<T> source)
        {
            var collection = new ObservableCollection<T>();
            foreach (var item in source)
            {
                collection.Add(item);
            }
            return collection;
        }
    }
}
