using DeWasteApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace DeWasteApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : Controller
    {

        private readonly DeWasteDbContext _context;

        public CategoriesController(DeWasteDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return _context.categories != null ?
                        Json(_context.items_categories) :
                        Problem("Entity set 'DeWasteDbContext.Categories'  is null.");
        }

        [HttpGet("{item_id}")]
        public IActionResult Details(int? item_id)
        {
            if (item_id == null || _context.categories == null)
            {
                return NotFound();
            }

            var categories = _context.items_categories
                .Where(m => m.item_id == item_id).Select(x => new { x.category_id });
            if (categories == null)
            {
                return NotFound();
            }

            return Json(categories);
        }
    }
}
