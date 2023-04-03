using HS_BlogProject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS_BlogProject.Application.Models.DTOs.CommentDTOs
{
    public class CreateCommentDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreateDate => DateTime.Now;

        public Status Status { get; set; }


    }
}
