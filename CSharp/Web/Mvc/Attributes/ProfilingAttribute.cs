using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using StackExchange.Profiling;

namespace Web.Mvc.Attributes
{
    public class ProfilingAttribute : ActionFilterAttribute
    {
        private const string STEP_KEY = "miniprofileractionstep";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var profiler = MiniProfiler.Current;
            var step = profiler.Step("Action: " + filterContext.ActionDescriptor.ActionName);

            filterContext.HttpContext.Items[STEP_KEY] = step;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var step = filterContext.HttpContext.Items[STEP_KEY] as IDisposable;
            if (step != null)
            {
                step.Dispose();
            }
        }
    }
}
