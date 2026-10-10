using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Sistem_Management_Hotel.Models;

public partial class HotelManagementDbContext : DbContext
{
    public HotelManagementDbContext()
    {
    }

    public HotelManagementDbContext(DbContextOptions<HotelManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Facility> Facilities { get; set; }

    public virtual DbSet<Guest> Guests { get; set; }

    public virtual DbSet<HousekeepingLog> HousekeepingLogs { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<PrItem> PrItems { get; set; }

    public virtual DbSet<PurchaseRequest> PurchaseRequests { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomBooking> RoomBookings { get; set; }

    public virtual DbSet<RoomBookingDetail> RoomBookingDetails { get; set; }

    public virtual DbSet<RoomType> RoomTypes { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=A416JAO-VIPS525;Database=hotel_management_db;User Id=hilmy;Password=Masuk123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Facility>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__faciliti__3213E83F833E1FFD");

            entity.ToTable("facilities");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Nama)
                .HasMaxLength(50)
                .HasColumnName("nama");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Guest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__guests__3213E83F715CF740");

            entity.ToTable("guests");

            entity.HasIndex(e => e.Email, "idx_guests_email");

            entity.HasIndex(e => e.Telepon, "idx_guests_telepon");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Alamat).HasColumnName("alamat");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Nama)
                .HasMaxLength(100)
                .HasColumnName("nama");
            entity.Property(e => e.NoIdentitas)
                .HasMaxLength(50)
                .HasColumnName("no_identitas");
            entity.Property(e => e.Telepon)
                .HasMaxLength(20)
                .HasColumnName("telepon");
        });

        modelBuilder.Entity<HousekeepingLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__housekee__3213E83FC548005D");

            entity.ToTable("housekeeping_logs");

            entity.HasIndex(e => e.CreatedAt, "idx_hk_created");

            entity.HasIndex(e => e.RoomId, "idx_hk_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Catatan).HasColumnName("catatan");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Room).WithMany(p => p.HousekeepingLogs)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_hk_room");

            entity.HasOne(d => d.User).WithMany(p => p.HousekeepingLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_hk_user");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__items__3213E83F4BA0066A");

            entity.ToTable("items", tb => tb.HasTrigger("trg_items_updated"));

            entity.HasIndex(e => e.Kode, "UQ__items__25A874926D239D10").IsUnique();

            entity.HasIndex(e => new { e.Stok, e.StokMin }, "idx_items_stok");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Aktif)
                .HasDefaultValueSql("((1))")
                .HasColumnName("aktif");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.HargaEstimasi)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("harga_estimasi");
            entity.Property(e => e.Kategori)
                .HasMaxLength(50)
                .HasColumnName("kategori");
            entity.Property(e => e.Kode)
                .HasMaxLength(30)
                .HasColumnName("kode");
            entity.Property(e => e.Nama)
                .HasMaxLength(100)
                .HasColumnName("nama");
            entity.Property(e => e.Satuan)
                .HasMaxLength(20)
                .HasColumnName("satuan");
            entity.Property(e => e.Stok)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("stok");
            entity.Property(e => e.StokMin)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("stok_min");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<PrItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__pr_items__3213E83F747CEE84");

            entity.ToTable("pr_items");

            entity.HasIndex(e => e.ItemId, "idx_pri_item");

            entity.HasIndex(e => e.PrId, "idx_pri_pr");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Catatan)
                .HasMaxLength(255)
                .HasColumnName("catatan");
            entity.Property(e => e.HargaEstimasi)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("harga_estimasi");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.PrId).HasColumnName("pr_id");
            entity.Property(e => e.Qty)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("qty");
            entity.Property(e => e.Subtotal)
                .HasColumnType("decimal(14, 2)")
                .HasColumnName("subtotal");

            entity.HasOne(d => d.Item).WithMany(p => p.PrItems)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pri_item");

            entity.HasOne(d => d.Pr).WithMany(p => p.PrItems)
                .HasForeignKey(d => d.PrId)
                .HasConstraintName("fk_pri_pr");
        });

        modelBuilder.Entity<PurchaseRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__purchase__3213E83F6BF0D3EC");

            entity.ToTable("purchase_requests", tb => tb.HasTrigger("trg_pr_updated"));

            entity.HasIndex(e => e.KodePr, "UQ__purchase__61007CAA7B81D1E2").IsUnique();

            entity.HasIndex(e => e.CreatedBy, "idx_pr_creator");

            entity.HasIndex(e => e.Status, "idx_pr_status");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Alasan).HasColumnName("alasan");
            entity.Property(e => e.ApprovedAt).HasColumnName("approved_at");
            entity.Property(e => e.ApprovedBy).HasColumnName("approved_by");
            entity.Property(e => e.CatatanApproval).HasColumnName("catatan_approval");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.KodePr)
                .HasMaxLength(30)
                .HasColumnName("kode_pr");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("('pending')")
                .HasColumnName("status");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.Tanggal)
                .HasColumnType("date")
                .HasColumnName("tanggal");
            entity.Property(e => e.TotalEstimasi)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(14, 2)")
                .HasColumnName("total_estimasi");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.PurchaseRequestApprovedByNavigations)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("fk_pr_approver");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PurchaseRequestCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pr_creator");

            entity.HasOne(d => d.Supplier).WithMany(p => p.PurchaseRequests)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("fk_pr_supplier");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__roles__3213E83F3A8DDFAE");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Nama, "UQ__roles__72E12F1FE901B00A").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Level)
                .HasDefaultValueSql("((1))")
                .HasColumnName("level");
            entity.Property(e => e.Nama)
                .HasMaxLength(50)
                .HasColumnName("nama");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__rooms__3213E83F93E53D57");

            entity.ToTable("rooms", tb => tb.HasTrigger("trg_rooms_updated"));

            entity.HasIndex(e => e.Nomor, "UQ__rooms__F76AFBB1A04961CD").IsUnique();

            entity.HasIndex(e => e.Status, "idx_rooms_status");

            entity.HasIndex(e => e.RoomTypeId, "idx_rooms_tipe");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Fasilitas).HasColumnName("fasilitas");
            entity.Property(e => e.Lantai).HasColumnName("lantai");
            entity.Property(e => e.Nomor)
                .HasMaxLength(10)
                .HasColumnName("nomor");
            entity.Property(e => e.RoomTypeId).HasColumnName("room_type_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("('tersedia')")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.RoomType).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.RoomTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_rooms_type");
        });

        modelBuilder.Entity<RoomBooking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__room_boo__3213E83F1B779E0C");

            entity.ToTable("room_bookings", tb => tb.HasTrigger("trg_rb_updated"));

            entity.HasIndex(e => e.KodeBooking, "UQ__room_boo__D4DD7E3811256E3D").IsUnique();

            entity.HasIndex(e => e.TglCheckin, "idx_rb_checkin");

            entity.HasIndex(e => e.TglCheckout, "idx_rb_checkout");

            entity.HasIndex(e => e.GuestId, "idx_rb_guest");

            entity.HasIndex(e => e.Status, "idx_rb_status");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.GuestId).HasColumnName("guest_id");
            entity.Property(e => e.JumlahKamar)
                .HasDefaultValueSql("((1))")
                .HasColumnName("jumlah_kamar");
            entity.Property(e => e.JumlahMalam).HasColumnName("jumlah_malam");
            entity.Property(e => e.Keterangan).HasColumnName("keterangan");
            entity.Property(e => e.KodeBooking)
                .HasMaxLength(30)
                .HasColumnName("kode_booking");
            entity.Property(e => e.MetodeBayar)
                .HasMaxLength(20)
                .HasColumnName("metode_bayar");
            entity.Property(e => e.RefNumber)
                .HasMaxLength(100)
                .HasColumnName("ref_number");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
            entity.Property(e => e.Sumber)
                .HasMaxLength(20)
                .HasColumnName("sumber");
            entity.Property(e => e.TglCheckin)
                .HasColumnType("date")
                .HasColumnName("tgl_checkin");
            entity.Property(e => e.TglCheckout)
                .HasColumnType("date")
                .HasColumnName("tgl_checkout");
            entity.Property(e => e.TotalBayar)
                .HasColumnType("decimal(14, 2)")
                .HasColumnName("total_bayar");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.RoomBookings)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("fk_rb_creator");

            entity.HasOne(d => d.Guest).WithMany(p => p.RoomBookings)
                .HasForeignKey(d => d.GuestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_rb_guest");
        });

        modelBuilder.Entity<RoomBookingDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__room_boo__3213E83F8943E962");

            entity.ToTable("room_booking_details");

            entity.HasIndex(e => e.BookingId, "idx_rbd_booking");

            entity.HasIndex(e => e.RoomId, "idx_rbd_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.Catatan)
                .HasMaxLength(255)
                .HasColumnName("catatan");
            entity.Property(e => e.RoomId).HasColumnName("room_id");

            entity.HasOne(d => d.Booking).WithMany(p => p.RoomBookingDetails)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("fk_rbd_booking");

            entity.HasOne(d => d.Room).WithMany(p => p.RoomBookingDetails)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_rbd_room");
        });

        modelBuilder.Entity<RoomType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__room_typ__3213E83F3255601A");

            entity.ToTable("room_types");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Deskripsi).HasColumnName("deskripsi");
            entity.Property(e => e.HargaDasar)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("harga_dasar");
            entity.Property(e => e.Kapasitas)
                .HasDefaultValueSql("((2))")
                .HasColumnName("kapasitas");
            entity.Property(e => e.Nama)
                .HasMaxLength(50)
                .HasColumnName("nama");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__supplier__3213E83FF565DD27");

            entity.ToTable("suppliers");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Aktif)
                .HasDefaultValueSql("((1))")
                .HasColumnName("aktif");
            entity.Property(e => e.Alamat).HasColumnName("alamat");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Kontak)
                .HasMaxLength(100)
                .HasColumnName("kontak");
            entity.Property(e => e.Nama)
                .HasMaxLength(100)
                .HasColumnName("nama");
            entity.Property(e => e.Telepon)
                .HasMaxLength(20)
                .HasColumnName("telepon");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83FA7B19ED7");

            entity.ToTable("users", tb => tb.HasTrigger("trg_users_updated"));

            entity.HasIndex(e => e.Username, "UQ__users__F3DBC57294E86674").IsUnique();

            entity.HasIndex(e => e.RoleId, "idx_users_role");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Aktif)
                .HasDefaultValueSql("((1))")
                .HasColumnName("aktif");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.LastLogin).HasColumnName("last_login");
            entity.Property(e => e.Nama)
                .HasMaxLength(100)
                .HasColumnName("nama");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Telepon)
                .HasMaxLength(20)
                .HasColumnName("telepon");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_users_role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
