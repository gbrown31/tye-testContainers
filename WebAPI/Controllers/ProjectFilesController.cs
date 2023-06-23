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
        public async Task<ActionResult> Get([FromQuery] GetAllUserFilesQuery query, [FromServices] IDatabase dbContext, [FromServices] IFileStorage fileStorage)
        {
            GetAllUserFiles handler = new GetAllUserFiles(dbContext, fileStorage);
            var result = await handler.HandleAsync(query);

            return Ok(result);
        }

        [HttpPost]
        public ActionResult Post([FromBody] AddFileToProjectCommand command, [FromServices] IDatabase dbContext, [FromServices] IFileStorage fileStorage)
        {
            AddFileToProjectHandler handler = new AddFileToProjectHandler(dbContext, fileStorage);

            return Ok(handler.Handle(command));
        }
    }
}