using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Entity
{
    public class ChuDe
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string TenChuDe { get; set; }

        public string MoTa { get; set; }
        public string? ImageUrl { get; set; }
        public bool isDeleted { get; set; } = false;
        [ValidateNever]
        public ICollection<CauHoi> CauHois { get; set; }


    }
}
