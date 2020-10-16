using System.Data.Entity;
using ChatService.Models.UserEntities;

namespace ChatService.Models.Context
{
    class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}