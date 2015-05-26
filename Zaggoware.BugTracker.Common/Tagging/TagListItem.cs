using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaggoware.BugTracker.Common.Tagging
{
    public class TagListItem
    {
        public string Label
        {
            get
            {
                return this.label ?? (this.HiddenValue != null ? this.HiddenValue.ToString() : string.Empty);
            }
            set
            {
                label = value;
            }
        }

        public object HiddenValue { get; set; }

        private string label;

        public TagListItem()
        {
        }

        public TagListItem(object value)
        {
            this.HiddenValue = value;
            this.Label = value.ToString();
        }

        public TagListItem(string label, object hiddenValue)
            : this()
        {
            this.Label = label;
            this.HiddenValue = hiddenValue;
        }
    }
}
