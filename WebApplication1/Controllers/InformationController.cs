using System.Threading.Channels;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Dtos.Responses;
using Application.Interfaces.Services;
using Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/")]
    public class InformationController : ControllerBase
    {
        private readonly IAreaService _areaService;
        private readonly IProjectTypeService _projectTypeService;
        private readonly IApproverRoleService _approverRoleService;
        private readonly IApprovalStatusService _approvalStatusService;
        private readonly IUserService _userService;

        public InformationController(IAreaService areaService, IProjectTypeService projectTypeService, IApproverRoleService approverRoleService, IApprovalStatusService approvalStatusService, IUserService userService)
        {
            _areaService = areaService;
            _projectTypeService = projectTypeService;
            _approverRoleService = approverRoleService;
            _approvalStatusService = approvalStatusService;
            _userService = userService;
        }

        [HttpGet("Area")]
        [ProducesResponseType(typeof(List<GenericResponse>), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 404)]
        public async Task<IActionResult> AreaGetAll()
        {
            try
            {
                var results = await _areaService.GetAll();
                return Ok(results);
            }
            catch (Exception ex) { return NotFound(new ApiErrorResponse() { message = ex.Message }); }
        }

        [HttpGet("ProjectType")]
        [ProducesResponseType(typeof(List<GenericResponse>), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 404)]
        public async Task<IActionResult> TypeGetAll()
        {
            try
            {
                var results = await _projectTypeService.GetAll();
                return Ok(results);
            }
            catch (Exception ex) { return NotFound(new ApiErrorResponse() { message = ex.Message }); }
        }

        [HttpGet("Role")]
        [ProducesResponseType(typeof(List<GenericResponse>), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 404)]
        public async Task<IActionResult> RoleGetAll()
        {
            try
            {
                var results = await _approverRoleService.GetAll();
                return Ok(results);
            }
            catch (Exception ex) { return NotFound(new ApiErrorResponse() { message = ex.Message }); }
        }

        [HttpGet("ApprovalStatus")]
        [ProducesResponseType(typeof(List<GenericResponse>), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 404)]
        public async Task<IActionResult> StatusGetAll()
        {
            try
            {
                var results = await _approvalStatusService.GetAll();
                return Ok(results);
            }
            catch (Exception ex) { return NotFound(new ApiErrorResponse() { message = ex.Message }); }
        }

        [HttpGet("User")]
        [ProducesResponseType(typeof(List<UsersResponse>), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 404)]
        public async Task<IActionResult> UserGetAll()
        {
            try
            {
                var results = await _userService.GetAll();
                return Ok(results);
            }
            catch (Exception ex) { return NotFound(new ApiErrorResponse() { message = ex.Message }); }
        }

    }
}
