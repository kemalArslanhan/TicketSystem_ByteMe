using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketSystem_ByteMe.Models
{
    public enum JobTitle
    {
      Developer,
      Tester
    }

  public class Employee
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EmployeeID { get; set; }
    [Required(ErrorMessage = "this field is required")]
    public string FirstName { get; set; }
    public bool Terminated { get; set; } = false;

    [Required(ErrorMessage = "this field is required")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "this field is required")]
    public JobTitle JobTitle { get; set; }


  }
}
