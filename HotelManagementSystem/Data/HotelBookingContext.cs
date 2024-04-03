using HotelManagementSystem.Aggregates;
using Microsoft.EntityFrameworkCore;


namespace HotelManagementSystem.Data;

public class HotelBookingContext  : DbContext
{
    public DbSet<Booking> Bookings { get; set; }
    
    public DbSet<Hotel> Hotels { get; set; }
    
    public DbSet<Room> Rooms { get; set; }

    public HotelBookingContext(DbContextOptions<HotelBookingContext> options) : base(options)
    {
    }

    public HotelBookingContext()
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Booking>().HasKey(p => p.Id);
        
        modelBuilder.Entity<Booking>().ToTable(nameof(Booking));

        modelBuilder.Entity<Booking>().HasKey(r => r.Id);
        modelBuilder.Entity<Booking>().Property(r => r.Id).ValueGeneratedNever()
            .HasConversion<Guid>(bookingId => bookingId.Value, dbId => BookingId.Of(dbId));
       
        modelBuilder.Entity<Booking>().OwnsOne(x => x.ReservationDates,
            a =>
            {
                a.Property(p => p.ArrivalDate)
                    .HasColumnName(nameof(Booking.ReservationDates.ArrivalDate))
                    .HasMaxLength(50)
                    .IsRequired();
            });
        modelBuilder.Entity<Booking>().OwnsOne(x => x.ReservationDates,
            a =>
            {
                a.Property(p => p.DepartureDate)
                    .HasColumnName(nameof(Booking.ReservationDates.DepartureDate))
                    .HasMaxLength(50)
                    .IsRequired();
            });

        modelBuilder.Entity<Booking>().OwnsOne(
            x => x.NumberOfGuests,
            a =>
            {
                a.Property(p => p.Value)
                    .HasColumnName(nameof(Booking.NumberOfGuests))
                    .HasMaxLength(50)
                    .IsRequired();
            }
        );
        
        modelBuilder.Entity<Booking>().HasOne(b => b.Hotel)
            .WithMany()
            .HasForeignKey(b => b.HotelId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Booking>().HasOne(b => b.Room)
            .WithMany()
            .HasForeignKey(b => b.RoomId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Booking>().OwnsOne(
            x => x.Discount,
            a =>
            {
                a.Property(p => p.Value)
                    .HasColumnName(nameof(Booking.Discount))
                    .IsRequired();
            }
        );
        modelBuilder.Entity<Booking>().OwnsOne(
            x => x.BookingPrice,
            a =>
            {
                a.Property(p => p.Value)
                    .HasColumnName(nameof(Booking.BookingPrice))
                    .IsRequired();
            }
        );

        modelBuilder.Entity<Hotel>().HasKey(p => p.Id);
        
        modelBuilder.Entity<Hotel>().ToTable(nameof(Hotel));

        modelBuilder.Entity<Hotel>().HasKey(r => r.Id);
        modelBuilder.Entity<Hotel>().Property(r => r.Id).ValueGeneratedNever()
            .HasConversion<Guid>(hotelId => hotelId.Value, dbId => HotelId.Of(dbId));
       
        modelBuilder.Entity<Hotel>().OwnsOne(
            x => x.Name,
            a =>
            {
                a.Property(p => p.Value)
                    .HasColumnName(nameof(Hotel.Name))
                    .HasMaxLength(50)
                    .IsRequired();
            }
        );

        modelBuilder.Entity<Hotel>().OwnsOne(
            x => x.Address,
            a =>
            {
                a.Property(p => p.Value)
                    .HasColumnName(nameof(Hotel.Address))
                    .HasMaxLength(250)
                    .IsRequired();
            }
        );

        modelBuilder.Entity<Hotel>().OwnsOne(
            x => x.HotelStarRating,
            a =>
            {
                a.Property(p => p.Value)
                    .HasColumnName(nameof(Hotel.HotelStarRating))
                    .IsRequired();
            }
        );
        // modelBuilder.Entity<Hotel>().OwnsOne(
        //     x => x.Rooms,
        //     a =>
        //     {
        //         a.Property(p => p.Value)
        //             .HasColumnName(nameof(Hotel.Rooms))
        //             .IsRequired();
        //     }
        // );
    }
}
// protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         base.OnModelCreating(modelBuilder);
//
//         modelBuilder.Entity<User>().HasKey(k => k.Id);
//
//         modelBuilder.Entity<User>().ToTable(nameof(User));
//
//         modelBuilder.Entity<User>().Property(r => r.Id).ValueGeneratedNever();
//         modelBuilder.Entity<Publication>().Property(r => r.Id).ValueGeneratedNever();
//
//         modelBuilder.Entity<User>().OwnsOne(x => x.UserName,
//             a =>
//             {
//                 a.Property(p => p.FirstName)
//                     .HasColumnName(nameof(User.UserName.FirstName))
//                     .HasMaxLength(50)
//                     .IsRequired();
//             });
//
//         modelBuilder.Entity<User>().OwnsOne(x => x.UserName,
//             a =>
//             {
//                 a.Property(p => p.LastName)
//                     .HasColumnName(nameof(User.UserName.LastName))
//                     .HasMaxLength(50)
//                     .IsRequired();
//             });
//
//         modelBuilder.Entity<User>().OwnsOne(x => x.Birthday,
//             a =>
//             {
//                 a.Property(p => p.Date)
//                     .HasColumnName(nameof(User.Birthday))
//                     .IsRequired();
//             });
//
//         modelBuilder.Entity<User>()
//             .HasMany(user => user.Publications)
//             .WithOne(publication => publication.User);
//
//         modelBuilder.Entity<UsersFriends>(entity =>
//         {
//             entity.HasKey(source => new { source.UserId, source.UsersFriendId });
//         });
//
//     }
