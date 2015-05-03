using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Zaggoware.BugTracker.Web.Helpers
{
    public static class ModelStateHelper
    {
        public static void AddModelError<TModel>(
            this ModelStateDictionary modelState,
            Expression<Func<TModel, object>>  propertyExpression,
            string errorMessage)
        {
            var propertyName = ExpressionHelper.GetExpressionText(propertyExpression);

            modelState.AddModelError(propertyName, errorMessage);
        }
    }
}
