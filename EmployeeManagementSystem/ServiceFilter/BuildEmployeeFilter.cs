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
            //BASIC
            var basicParam = context.ActionArguments.SingleOrDefault(p => p.Value is BasicEmployeeFilterCriteria);
            if (basicParam.Value == null)
            {
                context.Result = new BadRequestObjectResult("Object is null");
                return;
            }

            BasicEmployeeFilterCriteria basicFilterCriteria = (BasicEmployeeFilterCriteria)basicParam.Value;
            var basicStatusFilter = basicFilterCriteria.Filters.Find(e => e.FieldName == "status");
            if (basicStatusFilter == null)
            {
                basicStatusFilter = new FilterCriteria();
                basicStatusFilter.FieldName = "status";
                basicStatusFilter.FieldValue = "Active";
                basicFilterCriteria.Filters.Add(basicStatusFilter);
            }

            basicFilterCriteria.Filters.RemoveAll(e => string.IsNullOrEmpty(e.FieldName));

            //ADDITIONAL
            var additionalParam = context.ActionArguments.SingleOrDefault(p => p.Value is AdditionalEmployeeFilterCriteria);
            if (additionalParam.Value == null)
            {
                context.Result = new BadRequestObjectResult("Object is null");
                return;
            }

            AdditionalEmployeeFilterCriteria additionalFilterCriteria = (AdditionalEmployeeFilterCriteria)additionalParam.Value;
            var additionalStatusFilter = additionalFilterCriteria.Filters.Find(e => e.FieldName == "status");
            if (additionalStatusFilter == null)
            {
                additionalStatusFilter = new FilterCriteria();
                additionalStatusFilter.FieldName = "status";
                additionalStatusFilter.FieldValue = "Active";
                additionalFilterCriteria.Filters.Add(additionalStatusFilter);
            }

            additionalFilterCriteria.Filters.RemoveAll(e => string.IsNullOrEmpty(e.FieldName));

            var result = await next();
            
        }

    }
}
