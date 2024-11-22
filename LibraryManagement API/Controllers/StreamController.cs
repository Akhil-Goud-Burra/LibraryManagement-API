using LibraryManagement_API.DTO.Serializers;
using LibraryManagement_API.RepositoryPattern.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement_API.Controllers
{
    [Route("api/StreamController")]
    [ApiController]
    public class StreamController : ControllerBase
    {
        private readonly ILogger<StreamController> _logger;

        private IRepositoryStream _repository;

        public StreamController(ILogger<StreamController> logger , IRepositoryStream repository) 
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet(Name = "GetStreamNames")]
        public IActionResult GetStreamNames()
        {
            string baseUrl = $"{Request.Scheme}://{Request.Host}";

            var results = _repository.GetAll(baseUrl);

            return Ok(results);
        }
    }
}
