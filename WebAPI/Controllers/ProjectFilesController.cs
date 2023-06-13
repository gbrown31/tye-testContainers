using Application;
using Application.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectFilesController : ControllerBase
    {
        private readonly ILogger<ProjectFilesController> _logger;

        public ProjectFilesController(ILogger<ProjectFilesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] GetAllProjectFilesQuery query, [FromServices] IDatabase dbContext, [FromServices] IFileStorage fileStorage)
        {
            GetAllProjectFiles handler = new GetAllProjectFiles(dbContext, fileStorage);
            var result = await handler.HandleAsync(query);

            return Ok(result);
        }
    }
}