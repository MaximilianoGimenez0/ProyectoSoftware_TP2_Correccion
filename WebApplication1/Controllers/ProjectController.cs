using Application.Dtos;
using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Application.Exceptions;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/project")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectProposalService _projectProposalService;

        public ProjectController(IProjectProposalService projectProposalService)
        {
            _projectProposalService = projectProposalService;
        }

        [HttpPost()]
        [ProducesResponseType(typeof(ProjectResponse), 201)]
        [ProducesResponseType(typeof(ApiErrorResponse), 400)]
        public async Task<IActionResult> Create([FromBody] ProjectProposalCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var firstError = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .FirstOrDefault()?.ErrorMessage ?? "Error de validación.";

                return BadRequest(new ApiErrorResponse
                {
                    message = firstError
                });
            }

            try
            {
                var result = await _projectProposalService.present(new ProjectProposalDto()
                {
                    Title = request.title,
                    Description = request.description,
                    EstimatedAmount = request.amount,
                    EstimatedDuration = request.duration,
                    Area = request.area,
                    Type = request.type,
                    CreateBy = request.user
                });

                return Created(string.Empty, result);

            }
            catch (BadRequestException ex)
            {
                return BadRequest(new ApiErrorResponse { message = ex.Message });
            }

        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProjectResponse), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 404)]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new ApiErrorResponse { message = "El ID del proyecto no puede estar vacío." });
            }


            if (!ModelState.IsValid)
            {
                var firstError = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .FirstOrDefault()?.ErrorMessage ?? "Error de validación.";

                return BadRequest(new ApiErrorResponse
                {
                    message = firstError
                });
            }

            try
            {
                var result = await _projectProposalService.GetCompleteProjectGetById(id);
                return Ok(result);
            }
            catch (NotFoundException ex) { return NotFound(new ApiErrorResponse() { message = ex.Message }); }
            catch (BadRequestException ex) { return BadRequest(new ApiErrorResponse() { message = ex.Message }); }
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(List<ProjectShortResponse>), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 400)]
        public async Task<IActionResult> GetFiltered([FromQuery] string? title, [FromQuery] int? status, [FromQuery] int? applicant, [FromQuery] int? approvalUser)
        {

            if (!ModelState.IsValid)
            {
                var firstError = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .FirstOrDefault()?.ErrorMessage ?? "Error de validación.";

                return BadRequest(new ApiErrorResponse { message = firstError });
            }

            // Validar title (si no es null) y su longitud
            if (title != null && title.Length > 255)
                return BadRequest(new ApiErrorResponse { message = "El título no puede superar 255 caracteres." });
                       
            try
            {
                var results = await _projectProposalService.GetFiltered(title, status,applicant,approvalUser);
                return Ok(results);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new ApiErrorResponse { message = ex.Message });
            }
        }




        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(ProjectResponse), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 404)]
        [ProducesResponseType(typeof(ApiErrorResponse), 409)]
        [ProducesResponseType(typeof(ApiErrorResponse), 400)]
        public async Task<IActionResult> UpdateProjectInformation(Guid id, [FromBody] ProjectUpdateRequest request)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new ApiErrorResponse { message = "El ID del proyecto no puede estar vacío." });
            }

            if (!ModelState.IsValid)
            {
                var firstError = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .FirstOrDefault()?.ErrorMessage ?? "Error de validación.";

                return BadRequest(new ApiErrorResponse
                {
                    message = firstError
                });
            }


            try
            {
                
                var results = await _projectProposalService.UpdateProject(id, request);

                return Ok(results);
            }
            catch (NotFoundException ex) { return NotFound(new ApiErrorResponse() { message = ex.Message }); }
            catch (ConflictException ex) { return Conflict(new ApiErrorResponse() { message = ex.Message }); }
            catch (BadRequestException ex) { return BadRequest(new ApiErrorResponse() { message = ex.Message }); }
        }

        [HttpPatch("{id}/decision")]
        [ProducesResponseType(typeof(ProjectResponse), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 404)]
        [ProducesResponseType(typeof(ApiErrorResponse), 409)]
        [ProducesResponseType(typeof(ApiErrorResponse), 400)]
        public async Task<IActionResult> ChangeSepStatus(Guid id, [FromBody] DecisionStepUpdateRequest request)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new ApiErrorResponse { message = "El ID del proyecto no puede estar vacío." });
            }


            if (!ModelState.IsValid)
            {
                var firstError = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .FirstOrDefault()?.ErrorMessage ?? "Error de validación.";

                return BadRequest(new ApiErrorResponse
                {
                    message = firstError
                });
            }

            try
            {
                var result = await _projectProposalService.UpdateProjectStep(id, new updateApprovalStatusDto
                {
                    step = request.id,
                    newStatus = request.status,
                    observation = request.observation,
                    user = request.user
                });

                return Ok(result);
            }
            catch (NotFoundException ex) { return NotFound(new ApiErrorResponse() { message = ex.Message }); }
            catch (ConflictException ex) { return Conflict(new ApiErrorResponse() { message = ex.Message }); }
            catch (BadRequestException ex) { return BadRequest(new ApiErrorResponse() { message = ex.Message }); }

        }

     

    }
}