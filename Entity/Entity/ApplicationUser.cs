using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ApplicationUser:IdentityUser
    {
        public string Address { get; set; }
        public DateTime? BirthDate { get; set; }


    }
}
