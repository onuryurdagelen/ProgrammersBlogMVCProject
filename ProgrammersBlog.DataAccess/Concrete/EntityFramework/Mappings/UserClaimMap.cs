using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgrammersBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.DataAccess.Concrete.EntityFramework.Mappings
{
    //User talebi
    public class UserClaimMap : IEntityTypeConfiguration<UserClaim>
    {
        // Primary key
        public void Configure(EntityTypeBuilder<UserClaim> builder)
        {
            builder.HasKey(rc => rc.Id);

            // Maps to the AspNetRoleClaims table
            builder.ToTable("ProgrammersBlogUserClaims");
        }
    }
}
