﻿using LibraryManagement_API.DTO.DeSerializers.Stream;
using LibraryManagement_API.DTO.Serializers;
using LibraryManagement_API.Error_Handling.Custom_Exception_Setup;
using LibraryManagement_API.Models;
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

        private readonly MyDbContext _appDbContext;

        public StreamController(ILogger<StreamController> logger , IRepositoryStream repository, MyDbContext appDbContext) 
        {
            _logger = logger;
            _repository = repository;
            _appDbContext = appDbContext;
        }



        [HttpGet(Name = "GetStreamNames")]
        public IActionResult GetStreamNames()
        {
            string baseUrl = $"{Request.Scheme}://{Request.Host}";

            try
            {
                var results = _repository.GetAll_Stream(baseUrl);

                return Ok(results);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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

                try
                {
                    var results = _repository.Create_Stream(baseUrl, Incomming_Request);

                    return Ok(results);
                }
                catch(Exception)
                {
                    throw new Exception("This is a Generic Exception");
                }
            }
        }



        [HttpPut("{id:int}", Name = "UpdateStreamName")]
        public IActionResult UpdateStreamName(int id, [FromBody] UpdateStreamDTO Incomming_Request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                string baseUrl = $"{Request.Scheme}://{Request.Host}";
                var results = _repository.Update_Stream(baseUrl, id, Incomming_Request);

                return Ok(results);
            }

            throw new Exception("This is a Generic Exception");
        }





        [HttpGet("{Stream_Name}", Name = "Check_StreamName")]
        public IActionResult Check_StreamName(string Stream_Name)
        {
            if (string.IsNullOrEmpty(Stream_Name))
            {
                throw new CustomApiException("Stream name cannot be null or empty.", 400);
            }

            // Case-insensitive comparison
            bool Stream_Name_Existence = _appDbContext.Streams
                .Any(stream => stream.Name.ToLower() == Stream_Name.ToLower());

            if (Stream_Name_Existence)
            {
                return Ok(new { Message = "Stream name exists.", Exists = true });
            }
            else
            {
                return NotFound(new { Message = "Stream name does not exist.", Exists = false });
            }
        }





    }
}
