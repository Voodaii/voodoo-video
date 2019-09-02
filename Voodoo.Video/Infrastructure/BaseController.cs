using Microsoft.AspNetCore.Mvc;

namespace Voodoo.Video.Infrastructure
{
    [Route("[controller]/[action]", Name = "[controller]_[action]")]
    public abstract class BaseController : Controller
    {
    }
}
