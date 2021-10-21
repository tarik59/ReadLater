using Entity;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ITokenService
    {
        public string CreateToken(ApplicationUserDto user);
    }
}
