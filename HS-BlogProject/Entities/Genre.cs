using HS_BlogProject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS_BlogProject.Entities
{
    public class Genre:IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteTime { get; set; }
        public Status Status { get; set; }


        public List<Post> Posts { get; set; }

        public Genre()
        {
            Posts = new List<Post>();
        }
    }
}
