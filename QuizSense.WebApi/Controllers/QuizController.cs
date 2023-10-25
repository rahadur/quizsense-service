using Microsoft.AspNetCore.Mvc;
using QuizSense.Application.Dtos;
using QuizSense.Application.Services;

namespace QuizSense.WebApi.Controllers;

[Route("api/[controller]")]
public class QuizController : Controller
{
	private readonly IQuizService quizService;

	public QuizController(IQuizService quizService)
    {
        this.quizService = quizService;
    }

    [HttpGet]
    public IEnumerable<QuizResponse> Get()
    {
        return quizService.GetAll().ToList();
    }


    [HttpGet("{id}")]
    public ActionResult Get(int id)
    {
        var quiz = quizService.GetById(id);
        if (quiz != null)
        {
            return Ok(quiz);
        }
        return NotFound();
    }


    [HttpPost]
    public ActionResult Post([FromBody]QuizRequest body)
    {
		var quiz = quizService.Add(body);
        return Ok(quiz);
    }


    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody]QuizRequest body)
    {
        if (id != body.Id)
        {
            return BadRequest();
        }
        quizService.Update(body);
        return Ok(body);
    }


    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var quiz = quizService.GetById(id);
        if(quiz == null)
        {
            return NotFound();
        }
		quizService.Delete(id);
		return Ok();
	}
}

