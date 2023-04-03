using HS_BlogProject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS_BlogProject.Entities
{
    public interface IBaseEntity
    {
        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public DateTime? DeleteTime { get; set; }

        public Status Status { get; set; }
    }
}
