using ESP.Context;
using ESP.Request;
using ESP.Response;
using ESP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ESP.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        private readonly ProcessService _processService;

        public ProcessController(ApplicationContext context)
        {
            _processService = new ProcessService(context);
        }

        [HttpGet("{blockId}")]        
        public async Task<IActionResult> GetFilteredSubjects(int blockId) 
        {
            
            var response = new HttpSubjectTypeResponse();

            try
            {
                response = await _processService.GetFilteredSubjectsAsync(blockId);
            }
            catch(Exception exception)
            {
                response.ErrorMessage = exception.Message;
            } 

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetChecks(HttpCheckRequest request) 
        {
            var response = new HttpCheckResponse();

            try
            {
                response = await _processService.GetChecksAsync(request);
            }
            catch (Exception exception) 
            {
                response.ErrorMessage = exception.Message;
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetProcesses()
        {
            var response = new HttpProcessResponse();

            try
            {
                response = await _processService.GetProcessesAsync();
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddProcess(HttpProcessRequest request) 
        {
            var response = new HttpProcessResponse();

            try
            {
                response = await _processService.AddProcessAsync(request);
            }
            catch (Exception exception) 
            {
                response.ErrorMessage = exception.Message;
            }

            return Ok(response);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProcessById(int id)
        {
            var response = new HttpProcessResponse();

            try
            {
                response = await _processService.GetProcessByIdAsync(id);
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return Ok(response);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateProcess(HttpProcessRequest request)
        {
            var response = new HttpProcessResponse();

            try
            {
                response = await _processService.UpdateProcessAsync(request);
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcess(int id)
        {
            var response = new HttpProcessResponse();

            try
            {
                response = await _processService.DeleteProcessAsync(id);
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return Ok(response);
        }
    }
}
