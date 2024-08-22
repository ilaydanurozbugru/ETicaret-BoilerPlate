using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ETicaret.Storage.Dto
{
    public class UploadImage
    {
        [Required]
        [MaxLength(400)]
        public string FileToken { get; set; }

        public Guid? OldFile { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}
