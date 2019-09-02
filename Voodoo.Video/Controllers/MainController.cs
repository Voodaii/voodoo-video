using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Voodoo.Video.Data;
using Voodoo.Video.Infrastructure;
using Voodoo.Video.Infrastructure.ErrorHandling;
using Voodoo.Video.Models;
using Voodoo.Video.Models.Application;
using Voodoo.Video.Models.Identity;
using Voodoo.Video.Models.ViewModels.CameraLayoutsViewModels;

namespace Voodoo.Video.Controllers
{
    [Authorize]
    public class MainController : BaseController
    {
        private readonly ILogger<MainController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        [TempData]
        public string StatusMessage { get; set; }

        public MainController(
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

        [HttpGet("/")]
        public IActionResult Index()
        {
            var layout = _context.CameraLayouts
                .Include(l => l.CameraTiles)
                .ThenInclude(ct => ct.Camera)
                .FirstOrDefault();
            
            return View("DashboardLayout", new LayoutViewModel { CameraLayout = layout });
        }
        
        
        //! Dashboard Layouts
        //!! Get layout list for sidebar links.
        [HttpGet("/get-dashboard-layouts")]
        public IActionResult GetDashboardLayouts()
        {
            var layouts = _context.CameraLayouts.ToList();
            return PartialView("Sidebar/_DashboardLayoutListPartial", layouts);
        }
        
        //!! Get the requested layout.
        [HttpGet("/get-dashboard-layout")]
        public IActionResult GetDashboardLayout(int id)
        {
            var layout = _context.CameraLayouts
                .Include(cl => cl.CameraTiles)
                .ThenInclude(ct => ct.Camera)
                .SingleOrDefault(cl => cl.Id == id);
            var model = new LayoutViewModel
            {
                CameraLayout = layout
            };
            
            return PartialView("DashboardLayout", model);
        }
        
        //!! Get the form for making a new layout.
        [ImportModelState]
        [HttpGet("/new-layout-form")]
        public IActionResult NewLayoutForm()
        {
            return PartialView("NewDashboardLayout");
        }
        
        //!! Create a new layout.
        [ExportModelState]
        [HttpPost("/create-new-layout")]
        public IActionResult CreateNewLayout([FromForm] NewLayoutViewModel input)
        {
            if (string.IsNullOrEmpty(input.Name))
            {
                return BadRequest("Name cannot be empty.");
            }   
            
            var layout = new CameraLayout
            {
                Name = input.Name
            };
            
            _context.Add(layout);
            _context.SaveChanges();
            
            layout.CameraTiles = new List<CameraTile>();
            var model = new LayoutViewModel
            {
                CameraLayout = layout
            };
            
            return PartialView("DashboardLayout", model);
        }

        
        
        
        
        

        
        //! USER PROFILE PAGES.
        [ImportModelState]
        [HttpGet("/profile")]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return View(new ProfileViewModel
            {
                Email = user.Email,
                FullName = user.FullName
            });
        }

        [ExportModelState]
        [HttpPost("/profile")]
        public async Task<IActionResult> UpdateProfile(
            [FromForm]
            ProfileViewModel input)
        {
            if (!ModelState.IsValid)
            { 
                return RedirectToAction(nameof(Profile));
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var email = await _userManager.GetEmailAsync(user);
            if (input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, input.Email);
                if (!setEmailResult.Succeeded)
                {
                    foreach (var error in setEmailResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            // Model state might not be valid anymore if we weren't able to change the e-mail address
            // so we need to check for that before proceeding
            if (!ModelState.IsValid) return RedirectToAction(nameof(Profile));
            
            if (input.FullName != user.FullName)
            {
                // If we receive an empty string, set a null full name instead
                user.FullName = string.IsNullOrWhiteSpace(input.FullName) ? null : input.FullName;
            }

            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);

            StatusMessage = "Your profile has been updated";

            return RedirectToAction(nameof(Profile));
        }
        
        //! USER LOG OUT.
        [HttpPost("/logout")]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }
        
        //! PRIVACY POLICY.
        [HttpGet("/privacy")]
        public IActionResult Privacy()
        {
            return View();
        }
        
        //! ERROR PAGES.
        [HttpGet("/error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("/status-code")]
        public IActionResult StatusCodeHandler(int code)
        {
            ViewBag.StatusCode = code;
            ViewBag.StatusCodeDescription = ReasonPhrases.GetReasonPhrase(code);
            ViewBag.OriginalUrl = null;


            var statusCodeReExecuteFeature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            if (statusCodeReExecuteFeature != null)
            {
                ViewBag.OriginalUrl =
                    statusCodeReExecuteFeature.OriginalPathBase
                    + statusCodeReExecuteFeature.OriginalPath
                    + statusCodeReExecuteFeature.OriginalQueryString;
            }

            return View(code == 404 ? "Status404" : "Status4xx");
        }
    }
}
