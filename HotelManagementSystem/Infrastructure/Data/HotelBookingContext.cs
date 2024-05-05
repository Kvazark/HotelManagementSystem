using HotelManagementSystem.Aggregates;
using HotelManagementSystem.Domain.Aggregatеs;
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
        modelBuilder.Entity<Booking>().Property(r => r.Id).ValueGeneratedNever();

        modelBuilder.Entity<Hotel>().HasKey(r => r.Id);
        modelBuilder.Entity<Hotel>().ToTable(nameof(Hotel));
        modelBuilder.Entity<Hotel>().Property(r => r.Id).ValueGeneratedNever();
        
        modelBuilder.Entity<Room>().HasKey(p => p.Id);
        modelBuilder.Entity<Room>().ToTable(nameof(Room));
        modelBuilder.Entity<Room>().Property(r => r.Id).ValueGeneratedNever();
       
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
                    .IsRequired();
            }
        );

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

        // modelBuilder.Entity<Hotel>(entity =>
        // {
        //     entity
        //         .HasMany(hotel => hotel.Rooms)
        //         .WithOne(room => room.Hotel);
        // });
        //
        
        
        // modelBuilder.Entity<Room>(entity =>
        // {
        //     entity
        //         .HasMany(hotel => hotel.Bookings)
        //         .WithOne(booking => booking.Room);
        // });

        modelBuilder.Entity<Room>()
            .OwnsOne(
                r => r.NumberRoom,
                nr =>
                {
                    nr.Property(p => p.Value)
                        .HasColumnName(nameof(Room.NumberRoom))
                        .IsRequired();
                }
            );

        modelBuilder.Entity<Room>()
            .OwnsOne(
                r => r.RoomCategory,
                rc =>
                {
                    rc.Property(p => p.Value)
                        .HasColumnName(nameof(Room.RoomCategory))
                        .IsRequired();
                }
            );

        modelBuilder.Entity<Room>()
            .OwnsOne(
                r => r.Capacity,
                c =>
                {
                    c.Property(p => p.Value)
                        .HasColumnName(nameof(Room.Capacity))
                        .IsRequired();
                }
            );

        modelBuilder.Entity<Room>()
            .OwnsOne(
                r => r.RoomPrice,
                rp =>
                {
                    rp.Property(p => p.Value)
                        .HasColumnName(nameof(Room.RoomPrice))
                        .IsRequired();
                }
            );
    }
}
