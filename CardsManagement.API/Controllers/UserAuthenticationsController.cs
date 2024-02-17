using CardsManagement.Application.Service.Interface;
using CardsManagement.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using LoginRequest = CardsManagement.Application.ViewModels.LoginRequest;

namespace CardsManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag(description: "User Logins endpoint")]

    public class UserAuthenticationsController : ControllerBase
    {
        private readonly IUserManagementService _userManagementService;
        private GeneralViewModel res; 

        public UserAuthenticationsController(IUserManagementService userManagementService )
        {
            _userManagementService = userManagementService;
            res = new GeneralViewModel(); 
        }

        [HttpPost(nameof(Login))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserLoginResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GeneralViewModel))]
        [SwaggerOperation(Summary = "Member/Admin Login" )]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            res = new();

            #region Validation
            if (request == null)
            {
                res.Message = "Parameters are required";
                return BadRequest(res);
            }

            if (!(ModelState.IsValid))
            {
                var modelError = ModelState.Where(x => x.Value.Errors.Count > 0)
                                               .Select(x => new
                                               {
                                                   x.Key,
                                                   x.Value.Errors[0].ErrorMessage

                                               }).FirstOrDefault();

                res.Message = modelError.ErrorMessage;
                return BadRequest(res);

            }
            #endregion

            var response = await _userManagementService.Login(request);

            res.Message = response.Message;
            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest(res);
        }

    }
}
