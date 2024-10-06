using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
	public class ItemsController : Controller
	{
		// import the context in our contoller to access the database
		private readonly MyAppContext _context;

		// pass it to the constructor and store it
		public ItemsController(MyAppContext context)
		{
			_context = context;
		}

		// writing the needed actions

		// it is recommended to use async and await when quering data form the database
		public async Task<IActionResult> Index()
		{
			// return a view with the items from the database using the context variable
			var item = await _context.Items.ToListAsync();
			return View(item);
		}
		// now will go to the view and import the data returend form the database to view it


		public IActionResult Create() {
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create([Bind("Id, Name, Price")] Item item)
		{
			if (ModelState.IsValid)
			{
				_context.Items.Add(item);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View();
		}
	}
}
