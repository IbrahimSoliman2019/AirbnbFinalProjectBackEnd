using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;
using Domain.IdentityEntities;

namespace Infrastructure.EntitiesConfig
{
 
    public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasMany(x => x.transactions);
        }
    }
    public class TransactionConfig : IEntityTypeConfiguration<transaction>
    {


        public void Configure(EntityTypeBuilder<transaction> builder)
        {
            builder.HasOne(x => x.payee);
            builder.HasOne(x => x.Recevier);
        }
    }
    public class PropertyAmenitiesConfig : IEntityTypeConfiguration<property_amenities>
    {
        public void Configure(EntityTypeBuilder<property_amenities> builder)
        {
            builder.HasKey(x => new { x.amenity_id, x.property_id });

        }
    }
    public class BookingConfig : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasOne(x => x.transaction).WithOne(x => x.Booking);

        }
    }
    public class PropertyConfig : IEntityTypeConfiguration<property>
    {
        public void Configure(EntityTypeBuilder<property> builder)
        {

            builder.HasMany(x => x.property_reviews).WithOne(x => x.property);
        }
    }
}