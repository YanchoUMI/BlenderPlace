using System.Security.Claims;
using BlenderPlace.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlenderPlace.Controllers
{
    public class TutorialsController : Controller
	{
		private readonly ApplicationDbContext context;
		private readonly UserManager<ApplicationUser> _userManager;

		public TutorialsController(ApplicationDbContext context,
									UserManager<ApplicationUser> userManager)
		{
			this.context = context;
			this._userManager = userManager;
		}

		public IActionResult Index(string sortOrder, int? pageSize, int? pageNumber, string createdName, string category, string search)
		{
			var tutorials = context.Tutorials.AsQueryable();

			if (!string.IsNullOrEmpty(createdName))
			{
				tutorials = tutorials.Where(m => m.CreatedByName == createdName);
			}

			if (!string.IsNullOrEmpty(search))
			{
				tutorials = tutorials.Where(m => m.Title.Contains(search) || m.Description.Contains(search));
			}

			if (!string.IsNullOrEmpty(category) && Enum.TryParse<Category>(category, out var parsedCategory))
			{
				tutorials = tutorials.Where(m => m.Category == parsedCategory);
			}

			// sorting
			switch (sortOrder)
			{
				case "date_asc":
					tutorials = tutorials.OrderBy(t => t.CreateDate);
					break;
				case "date_desc":
					tutorials = tutorials.OrderByDescending(t => t.CreateDate);
					break;
				default:
					tutorials = tutorials.OrderBy(t => t.CreateDate);
					break;
			}

			pageSize = pageSize ?? 5;
			pageNumber = pageNumber ?? 1;
			ViewBag.PageSize = pageSize.Value;
			ViewBag.CurrentPage = pageNumber.Value;
			ViewBag.TotalPages = (int)Math.Ceiling((double)tutorials.Count() / pageSize.Value);

			// Set the available page size options
			ViewBag.PageSizeOptions = new List<int> { 5, 10, 15, 20 };

			var paginatedMaterials = tutorials.Skip((pageNumber.Value - 1) * pageSize.Value)
											  .Take(pageSize.Value);

			//Pass the paginated materials to the view
			return View(paginatedMaterials.ToList());
		}

		[Authorize(Roles = "Creator, Admin")]
		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(Tutorial tutorial)
		{

			var userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;

			if (userId == null)
			{
				throw new ArgumentException("Invalid User.");
			}

			var user = await _userManager.FindByIdAsync(userId);

			tutorial.CreatedByName = user.Name;

			if (ModelState.IsValid)
			{
				tutorial.CreatorId = userId;
				context.Tutorials.Add(tutorial);
				context.SaveChanges();
				return RedirectToAction("Index");
			}

			return View("Add", tutorial);
		}

		[Authorize(Roles = "Creator, Admin")]
		public IActionResult Edit(int id)
		{
			var tutorial = context.Tutorials.FirstOrDefault(t => t.Id == id);

			if (tutorial == null)
			{
				return NotFound();
			}

            var userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;

            if (tutorial?.CreatorId != userId)
			{
				return Forbid();
			}

			return View(tutorial);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Tutorial tutorial)
		{
			var dbTutorial = await context.Tutorials.AsNoTracking().FirstAsync(t => t.Id == tutorial.Id);
			var originalCreator = await context.Users.AsNoTracking().FirstAsync(u => u.Id == dbTutorial.CreatorId);
			tutorial.CreatedByName = dbTutorial.CreatedByName;
			tutorial.Creator = originalCreator;
			if (ModelState.IsValid)
			{
				context.Tutorials.Update(tutorial);
				context.SaveChanges();
				return RedirectToAction("Index");
			}
			return View("Edit", tutorial);
		}

		[HttpPost]
		[Authorize(Roles = "Creator, Admin")]
		public IActionResult Delete(int id)
		{
			var tutorial = context.Tutorials.Find(id);

			if (tutorial == null)
			{
				return NotFound();
			}

			context.Tutorials.Remove(tutorial);
			context.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult Details(int id)
		{
            var tutorial = context.Tutorials.FirstOrDefault(t => t.Id == id);

            if (tutorial == null)
            {
                return NotFound();
            }

            return View(tutorial);
		}

		public IActionResult Ascending()
		{
			var tutorials = context.Tutorials.OrderBy(s => s.CreateDate).ToList();
			return View("Index", tutorials);
		}

		public IActionResult Descending()
		{
			var tutorials = context.Tutorials.OrderByDescending(s => s.CreateDate).ToList();
			return View("Index", tutorials);
		}
	}
}
