using KeyManagementAPI.Entities;
using KeyManagementAPI.Services.PredictServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KeyManagementAPI.Controllers.Predict
{
    [ApiController]
    [Route("api/predict")]
    public class PredictController : ControllerBase
    {
        private readonly IBruteEstimationService _estimate;

        public PredictController(IBruteEstimationService estimate) => _estimate = estimate;

        [HttpPost]
        public ActionResult<BruteForceEstimateOutput> Estimate([FromBody] BruteForceEstimateInput input)
        {
            return Ok(_estimate.Predict(input));
        }
    }
}
