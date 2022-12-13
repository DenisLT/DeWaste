using DeWasteApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeWasteApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SimilarItemsController : Controller
    {

        private readonly DeWasteDbContext _context;

        public SimilarItemsController(DeWasteDbContext context)
        {
            _context = context;
        }

        // GET: SimilarItems
        [HttpGet]
        public async Task<IActionResult> SimilarItems()
        {
            return _context.items != null ?
                        Json(await _context.items.Select(x => new { x.id, x.name }).ToListAsync()) :
                        Problem("Entity set 'DeWasteDbContext.Items'  is null.");
        }


        [HttpGet("{name}")]
        public async Task<IActionResult> SimilarItem(string? name)
        {
            if (name == null || _context.items == null)
            {
                return NotFound();
            }

            var items = await _context.items
                .Where(m => m.name.ToLower().Contains(name)).Select(x => new { x.id, x.name})
                .ToListAsync();
            if (items == null)
            {
                return NotFound();
            }

            return Json(items);
        }
    }
}
