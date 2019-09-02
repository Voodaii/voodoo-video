using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Voodoo.Video.Models.Application
{
    public class CameraLayout
    {
        [Key] public int Id {get;set;}
        public string Name {get;set;}
        
        //Relationships
        [ForeignKey("CameraLayoutId")]
        public List<CameraTile> CameraTiles {get;set;}
    }
}