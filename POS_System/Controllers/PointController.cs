using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS_System.Entity;
using POS_System.Interface;

namespace POS_System.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class PointController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;
        private readonly IPointService _pointService;
        private static object _lock = new object();

        public PointController(DatabaseContext dbContext, IPointService pointService)
        {
            _dbContext = dbContext;
            _pointService = pointService;
        }

        [HttpGet("points")]
        public IEnumerable<Point> Get()
        {
            var cacheData = _pointService.GetData<IEnumerable<Point>>("point");
            if (cacheData != null)
            {
                return cacheData;
            }
            lock (_lock)
            {
                var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
                cacheData = _dbContext.Points.ToList();
                _pointService.SetData<IEnumerable<Point>>("point", cacheData, expirationTime);
            }
            return cacheData;
        }

        [HttpGet("point")]
        public Point Get(int id)
        {
            Point filteredData;
            var cacheData = _pointService.GetData<IEnumerable<Point>>("point");
            if (cacheData != null)
            {
                filteredData = cacheData.Where(x => x.Id == id).FirstOrDefault();
                return filteredData;
            }
            filteredData = _dbContext.Points.Where(x => x.Id == id).FirstOrDefault();
            return filteredData;
        }

        [HttpPost("addpoint")]
        public async Task<Point> Post(Point value)
        {
            var obj = await _dbContext.Points.AddAsync(value);
            _pointService.RemoveData("point");
            _dbContext.SaveChanges();
            return obj.Entity;
        }

        [HttpPut("updatepoint")]
        public void Put(Point product)
        {
            _dbContext.Points.Update(product);
            _pointService.RemoveData("point");
            _dbContext.SaveChanges();
        }

        [HttpDelete("deletepoint")]
        public void Delete(int Id)
        {
            var filteredData = _dbContext.Points.Where(x => x.Id == Id).FirstOrDefault();
            _dbContext.Remove(filteredData);
            _pointService.RemoveData("point");
            _dbContext.SaveChanges();
        }
    }
}
