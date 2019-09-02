using Microsoft.AspNetCore.Mvc.Filters;

namespace Voodoo.Video.Infrastructure.ErrorHandling
{
    public abstract class ModelStateTransfer : ActionFilterAttribute
    {
        protected const string Key = nameof(ModelStateTransfer);
    }
}