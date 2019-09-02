using System.Collections.Generic;
using Voodoo.Video.Models.Application;

namespace Voodoo.Video.Models.ViewModels.CameraManagerViewModels
{
    public class CameraManagerViewModel
    {
        public List<Camera> Cameras {get;set;}
        
        public Camera NewCamera {get;set;}
    }
}