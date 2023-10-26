using Microsoft.AspNetCore.Mvc;
using QuizSense.Application.Dtos;
using QuizSense.Application.Services;

namespace QuizSense.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuizController : ControllerBase
{
	private readonly IQuizService quizService;

	public QuizController(IQuizService quizService)
    {
        this.quizService = quizService;
    }

    [HttpGet]
    public async Task<IEnumerable<QuizResponse>> Get([FromQuery] QueryParameterDto queryParameter)
    {
        return await quizService.GetAllAsync(queryParameter);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<QuizResponse>> Get(int id)
    {
        var quiz = await quizService.GetByIdAsync(id);
        if (quiz != null)
        {
            return Ok(quiz);
        }
		return NotFound(new { });
	}


    [HttpPost]
    public async Task<ActionResult<QuizResponse>> Post([FromBody]QuizRequest body)
    {
		var quiz = await quizService.AddAsync(body);
        return Ok(quiz);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<QuizResponse>> Put(int id, [FromBody]QuizRequest body)
    {
        if (id != body.Id)
        {
            return BadRequest();
        }
        var quiz = await quizService.UpdateAsync(body);
        return Ok(quiz);
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var quiz = await quizService.GetByIdAsync(id);
        if(quiz == null)
        {
            return NotFound();
        }
		await quizService.DeleteAsync(id);
		return Ok();
	}
}

