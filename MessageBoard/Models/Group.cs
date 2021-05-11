using System.ComponentModel.DataAnnotations;

namespace MessageBoard.Models
{
  public class Group
  {
    public int GroupId { get; set; }
    [Required]
    [StringLength(32)]
    public string Name { get; set; }
  }
}