using System.ComponentModel.DataAnnotations;

namespace Voodoo.Video.Models.Application
{
    public class CameraTile
    {
        [Key] public int Id { get; set; }
        
        //Foreign Key
        public int CameraId { get; set; }
        
        //Foreign Key
        public int CameraLayoutId {get; set;}
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public string HeaderColor {get;set;}
        
        public int X {get;set;}
        
        public int Y {get;set;}
        
        public int Height {get;set;}
        
        public int Width {get;set;}
        
        //Relationships
        public Camera Camera {get;set;}
        public CameraLayout CameraLayout {get;set;}
    }
}