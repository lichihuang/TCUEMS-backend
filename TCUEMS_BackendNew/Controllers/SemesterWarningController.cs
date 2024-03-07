using Microsoft.AspNetCore.Mvc;
using TCUEMS_BackendNew.Data;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class SemesterWarningController : ControllerBase
{
    private readonly ISemesterWarningRepository _semesterWarningRepository;

    public SemesterWarningController(ISemesterWarningRepository semesterWarningRepository)
    {
        _semesterWarningRepository = semesterWarningRepository;
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
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
