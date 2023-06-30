using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TicketSystem_ByteMe.Models
{
  public class Ticket
  {
    [Key]
    public int TicketID { get; set; }
    [Required(ErrorMessage = "this field is required")]
    [MaxLength(20, ErrorMessage = "max. 20 characters allowed")]
    [MinLength(5, ErrorMessage = "min. 5 characters required")]
    public string Headline { get; set; }
    [Required(ErrorMessage = "this field is required")]
    [MaxLength(200, ErrorMessage = "max. 200 characters allowed")]
    [MinLength(3, ErrorMessage = "min. 3 characters required")]
    public string Description { get; set; }
    [Required(ErrorMessage = "this field is required")]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Project Project { get; set; }
    public DateTime CreatedAt { get; set; }

    [Required(ErrorMessage = "this field is required")]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Employee CreatedBy { get; set; }

    [Required(ErrorMessage = "this field is required")]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Employee AssignedTo { get; set; }

    public DateTime? SolvedAt { get; set; }
  }
}
