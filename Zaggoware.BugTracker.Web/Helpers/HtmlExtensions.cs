using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaggoware.BugTracker.Web.Helpers
{
    using System.Collections;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    using Zaggoware.BugTracker.Common.Tagging;

    public static class HtmlExtensions
    {
        public static bool IncludeTagger { get; private set; }

        public static MvcHtmlString TagListFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression)
            where TProperty : IEnumerable<TagListItem>
        {
            IncludeTagger = true;

            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var value = metadata.Model as IEnumerable<TagListItem>;

            var container = new TagBuilder("div");
            container.AddCssClass("tag-list-container");

            var textBox = new TagBuilder("input");
            textBox.Attributes.Add("type", "text");
            textBox.Attributes.Add("data-name", metadata.PropertyName);
            textBox.AddCssClass("form-control");
            textBox.GenerateId(metadata.PropertyName);
            container.InnerHtml = textBox.ToString(TagRenderMode.SelfClosing);

            var tagList = new TagBuilder("ul");
            tagList.AddCssClass("tag-list");

            if (value != null)
            {
                var index = 0;
                foreach (var item in value)
                {
                    var listItem = new TagBuilder("li");
                    listItem.AddCssClass("tag-list-item");

                    var label = new TagBuilder("span");
                    label.AddCssClass("tag-list-item-label");
                    label.SetInnerText(item.Label);

                    var spanRemoveButton = new TagBuilder("span");
                    spanRemoveButton.AddCssClass("tag-list-item-remove");
                    spanRemoveButton.SetInnerText("X");
                    label.InnerHtml += spanRemoveButton.ToString();

                    listItem.InnerHtml = label.ToString();

                    var hiddenBox = new TagBuilder("input");
                    hiddenBox.Attributes.Add("type", "hidden");
                    hiddenBox.Attributes.Add("name", metadata.PropertyName + "[" + index + "].Label");
                    hiddenBox.Attributes.Add("value", item.Label);
                    listItem.InnerHtml += hiddenBox.ToString(TagRenderMode.SelfClosing);

                    hiddenBox = new TagBuilder("input");
                    hiddenBox.Attributes.Add("type", "hidden");
                    hiddenBox.Attributes.Add("name", metadata.PropertyName +"["+ index +"].HiddenValue");
                    hiddenBox.Attributes.Add("value", item.HiddenValue.ToString());
                    listItem.InnerHtml += hiddenBox.ToString(TagRenderMode.SelfClosing);

                    tagList.InnerHtml += listItem.ToString();

                    index++;
                }
            }

            container.InnerHtml += tagList.ToString();

            return new MvcHtmlString(container.ToString());
        }
    }
}
