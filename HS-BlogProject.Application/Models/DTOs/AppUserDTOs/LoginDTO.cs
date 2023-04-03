using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS_BlogProject.Application.Models.DTOs.AppUserDTOs
{
    public class LoginDTO
    {
        //ToDo : DataAnnotations ekeleyeceğiz. 
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
