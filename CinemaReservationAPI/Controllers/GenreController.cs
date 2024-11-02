using Microsoft.AspNetCore.Mvc;
using MovieServiceDAL.Entities;
using MovieServiceDAL.Repositories.Interfaces;

namespace CinemaReservationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly ILogger<GenreController> _logger;

        private IUnitOfWorkDapper _unitofWork;
        public GenreController(ILogger<GenreController> logger, IUnitOfWorkDapper unitofWork)
        {
            _logger = logger;
            _unitofWork = unitofWork;
        }


        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Genre>>> GetAllGenresAsync()
        {
            try
            {
                var results = await _unitofWork._genreRepository.GetAllAsync();
                _unitofWork.Commit();
                _logger.LogInformation($"Returned all genres from database.");
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Transaction Failed! Something went wrong inside GetAllGenresAsync() action: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error {ex.Message}");
            }
        }

        [HttpGet("Top5Genres")]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenresWithMostMovies()
        {
            try
            {
                var results = await _unitofWork._genreRepository.GetGenresWithMostMovies();
                _unitofWork.Commit();
                _logger.LogInformation($"Returned all genres from database.");
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Transaction Failed! Something went wrong inside GetAllGenresAsync() action: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error {ex.Message}");
            }
        }

        [HttpGet("{id}", Name = "GetGenreById")]
        public async Task<ActionResult<Genre>> GetByIdAsync(int id)
        {
            try
            {
                var result = await _unitofWork._genreRepository.GetAsync(id);
                _unitofWork.Commit();
                if (result == null)
                {
                    _logger.LogError($"Genre with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Returned genre with id: {id}");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetByIdAsync() action: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPost()]
        public async Task<ActionResult> PostGenreAsync([FromBody] Genre newGenre)
        {
            try
            {
                if (newGenre == null)
                {
                    _logger.LogError("Genre object sent from client is null.");
                    return BadRequest("Genre object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid genre object sent from client.");
                    return BadRequest("Invalid genre object");
                }
                var created_id = await _unitofWork._genreRepository.AddAsync(newGenre);
                var CreatedGenre = await _unitofWork._genreRepository.GetAsync(created_id);
                _unitofWork.Commit();
                return CreatedAtRoute("GetGenreById", new { id = created_id }, CreatedGenre);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside PostGenreAsync action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] Genre updateGenre)
        {
            try
            {
                if (updateGenre == null)
                {
                    _logger.LogError("Genre object sent from client is null.");
                    return BadRequest("Genre object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid genre object sent from client.");
                    return BadRequest("Invalid genre object");
                }
                var GenreEntity = await _unitofWork._genreRepository.GetAsync(id);
                if (GenreEntity == null)
                {
                    _logger.LogError($"Genre with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                await _unitofWork._genreRepository.UpdateAsync(updateGenre);
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
                var GenreEntity = await _unitofWork._genreRepository.GetAsync(id);
                if (GenreEntity == null)
                {
                    _logger.LogError($"Genre with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                await _unitofWork._genreRepository.DeleteAsync(id);
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
