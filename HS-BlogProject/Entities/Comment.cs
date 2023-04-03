using HS_BlogProject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS_BlogProject.Entities
{
    public class Comment: IBaseEntity 
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
        

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteTime { get; set; }
        public Status Status { get; set; }


        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
