using Microsoft.EntityFrameworkCore;
using NotificationSystem.Models;

namespace NotificationSystem.Data;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options) : base(options) {}

    public DbSet<Notification> Notifications { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserNotification> UserNotifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Notification>().ToTable("Notifications");
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<UserNotification>().ToTable("UserNotifications");
    }
}