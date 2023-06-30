using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Storage;
using System.Net.Sockets;
using TicketSystem_ByteMe.Models;

namespace TicketSystem_ByteMe.Home
{
  public class HomeController : Controller
  {
    private ITicketSystemRepository repo;
    public HomeController(ITicketSystemRepository repo)
    {
      this.repo = repo;
    }
    private void GenerateValues()
    {
      var employees = repo.Employees
        .Where(t => t.Terminated == false)
        .Select(n => new
        {
          Name = n.FirstName + ' ' + n.LastName,
          ID = n.EmployeeID
        .ToString()
        });

      var projects = repo.Projects
        .Select(n => new { n.Title, n.ProjectID });

      ViewBag.Employee = new SelectList(employees, "ID", "Name");
      ViewBag.Project = new SelectList(projects, "ProjectID", "Title");

    }
    public IActionResult Index()
    {
      return View(repo);
    }
    public IActionResult ShowTickets()
    {

      return View(repo.Tickets
        .Include(t => t.Project)
        .Include(t => t.AssignedTo)
        .Include(t => t.CreatedBy)
        .OrderBy(h => h.Project));
    }
    public IActionResult TicketDetail(int id)
    {
      return View(repo.Tickets
        .Include(t => t.Project)
        .Include(e => e.AssignedTo)
        .Include(t => t.CreatedBy)
        .FirstOrDefault(t => t.TicketID == id));
    }
    [HttpGet]
    public IActionResult NewTicket()
    {
      GenerateValues();
      return View();
    }
    [HttpPost]
    public IActionResult NewTicket(Ticket ticket)
    {
      if (ticket.Project is not null)
      {
        ticket.Project = repo.Projects
            .FirstOrDefault(t => t.ProjectID == ticket.Project.ProjectID);
      }
      if (ticket.CreatedBy is not null)
      {
        ticket.CreatedBy = repo.Employees
            .FirstOrDefault(t => t.EmployeeID == ticket.CreatedBy.EmployeeID);
      }
      if (ticket.AssignedTo is not null)
      {
        ticket.AssignedTo = repo.Employees
            .FirstOrDefault(t => t.EmployeeID == ticket.AssignedTo.EmployeeID);
      }

      ModelState.Clear();
      TryValidateModel(ticket);

      if (ModelState.IsValid)
      {
        repo.AddTicket(ticket);
        return RedirectToAction("ShowTickets");
      }
      else
      {
        GenerateValues();
        return View(ticket);

      }
    }

    [HttpGet]
    public IActionResult EditTicket(int id)
    {
      GenerateValues();
      return View(repo.Tickets
        .Include(t => t.Project)
        .Include(e => e.AssignedTo)
        .Include(t => t.CreatedBy)
        .FirstOrDefault(t => t.TicketID == id));
    }
    [HttpPost]
    public IActionResult EditTicket(Ticket ticket)
    {
      ticket.Project = repo.Projects
        .FirstOrDefault(t => t.ProjectID == ticket.Project.ProjectID);

      ticket.CreatedBy = repo.Employees
        .FirstOrDefault(t => t.EmployeeID == ticket.CreatedBy.EmployeeID);

      ticket.AssignedTo = repo.Employees
        .FirstOrDefault(t => t.EmployeeID == ticket.AssignedTo.EmployeeID);

      ModelState.Clear();
      TryValidateModel(ticket);

      if (ModelState.IsValid)
      {
        repo.EditTicket(ticket);
        return RedirectToAction("ShowTickets");
      }
      else
      {
        GenerateValues();
        return View(ticket);

      }
    }
    public IActionResult SolvedTicket(int id)
    {
      repo.SolvedTicket(id);
      return RedirectToAction("ShowTickets");
    }
  }
}
