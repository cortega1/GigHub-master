using GigHub.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace GigHub.Persistence.EntityConfigurations
{
    public class FollowConfiguration : EntityTypeConfiguration<Following>
    {
        public FollowConfiguration()
        {
            Property(f => f.FollowerId)
                .HasColumnOrder(1);

            Property(f => f.FolloweeId)
                .HasColumnOrder(2);
        }
    }
}