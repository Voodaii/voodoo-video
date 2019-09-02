using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Voodoo.Video.Data;
using Voodoo.Video.Models.Application;
using Voodoo.Video.Models.Identity;
using Voodoo.Video.Models.ViewModels.CameraManagerViewModels;

namespace Voodoo.Video.Controllers
{
    public class CameraController : Controller
    {
        private readonly ILogger<MainController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public CameraController(
            ILogger<MainController> logger,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult CameraManager()
        {
//            var model = new CameraManagerViewModel
//            {
//                Cameras = _context.Cameras.ToList(),
//                NewCamera = new Camera()
//            };
            
            return PartialView("~/Views/Main/CameraManager.cshtml");
        }
        
        public string CameraList()
        {
            return JsonConvert.SerializeObject(_context.Cameras.ToList());
        }
    }
}