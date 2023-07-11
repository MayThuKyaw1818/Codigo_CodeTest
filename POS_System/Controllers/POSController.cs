using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS_System.Model;

namespace POS_System.Controllers
{
    [ApiController]
    [Route("API/[controller]")]
    public class POSController : Controller
    {
        private readonly DatabaseContext _context;

        public POSController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("POSSystemAPI")]
        [Authorize]
        public List<Point> POSSystemAPI()
        {
            var pointList = _context.Points.ToList();

            return pointList;
        }
    }
}
