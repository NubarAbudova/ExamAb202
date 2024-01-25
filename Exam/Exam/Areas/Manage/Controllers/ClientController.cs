using Exam.Areas.Manage.ViewModels.Client;
using Exam.DAL;
using Exam.Models;
using Exam.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exam.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ClientController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ClientController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
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
        public async Task<IActionResult> Create(CreateClientVM createClientVM)
        {
            if (!ModelState.IsValid) return View(createClientVM);

            bool result = await _context.Clients.AnyAsync(c => c.Name == createClientVM.Name);
            if (result)
            {
                ModelState.AddModelError("Name", "Name already exists");
                return View();
            }
            if (!createClientVM.Photo.ValidateType("image/"))
            {
                ModelState.AddModelError("Photo", "Photo type does not match");
                return View(createClientVM);
            }
            if (!createClientVM.Photo.ValidateSize(600))
            {
                ModelState.AddModelError("Photo", "Photo size does not match");
                return View(createClientVM);
            }

            Client client = new Client()
            {
                Name = createClientVM.Name,
                Description = createClientVM.Description,
                City = createClientVM.City,
                Image = await createClientVM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images")
            };

            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();

            Client client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);

            if (client == null) return NotFound();

            UpdateClientVM updateClientVM = new UpdateClientVM()
            {
                Name = client.Name,
                City = client.City,
                Description = client.Description,
                Image=client.Image
            };
            return View(updateClientVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateClientVM updateClientVM)
        {
            if (!ModelState.IsValid) return View(updateClientVM);

            Client exists = await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);

            if (exists == null) return NotFound();

            if (!updateClientVM.Photo.ValidateType("image/"))
            {
                ModelState.AddModelError("Photo", "Photo type does not match");
                return View(updateClientVM);
            }
            if (!updateClientVM.Photo.ValidateSize(600))
            {
                ModelState.AddModelError("Photo", "Photo size does not match");
                return View(updateClientVM);
            }

            exists.Name = updateClientVM.Name;
            exists.City = updateClientVM.City;
            exists.Description = updateClientVM.Description;
            exists.Image = await updateClientVM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images");

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();

            Client exists = await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);

            if (exists == null) return NotFound();

            _context.Clients.Remove(exists);
            _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
