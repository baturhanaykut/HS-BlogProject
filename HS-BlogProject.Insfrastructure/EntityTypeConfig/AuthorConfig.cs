﻿using HS_BlogProject.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS_BlogProject.Insfrastructure.EntityTypeConfig
{
    internal class AuthorConfig : BaseEntityConfig<Author>
    {
        public override void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName).IsRequired(true);

            builder.Property(x => x.LastName).IsRequired(true);

            builder.Property(x => x.ImagePath).IsRequired(false);

            base.Configure(builder);
        }
    }
}
