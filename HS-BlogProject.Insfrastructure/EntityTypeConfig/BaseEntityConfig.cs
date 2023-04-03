﻿using HS_BlogProject.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS_BlogProject.Insfrastructure.EntityTypeConfig
{
    internal abstract class BaseEntityConfig<T> : IEntityTypeConfiguration<T> where T : class, IBaseEntity
    {
        public virtual void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.CreateDate).IsRequired(true);
            builder.Property(x => x.DeleteTime).IsRequired(false);
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.Status).IsRequired(true);


        }
    }
}
