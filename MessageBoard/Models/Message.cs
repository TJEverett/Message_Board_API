using System;
using System.ComponentModel.DataAnnotations;

namespace MessageBoard.Models
{
  public class Message
  {
    public int MessageId { get; set; }
    [Required]
    [StringLength(32)]
    public string UserName { get; set; }
    [Required]
    [StringLength(256)]
    public string Content { get; set; }
    public DateTime Date { get; set; }
  }
}