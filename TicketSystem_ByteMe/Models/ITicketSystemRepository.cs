using Microsoft.AspNetCore.Mvc;

namespace TicketSystem_ByteMe.Models
{
  public interface ITicketSystemRepository
  {
    IQueryable<Project> Projects { get; }
    IQueryable<Employee> Employees { get; }
    IQueryable<Ticket> Tickets { get; }

    public void AddEmployee(Employee employee);
    public void AddProject(Project project);
    public void AddTicket(Ticket ticket);
    public void EditEmployee(Employee employee);
    public void EditProject(Project project);
    public void EditTicket(Ticket ticket);
    public void RemoveEmployee(int id);
    public void RemoveProject(int id);
    public void TerminateEmployee(int id);
    public void SolvedTicket(int id);
    public void EndProject(int id);
  }
}
