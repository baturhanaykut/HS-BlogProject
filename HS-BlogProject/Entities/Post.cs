using HS_BlogProject.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS_BlogProject.Entities
{
    public class Post : IBaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile UploadPath { get; set; }  // Resim yüklemek için kullanılıyor

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteTime { get; set; }
        public Status Status { get; set; }


        public int AuthorId { get; set; }

        public int GenreId { get; set; }

        //ToDo : Author, Genre, Commets, Likes

        public Author Author { get; set; }

        public Genre Genre { get; set; }

        public List<Like> Likes { get; set; }

        public List<Comment> Comments { get; set; }

        public Post()
        {
            Comments = new List<Comment>();
            Likes = new List<Like> ();
            
        }
    }
}
