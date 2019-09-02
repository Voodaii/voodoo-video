using System.ComponentModel.DataAnnotations;

namespace Voodoo.Video.Models.Application
{
    public class Camera
    {
        public Camera(){}
        public Camera(string name, string description, string ipAddress, int? resW, int? resH)
        {
            Name = name;
            Description = description;
            IpAddress = ipAddress;
            ResWidth = resW;
            ResHeight = resH;
        }
        
        [Key] public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string IpAddress { get; set; }

        public int? ResWidth { get; set; }

        public int? ResHeight { get; set; }
    }
}