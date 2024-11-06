using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MovieServiceDAL.Entities;
using MovieServiceDAL.Repositories.Interfaces;

namespace CinemaReservationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;

        private IUnitOfWorkDapper _unitofWork;

        private readonly IMemoryCache _memoryCache;

        private readonly string cacheKey = "getMoviesCacheKey";
        public MovieController(ILogger<MovieController> logger, IUnitOfWorkDapper unitofWork, IMemoryCache memoryCache)
        {
            _logger = logger;
            _unitofWork = unitofWork;
            _memoryCache = memoryCache;
        }


        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Movie>>> GetAllMoviesAsync()
        {
            try
            {
                var results = (List<Movie>)await _unitofWork._movieRepository.GetAllAsync();
                _unitofWork.Commit();
                if (_memoryCache.TryGetValue(cacheKey, out IEnumerable<Movie> movies))
                {
                    _logger.Log(LogLevel.Information, "Movies found in cache.");
                }
                else
                {
                   _logger.Log(LogLevel.Information, "There are no movies in the cache. Loading them...");
                   
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(45))
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                        .SetPriority(CacheItemPriority.Normal);

                    _memoryCache.Set(cacheKey, results, cacheEntryOptions);
                }

                _logger.LogInformation($"Returned all movies from database.");
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Transaction Failed! Something went wrong inside GetAllMovieAsync() action: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "GetMovieById")]
        public async Task<ActionResult<Movie>> GetByIdAsync(int id)
        {
            try
            {
                var result = await _unitofWork._movieRepository.GetAsync(id);
                _unitofWork.Commit();
                if (result == null)
                {
                    _logger.LogError($"Movie with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Returned movie with id: {id}");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetByIdAsync() action: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("GetMovieGenres/{id}", Name = "GetMovieGenresById")]
        public async Task<ActionResult<Movie>> GetMovieGenresAsync(int id)
        {
            try
            {
                var result = await _unitofWork._movieRepository.GetMovieGenres(id);
                _unitofWork.Commit();
                if (result == null)
                {
                    _logger.LogError($"Movie with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Returned movie with id: {id}");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetMovieGenresByIdAsync() action: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPost()]
        public async Task<ActionResult> PostMovieAsync([FromBody] Movie newMovie)
        {
            try
            {
                if (newMovie == null)
                {
                    _logger.LogError("Movie object sent from client is null.");
                    return BadRequest("Movie object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid movie object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var created_id = await _unitofWork._movieRepository.AddAsync(newMovie);
                var CreatedMovie = await _unitofWork._movieRepository.GetAsync(created_id);
                _unitofWork.Commit();
                return CreatedAtRoute("GetMovieById", new { id = created_id }, CreatedMovie);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside PostBookAsync action: {ex.Message}");
                return StatusCode(500, $"Internal server error {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] Movie updateMovie)
        {
            try
            {
                if (updateMovie == null)
                {
                    _logger.LogError("Movie object sent from client is null.");
                    return BadRequest("Movie object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid movie object sent from client.");
                    return BadRequest("Invalid movie object");
                }
                var MovieEntity = await _unitofWork._movieRepository.GetAsync(id);
                if (MovieEntity == null)
                {
                    _logger.LogError($"Movie with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                await _unitofWork._movieRepository.UpdateAsync(updateMovie);
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
                var MovieEntity = await _unitofWork._movieRepository.GetAsync(id);
                if (MovieEntity == null)
                {
                    _logger.LogError($"Movie with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                await _unitofWork._movieRepository.DeleteAsync(id);
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
