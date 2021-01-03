using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace fms.Models
{
    public class FileCreate
    {
        [Required(ErrorMessage ="please choose a file")]
        public IFormFile FileToUpload { get; set; }
    }
}
