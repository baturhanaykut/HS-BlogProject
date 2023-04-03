using HS_BlogProject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS_BlogProject.Application.Models.DTOs.AppUserDTOs
{
    public class RegisterDTO
    {
        //ToDo : DataAnnotations ekeleyeceğiz. 
        public string UserName { get; set; }

        public string Password { get; set; }

        public string ComfirmPassword { get; set; }

        public string Email { get; set; }

        public DateTime CreateDate => DateTime.Now;

        public Status status => Status.Active;
    }
}
