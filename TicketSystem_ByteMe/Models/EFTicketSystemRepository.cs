using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.EntityFrameworkCore;

namespace TicketSystem_ByteMe.Models
{
  public class EFTicketSystemRepository : ITicketSystemRepository
  {
    private TicketSystemDBContext ctx;

    public EFTicketSystemRepository(TicketSystemDBContext ctx)
    {
      this.ctx = ctx;
    }

    public IQueryable<Project> Projects => ctx.Projects;
    public IQueryable<Employee> Employees => ctx.Employees;
    public IQueryable<Ticket> Tickets => ctx.Tickets;

    public void AddEmployee(Employee employee)
    {
      ctx.Employees.Add(employee);
      ctx.SaveChanges();
    }
    public void AddProject(Project project)
    {
      ctx.Projects.Add(project);
      ctx.SaveChanges();
    }
    public void AddTicket(Ticket ticket)
    {
      ctx.Tickets.Add(ticket);
      ctx.SaveChanges();
    }
   public void EditEmployee(Employee employee)
    {
      Employee oldEmployee = ctx.Employees
        .FirstOrDefault(p => p.EmployeeID == employee.EmployeeID);

      oldEmployee.FirstName = employee.FirstName;
      oldEmployee.LastName = employee.LastName;
      oldEmployee.JobTitle = employee.JobTitle;

      ctx.Employees.Update(oldEmployee);
      ctx.SaveChanges();
    }
    public void EditProject(Project project)
    { 
      Project oldProject = ctx.Projects
        .FirstOrDefault(p => p.ProjectID == project.ProjectID);

      oldProject.Start = project.Start;
      oldProject.Title = project.Title;
      oldProject.Description = project.Description; 

      ctx.Projects.Update(oldProject);
      ctx.SaveChanges();
    }
    public void EditTicket(Ticket ticket)
    {
      Ticket oldTicket = ctx.Tickets
        .FirstOrDefault(t => t.TicketID == ticket.TicketID);

      oldTicket.Description = ticket.Description;
      oldTicket.AssignedTo = ticket.AssignedTo;

      ctx.Update(oldTicket);
      ctx.SaveChanges();
    }
  public void RemoveEmployee(int id)
    {
      ctx.Remove(Employees
        .FirstOrDefault(e => e.EmployeeID == id));

      ctx.SaveChanges();
    }
  public void RemoveProject(int id)
    {
      ctx.Remove(Projects
        .FirstOrDefault(p => p.ProjectID == id));

      ctx.SaveChanges();
    }
      public void TerminateEmployee(int id)
    {
      Employee oldEmployee = ctx.Employees
        .FirstOrDefault(p => p.EmployeeID == id);

      oldEmployee.FirstName = "[Terminated]" + ' ' + oldEmployee.FirstName;
      oldEmployee.Terminated = true;

      ctx.Employees.Update(oldEmployee);
      ctx.SaveChanges();
    }
    public void SolvedTicket(int id)
    {
      Ticket oldTicket = ctx.Tickets
        .FirstOrDefault(t => t.TicketID == id);

      oldTicket.SolvedAt = DateTime.Now;

      ctx.Update(oldTicket);
      ctx.SaveChanges();
    }
    public void EndProject(int id)
    {
      Project oldProject = ctx.Projects
        .FirstOrDefault(p => p.ProjectID == id);

      oldProject.End = DateTime.Now;

      ctx.Projects.Update(oldProject);
      ctx.SaveChanges();
    } 
  }
}

