using HS_BlogProject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS_BlogProject.Insfrastructure.EntityTypeConfig
{
    internal class PostConfig : BaseEntityConfig<Post>
    {
        public override void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).IsRequired(true);

            builder.Property(x => x.Content).IsRequired(true);

            builder.Property(x => x.ImagePath).IsRequired(true);

            // 1 Authorun 1 den fazla Postu olabilir. (1'e Çok İlişki)
            builder.HasOne(x => x.Author)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);

            builder.HasOne(x => x.Genre)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

            //Genre ile bağlantısını yazıcaz.
            base.Configure(builder);
        }
    }
}
