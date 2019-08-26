using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Models
{
    public class ApplicationUser: IdentityUser 
    {
        [Column(TypeName ="nvarchar(150)")]
        public string Name { get; set; }
        
    }
}
