using Microsoft.EntityFrameworkCore;

namespace MessageBoard.Models
{
  public class MessageBoardContext : DbContext
  {
    public DbSet<Message> Messages { get; set; }
    public DbSet<Group> Groups { get; set; }

    public MessageBoardContext(DbContextOptions<MessageBoardContext> options) : base(options)
    {

    }
  }
}