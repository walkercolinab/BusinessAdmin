using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace System.Web.Mvc.Html
{
    public static class Validator
    {
        public static MvcHtmlString CustomValidationMessageFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {

            TagBuilder labelBuilder = new TagBuilder("span");
            labelBuilder.AddCssClass("fa fa-times-circle-o");

            labelBuilder.InnerHtml += helper.ValidationMessageFor(expression).ToString();

            return MvcHtmlString.Create(labelBuilder.ToString(TagRenderMode.Normal));
        }
    }
}