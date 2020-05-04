using Microsoft.EntityFrameworkCore;

namespace OnlineChatRoom.DataAccess.Models
{
    public partial class ChatContext
    {
        private readonly string _connectionString;

        public ChatContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
