using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaggoware.BugTracker.Common.Paging
{
    public abstract class PagedList: IPagedList
    {
        public const int DefaultItemsPerPage = 25;

        public int Page { get; protected set; }

        public int ItemsPerPage { get; protected set; }

        public abstract int NumberOfPages { get; }

        protected IEnumerable Soure { get; set; }

        protected PagedList(IEnumerable source, int page, int itemsPerPage)
        {
            this.Soure = source;
            this.Page = page;
            this.ItemsPerPage = itemsPerPage;
        }

        public IEnumerator GetEnumerator()
        {
            return this.Soure.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public class PagedList<T> : PagedList, IPagedList<T> where T : class
    {
        private readonly IEnumerable<T> typedSource;

        private readonly IEnumerable<T> pagedSource;

        public PagedList(IEnumerable<T> source, int pageNumber, int itemsPerPage)
            : base(source, pageNumber, itemsPerPage)
        {
            this.typedSource = source;
            this.pagedSource = source.Skip(pageNumber).Take(itemsPerPage);
        }

        public override int NumberOfPages
        {
            get
            {
                return (int)(Math.Ceiling(this.typedSource.Count() / (double)this.ItemsPerPage));
            }
        }

        public new IEnumerator<T> GetEnumerator()
        {
            return this.pagedSource.GetEnumerator();
        }
    }
}
