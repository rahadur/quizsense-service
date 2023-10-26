using Microsoft.AspNetCore.Mvc;
using QuizSense.Application.Dtos;
using QuizSense.Application.Services;

namespace QuizSense.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionController : ControllerBase
{
	private readonly IQuestionService questionService;

	public QuestionController(IQuestionService questionService)
	{
		this.questionService = questionService;
	}

	[HttpGet]
	public async Task<IEnumerable<QuestionResponse>> Get([FromQuery] QueryParameterDto queryParameter)
	{
		return await questionService.GetAllAsync(queryParameter);
	}


	[HttpGet("{quizId}/quiz")]
	public async Task<IEnumerable<QuestionResponse>> QuizQuestions(int quizId)
	{
		return await questionService.QuizQuestionsAsync(quizId);
	}


	[HttpGet("{id}")]
	public async Task<ActionResult<QuestionResponse>> GetById(int id)
	{
		var question = await questionService.GetByIdAsync(id);
		if (question != null)
		{
			return Ok(question);
		}
		return NotFound(new { });
	}


	[HttpPost]
	public async Task<ActionResult<QuestionResponse>> Create([FromBody] QuestionRequest body)
	{
		var newQuestion = await questionService.AddAsync(body);
		return Ok(newQuestion);
	}


	[HttpPut("{id}")]
	public async Task<ActionResult<QuestionResponse>> Update(int id, [FromBody] QuestionRequest body)
	{
		if(id != body.Id)
		{
			return BadRequest("Id dose't match with entity");
		}
		var question = await questionService.GetByIdAsync(id);
		if (question == null) return NotFound();

		return await questionService.UpdateAsync(body);
	}


	[HttpDelete("{id}")]
	public async Task<ActionResult> Delete(int id)
	{
		var question = await questionService.GetByIdAsync(id);
		if(question == null)
		{
			return NotFound();
		}
		await questionService.DeleteAsync(id);
		return Ok();

	}
}

