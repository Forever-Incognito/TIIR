using Microsoft.AspNetCore.Mvc;
using EtlServiceApp.Models;
using EtlServiceApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EtlServiceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EtlController : ControllerBase
    {
        private readonly EtlService _etlService;

        public EtlController(EtlService etlService)
        {
            _etlService = etlService;
        }

        // POST: api/etl/run
        // Запускає ETL-процес
        [HttpPost("run")]
        public async Task<IActionResult> RunEtl()
        {
            await _etlService.RunEtlProcess();
            return Ok("ETL process completed successfully."); // Повертаємо підтвердження
        }

        // GET: api/etl/consolidated
        // Отримує результат ETL-процесу (консолідовані дані)
        [HttpGet("consolidated")]
        public ActionResult<IEnumerable<ConsolidatedRecipe>> GetConsolidatedData()
        {
            var data = _etlService.GetConsolidatedData();
            if (data == null || !data.Any())
            {
                return NotFound("No consolidated data available. Run ETL process first.");
            }
            return Ok(data); // Повертаємо консолідовані дані
        }
    }
}