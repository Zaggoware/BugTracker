using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaggoware.BugTracker.Common.Paging
{
    using System.Collections;

    public interface IPagedList : IEnumerable
    {
        int NumberOfPages { get; }

        int ItemsPerPage { get; }

        int Page { get; }
    }

    public interface IPagedList<T> : IPagedList, IEnumerable<T> where T : class
    {
    }
}
