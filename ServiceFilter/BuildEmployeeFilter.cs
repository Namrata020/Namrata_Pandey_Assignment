using EmployeeManagementSystem.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EmployeeManagementSystem.ServiceFilter
{
    public class BuildEmployeeFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var param = context.ActionArguments.SingleOrDefault(p => p.Value is EmployeeFilterCriteria);
            if (param.Value == null)
            {
                context.Result = new BadRequestObjectResult("Object is null");
                return;
            }

            EmployeeFilterCriteria filterCriteria = (EmployeeFilterCriteria)param.Value;
            var statusFilter = filterCriteria.Filters.Find(e => e.FieldName == "status");
            if (statusFilter == null)
            {
                statusFilter = new FilterCriteria();
                statusFilter.FieldName = "status";
                statusFilter.FieldValue = "Active";
                filterCriteria.Filters.Add(statusFilter);
            }

            filterCriteria.Filters.RemoveAll(e => string.IsNullOrEmpty(e.FieldName));

            var result = await next();
        }
    }
}
