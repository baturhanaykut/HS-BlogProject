using HS_BlogProject.Entities;
using HS_BlogProject.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS_BlogProject.Application.Models.VMs.AuthorVMs
{
    public class AuthorVM
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }
        
        public string ImagePath { get; set; }

        public List<Post> Posts { get; set; }


    }
}
