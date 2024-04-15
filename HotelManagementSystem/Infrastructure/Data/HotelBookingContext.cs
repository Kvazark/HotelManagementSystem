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

      
        modelBuilder.Entity<Booking>().Property(r => r.Id).ValueGeneratedNever()
            .HasConversion<Guid>(bookingId => bookingId.Value, dbId => BookingId.Of(dbId));

        modelBuilder.Entity<Hotel>().HasKey(r => r.Id);
        modelBuilder.Entity<Hotel>().ToTable(nameof(Booking));
        modelBuilder.Entity<Hotel>().Property(r => r.Id).ValueGeneratedNever();
        
        modelBuilder.Entity<Room>().HasKey(p => p.Id);
        modelBuilder.Entity<Room>().ToTable(nameof(Booking));
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
            x => x.HotelId,
            a =>
            {
                a.Property(p => p.Value)
                    .HasColumnName(nameof(Booking.HotelId))
                    .IsRequired();
            }
        );
        
        modelBuilder.Entity<Booking>().HasOne(b => b.HotelId)
            .WithMany()
            .HasForeignKey(b => b.HotelId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Booking>().HasOne(b => b.RoomId)
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
        modelBuilder.Entity<Hotel>() .HasMany(h => h.Rooms)
            .WithOne()
            .HasForeignKey(r => r.HotelId);
        
        
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

        modelBuilder.Entity<Room>()
            .Property(r => r.HotelId)
            .HasColumnName(nameof(Room.HotelId))
            .IsRequired();

        modelBuilder.Entity<Room>()
            .HasOne<Hotel>() 
            .WithMany(h => h.Rooms)
            .HasForeignKey(r => r.HotelId);
    }
}
