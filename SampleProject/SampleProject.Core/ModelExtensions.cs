using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Core
{
    public static class ModelExtensions
    {
        public static Tuple<IPagedList<T>, int> ToPagedList<T>(this IPagedList<T> list)
        {
            return new Tuple<IPagedList<T>, int>(list, list.TotalCount);
        }
    }
}
