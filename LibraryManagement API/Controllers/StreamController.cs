using LibraryManagement_API.DTO.DeSerializers;
using LibraryManagement_API.DTO.Serializers;
using LibraryManagement_API.Error_Handling.Custom_Exception_Setup;
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

            var results = _repository.GetAll_Stream(baseUrl);

            return Ok(results);
        }


        [HttpPost(Name = "CreateStreamName")]
        public IActionResult CreateStreamName([FromBody]CreateStreamDTO Incomming_Request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                string baseUrl = $"{Request.Scheme}://{Request.Host}";

                var results = _repository.Create_Stream(baseUrl, Incomming_Request);

                return Ok(results);
            }

        }

    }
}
