using Data.enums;
using Domain.DTOs;
using Domain.IServices;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace MediConnect_Plus.Controllers
{
    public class TestController : Controller
    {
        private readonly ITestService _TestService;

        public TestController(ITestService caseService)
        {
            _TestService = caseService;
        }


        [HttpGet("tests/{doctorUsername}")]
        public async Task<ActionResult<IEnumerable<TestDTO>>> GetTests(string doctorUsername)
        {
            var tests = await _TestService.GetTests(doctorUsername);
            return Ok(tests);
        }

        [HttpPost("test/{doctorUsername}")]
        public async Task<ActionResult<TestDTO>> AddTest(string doctorUsername, [FromBody] TestDTO testDTO)
        {
            var test = await _TestService.AddTest(doctorUsername, testDTO);
            if (test != null)
                return Ok(test);
            return BadRequest();
        }

        [HttpPut("test-status/{testId}")]
        public async Task<IActionResult> UpdateTestStatus(int testId, [FromBody] TestStatus status)
        {
            var result = await _TestService.UpdateTestStatus(testId, status);
            if (result)
                return Ok();
            return BadRequest();
        }

        [HttpDelete("test/{testId}")]
        public async Task<IActionResult> DeleteTest(int testId)
        {
            var result = await _TestService.DeleteTest(testId);
            if (result)
                return Ok();
            return BadRequest();
        }
    }
}
