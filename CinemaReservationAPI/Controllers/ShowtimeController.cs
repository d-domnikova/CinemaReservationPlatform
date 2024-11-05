using Microsoft.AspNetCore.Mvc;
using MovieServiceDAL.Entities;
using MovieServiceDAL.Repositories.Interfaces;

namespace CinemaReservationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShowtimeController : ControllerBase
    {
        private readonly ILogger<ShowtimeController> _logger;

        private IUnitOfWorkDapper _unitofWork;
        public ShowtimeController(ILogger<ShowtimeController> logger, IUnitOfWorkDapper unitofWork)
        {
            _logger = logger;
            _unitofWork = unitofWork;
        }


        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Showtime>>> GetAllShowTimesAsync()
        {
            try
            {
                var results = await _unitofWork._showTimeRepository.GetAllAsync();
                _unitofWork.Commit();
                _logger.LogInformation($"Returned all show times from database.");
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Transaction Failed! Something went wrong inside GetAllShowTimeAsync() action: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error {ex.Message}");
            }
        }

        [HttpGet("GetMovieInfo")]
        public async Task<ActionResult<IEnumerable<Showtime>>> GetMovieInfo()
        {
            try
            {
                var results = await _unitofWork._showTimeRepository.GetMovieInfo();
                _unitofWork.Commit();
                _logger.LogInformation($"Returned all show times from database.");
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Transaction Failed! Something went wrong inside GetMovieInfo action: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "GetShowTimeById")]
        public async Task<ActionResult<Showtime>> GetByIdAsync(int id)
        {
            try
            {
                var result = await _unitofWork._showTimeRepository.GetAsync(id);
                _unitofWork.Commit();
                if (result == null)
                {
                    _logger.LogError($"Show time with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Returned show time with id: {id}");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetByIdAsync() action: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error {ex.Message}");
            }
        }

        [HttpPost()]
        public async Task<ActionResult> PostShowTimeAsync([FromBody] Showtime newShowTime)
        {
            try
            {
                if (newShowTime == null)
                {
                    _logger.LogError("ShowTime object sent from client is null.");
                    return BadRequest("ShowTime object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid show time object sent from client.");
                    return BadRequest("Invalid show time object");
                }
                var created_id = await _unitofWork._showTimeRepository.AddAsync(newShowTime);
                var CreatedShowTime = await _unitofWork._showTimeRepository.GetAsync(created_id);
                _unitofWork.Commit();
                return CreatedAtRoute("GetShowTimeById", new { id = created_id }, CreatedShowTime);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside PostShowTimeAsync() action: {ex.Message}");
                return StatusCode(500, $"Internal server error {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] Showtime updateShowTime)
        {
            try
            {
                if (updateShowTime == null)
                {
                    _logger.LogError("ShowTime object sent from client is null.");
                    return BadRequest("ShowTime object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid show time object sent from client.");
                    return BadRequest("Invalid show time object");
                }
                var ShowTimeEntity = await _unitofWork._showTimeRepository.GetAsync(id);
                if (ShowTimeEntity == null)
                {
                    _logger.LogError($"Show time with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                await _unitofWork._showTimeRepository.UpdateAsync(updateShowTime);
                _unitofWork.Commit();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside PutAsync() action: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                var ShowTimeEntity = await _unitofWork._showTimeRepository.GetAsync(id);
                if (ShowTimeEntity == null)
                {
                    _logger.LogError($"ShowTime with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                await _unitofWork._showTimeRepository.DeleteAsync(id);
                _unitofWork.Commit();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteAsync() action: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
