using System.ComponentModel.DataAnnotations;

namespace TicketSystem_ByteMe.Models
{
  public class Project
  {
    [Key]
    public long ProjectID { get; set; }
    [Required(ErrorMessage = "this field is required")]
    [MaxLength(20, ErrorMessage = "max. 20 characters allowed")]
    [MinLength(5, ErrorMessage = "min. 5 characters required")]
    public string Title { get; set; }
    [Required(ErrorMessage = "this field is required")]
    [MinLength(3, ErrorMessage = "min. 3 characters required")]
    public string Description { get; set; }
    public DateTime Start { get; set; }
    public DateTime? End { get; set; }
  }
}
