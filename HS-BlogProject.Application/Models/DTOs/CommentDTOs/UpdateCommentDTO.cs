using HS_BlogProject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS_BlogProject.Application.Models.DTOs.CommentDTOs
{
    public class UpdateCommentDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? UpdateDate => DateTime.Now;
        public DateTime? DeleteTime { get; set; }
        public Status Status { get; set; }
    }
}
