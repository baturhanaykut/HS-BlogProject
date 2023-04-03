using HS_BlogProject.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS_BlogProject.Entities
{
    public class Author : IBaseEntity
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile UploadPath { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteTime { get; set; }
        public Status Status { get; set; }

        //Navigaiton Property
        public List<Post> Posts { get; set; }

        public Author()
        {
            Posts = new List<Post>();
        }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
    }
}
