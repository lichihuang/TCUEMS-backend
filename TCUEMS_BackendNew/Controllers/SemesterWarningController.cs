using Microsoft.AspNetCore.Mvc;
using TCUEMS_BackendNew.Data;
using TCUEMS_BackendNew.Models;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/[controller]")]
public class SemesterWarningController : ControllerBase
{
    private readonly ISemesterWarningRepository _semesterWarningRepository;
    private readonly ILogger<SemesterWarningController> _logger;

    public SemesterWarningController(
        ISemesterWarningRepository semesterWarningRepository,
        ILogger<SemesterWarningController> logger)
    {
        _semesterWarningRepository = semesterWarningRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetSemesterWarnings()
    {
        try
        {
            var semesterWarnings = await _semesterWarningRepository.GetAllSemesterWarnings();
            return Ok(semesterWarnings);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while processing GetSemesterWarnings: {ex.Message}");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("Search")]
    public async Task<IActionResult> SearchSemesterWarnings([FromBody] SemesterWarning searchCriteria)
    {
        try
        {
            if (searchCriteria == null || !searchCriteria.IsValid())
            {
                return BadRequest("Invalid search criteria");
            }

            _logger.LogInformation($"Received search criteria: {JsonConvert.SerializeObject(searchCriteria)}");

            var semesterWarnings = await _semesterWarningRepository.GetSemesterWarningsByCriteria(searchCriteria);
            return Ok(semesterWarnings);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while processing SearchSemesterWarnings: {ex.Message}");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

}
