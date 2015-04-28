using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaggoware.BugTracker.Common.Paging
{
    public static class PagingHelper
    {
        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int page, int itemsPerPage)
            where T : class
        {
            return new PagedList<T>(source, page, itemsPerPage);
        }
    }
}
