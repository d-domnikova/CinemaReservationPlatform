using JWTAuthentication.Entities;
using JWTAuthentication.Models.Request;
using JWTAuthentication.Models.Response;
using JWTAuthentication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationServiceDAL.Exceptions;

namespace JWTAuthenticationAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("registrate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AuthenticationResponse>> SignUp(RegistrationRequest request)
        {
            try
            {
                var result = await _userService.RegistrateAsync(request);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }


        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AuthenticationResponse>> SignIn(AuthenticationRequest request)
        {
            try
            {
                var result = await _userService.AuthenticateAsync(request);
                SetRefreshTokenInCookie(result.RefreshToken);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        [HttpPost("addrole")]
        public async Task<IActionResult> AddRoleAsync(AddRoleRequest request)
        {
            var result = await _userService.AddRoleAsync(request);
            return Ok(result);
        }


        [HttpPost("refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RefreshToken()
        {
            try
            {
                var refreshToken = Request.Cookies["refreshToken"];
                var response = await _userService.RefreshTokenAsync(refreshToken);
                if (!string.IsNullOrEmpty(response.RefreshToken))
                    SetRefreshTokenInCookie(response.RefreshToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        [Authorize]
        [HttpPost("revoke-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenRequest request)
        {
            // accept token from request body or cookie
            var token = request.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            var response = _userService.RevokeTokenAsync(token);

            if (!response)
                return NotFound(new { message = "Token not found" });

            return Ok(new { message = "Token revoked" });
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<User>>> GetAllAsync()
        {
            try
            {
                var reservations = await _userService.GetAllAsync();
                return Ok(reservations);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> GetByIdAsync([FromRoute] string id)
        {
            try
            {
                return Ok(await _userService.GetByIdAsync(id));
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        [Authorize]
        [HttpGet("{id}/refresh-tokens")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<RefreshToken>>> GetRefreshTokensAsync([FromRoute] string id)
        {
            try
            {
                return Ok(await _userService.RefreshTokenAsync(id));
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        private void SetRefreshTokenInCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddMinutes(2),
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}
