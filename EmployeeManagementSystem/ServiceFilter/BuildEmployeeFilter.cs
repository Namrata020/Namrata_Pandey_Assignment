using EmployeeManagementSystem.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using EmployeeManagementSystem.Entities;

namespace EmployeeManagementSystem.ServiceFilter
{
    public class BuildEmployeeFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var basicParam = context.ActionArguments.SingleOrDefault(p => p.Value is BasicEmployeeFilterCriteria);
            if (basicParam.Value != null)
            {
                BasicEmployeeFilterCriteria basicFilterCriteria = (BasicEmployeeFilterCriteria)basicParam.Value;
                AddStatusFilterIfNeeded(basicFilterCriteria.Filters);
                basicFilterCriteria.Filters.RemoveAll(e => string.IsNullOrEmpty(e.FieldName));
            }

            var additionalParam = context.ActionArguments.SingleOrDefault(p => p.Value is AdditionalEmployeeFilterCriteria);
            if (additionalParam.Value != null)
            {
                AdditionalEmployeeFilterCriteria additionalFilterCriteria = (AdditionalEmployeeFilterCriteria)additionalParam.Value;
                AddStatusFilterIfNeeded(additionalFilterCriteria.Filters);
                additionalFilterCriteria.Filters.RemoveAll(e => string.IsNullOrEmpty(e.FieldName));
            }

            await next();
        }

        private void AddStatusFilterIfNeeded(IList<FilterCriteria> filters)
        {
            var statusFilter = filters.FirstOrDefault(e => e.FieldName == "status");
            if (statusFilter == null)
            {
                statusFilter = new FilterCriteria
                {
                    FieldName = "status",
                    FieldValue = "Active"
                };
                filters.Add(statusFilter);
            }
        }
    }
}
