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

    public static class HtmlExtensions
    {
        public static MvcHtmlString TagListFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression)
            where TProperty : IEnumerable
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var value = metadata.Model as IEnumerable;

            var container = new TagBuilder("div");
            container.AddCssClass("tag-list-container");

            var textBox = new TagBuilder("input");
            textBox.Attributes.Add("type", "text");
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
                    label.InnerHtml = item.ToString();

                    var spanRemoveButton = new TagBuilder("span");
                    spanRemoveButton.AddCssClass("tag-list-item-remove");
                    spanRemoveButton.SetInnerText("X");
                    label.InnerHtml += spanRemoveButton.ToString();

                    listItem.InnerHtml = label.ToString();

                    var hiddenBox = new TagBuilder("input");
                    hiddenBox.Attributes.Add("type", "hidden");
                    hiddenBox.Attributes.Add("name", metadata.PropertyName +"["+ index +"]");
                    hiddenBox.Attributes.Add("value", item.ToString());
                    listItem.InnerHtml += hiddenBox.ToString(TagRenderMode.SelfClosing);

                    container.InnerHtml += listItem.ToString();

                    index++;
                }
            }

            return new MvcHtmlString(container.ToString());
        }
    }
}
