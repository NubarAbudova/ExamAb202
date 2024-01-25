using Exam.Areas.Manage.ViewModels.Client;
using Exam.DAL;
using Exam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exam.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ClientController : Controller
    {
        private readonly AppDbContext _context;

        public ClientController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Client> clients = await _context.Clients.ToListAsync();
            return View(clients);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Create(CreateClientVM createClientVM)
        {
            if (!ModelState.IsValid) return View(createClientVM);

            bool result = await _context.Clients.AnyAsync(c => c.Name == createClientVM.Name);
            if (result)
            {
                ModelState.AddModelError("Name", "Name already exists");
            }
        }
    }
}
