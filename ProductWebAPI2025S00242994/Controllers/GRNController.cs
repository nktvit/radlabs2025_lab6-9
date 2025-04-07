using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductModel.GRN;
using Tracker.WebAPIClient;

namespace ProductWebAPI2025.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Store Manager")]
    public class GRNController : ControllerBase
    {
        private readonly IGRN<ProductModel.GRN.GRN> _grnRepository;

        public GRNController(IGRN<ProductModel.GRN.GRN> grnRepository)
        {
            _grnRepository = grnRepository;
            ActivityAPIClient.Track(StudentID: "S00242994", StudentName: "Mykyta Vitkovsky", 
                activityName: "RAD30223 Week 8 Lab 1", Task: "Testing GRN Controller Actions");
        }

        // GET: api/GRN
        [HttpGet]
        public ActionResult<IEnumerable<GRN>> GetGRNs()
        {
            return Ok(_grnRepository.GetAll());
        }

        // GET: api/GRN/5
        [HttpGet("{id}")]
        public ActionResult<GRN> GetGRN(int id)
        {
            var grn = _grnRepository.Get(id);

            if (grn == null)
            {
                return NotFound();
            }

            return Ok(grn);
        }
    }
}
