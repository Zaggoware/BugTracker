using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaggoware.BugTracker.Common.Tagging
{
    using System.Collections;
    using System.Collections.ObjectModel;

    public class TagList : ICollection<TagListItem>
    {
        private readonly List<TagListItem> collection = new List<TagListItem>();

        public TagList()
        {
        }

        public TagList(IEnumerable<TagListItem> items)
            : this()
        {
            this.collection.AddRange(items);
        }

        public int Count
        {
            get
            {
                return this.collection.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public TagListItem this[int index]
        {
            get
            {
                return this.collection[index];
            }
            set
            {
                this.collection[index] = value;
            }
        }

        public IEnumerator<TagListItem> GetEnumerator()
        {
            return this.collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public TagListItem Add(string label, object hiddenValue)
        {
            var item = new TagListItem(label, hiddenValue);

            return this.Add(item);
        }

        public TagListItem Add(TagListItem item)
        {
            this.collection.Add(item);

            return item;
        }

        void ICollection<TagListItem>.Add(TagListItem item)
        {
            this.Add(item);
        }

        public void Clear()
        {
            this.collection.Clear();
        }

        public bool Contains(TagListItem item)
        {
            return this.collection.Contains(item);
        }

        public void CopyTo(TagListItem[] array, int arrayIndex)
        {
            this.collection.CopyTo(array, arrayIndex);
        }

        public bool Remove(TagListItem item)
        {
            return this.collection.Remove(item);
        }
    }
}
