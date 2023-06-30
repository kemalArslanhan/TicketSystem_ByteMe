using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketSystem_ByteMe.Models;

namespace TicketSystem_ByteMe.Home
{
  public class AdminController : Controller
  {
    private ITicketSystemRepository repo;

    public AdminController(ITicketSystemRepository repository)
    {
      repo = repository;
    }

    public IActionResult Home()
    {
      return View(repo);
    }
    public IActionResult ShowEmployees()
    {
      var employees = repo.Employees.ToList();
      return View(employees);
    }
    public IActionResult ShowProjects()
    {
      return View(repo.Projects);
    }
    public IActionResult ShowTickets()
    {
      return View(repo.Tickets
        .Include(t => t.Project)
        .Include(t => t.AssignedTo)
        .Include(t => t.CreatedBy)
        .OrderBy(h => h.Project));
    }
    public IActionResult EmployeeDetail(int id)
    {
      GenerateValuesEmployees();
      return View(repo.Employees
        .FirstOrDefault(p => p.EmployeeID == id));
    }
    public IActionResult Error()
    {
      return View();
    }
    public IActionResult ProjectDetail(int id)
    {
      GenerateValuesTickets();
      return View(repo.Projects
        .FirstOrDefault(p => p.ProjectID == id));
    }
    [HttpGet]
    public IActionResult NewEmployee()
    {
      return View();
    }
    [HttpPost]
    public IActionResult NewEmployee(Employee employee)
    {
      if (ModelState.IsValid)
      {
        repo.AddEmployee(employee);
        return RedirectToAction("ShowEmployees");
      }
      return View(employee);
    }
    [HttpGet]
    public IActionResult NewProject()
    {
      return View();
    }
    [HttpPost]
    public IActionResult NewProject(Project project)
    {
      if (ModelState.IsValid)
      {
        repo.AddProject(project);
        return RedirectToAction("ShowProjects");
      }
      return View(project);
    }
    [HttpGet]
    public IActionResult EditEmployee(int id)
    {
      return View(repo.Employees
        .FirstOrDefault(p => p.EmployeeID == id));
    }
    [HttpPost]
    public IActionResult EditEmployee(Employee employee)
    {
      if (ModelState.IsValid)
      {
        repo.EditEmployee(employee);
        return RedirectToAction("ShowEmployees");
      }
      return View(employee);
    }
    [HttpGet]
    public IActionResult EditProject(int id)
    {
      return View(repo.Projects
        .FirstOrDefault(p => p.ProjectID == id));
    }
    [HttpPost]
    public IActionResult EditProject(Project project)
    {
      if (ModelState.IsValid)
      {
        repo.EditProject(project);
        return RedirectToAction("ShowProjects");
      }
      return View(project);
    }
    public IActionResult DeleteEmployee(int id)
    {
      repo.RemoveEmployee(id);
      return RedirectToAction("ShowEmployees");
    }
    public IActionResult DeleteProject(int id)
    {
      repo.RemoveProject(id);
      return RedirectToAction("ShowProjects");
    }
    public IActionResult TerminateEmployee(int id)
    {
      repo.TerminateEmployee(id);
      return RedirectToAction("ShowEmployees");
    }
    public IActionResult EndProject(int id)
    {
      repo.EndProject(id);
      return RedirectToAction("ShowProjects");
    }
    public void GenerateValuesTickets()
    {
      ViewBag.TicketsProjectID = repo.Tickets
        .Select(n => n.Project.ProjectID)
        .ToList();
    }
    public void GenerateValuesEmployees()
    {
      ViewBag.TicketsAssignedTo = repo.Tickets
        .Select(a => a.AssignedTo.EmployeeID)
        .ToList();

      ViewBag.TicketsCreatedBy = repo.Tickets
        .Select(a => a.CreatedBy.EmployeeID)
        .ToList();
    }
  }
}
