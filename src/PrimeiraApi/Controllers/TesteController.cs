using Microsoft.AspNetCore.Mvc;
using PrimeiraApi.Models;

namespace PrimeiraApi.Controllers;

[ApiController]
[Route("api/demo")]
public class TesteController : ControllerBase
{
    private readonly ILogger<TesteController> _logger;
    
    public TesteController(ILogger<TesteController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new Aluno { Id = 1, Nome = "Aluno"});
    }
    
    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
        return Ok(new Aluno { Id = id, Nome = "Aluno"});
    }

    [HttpPost]
    public IActionResult Create(Aluno aluno)
    {
        return CreatedAtAction("Get", new { id = aluno.Id }, aluno);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, Aluno aluno)
    {
        if (id != aluno.Id) return BadRequest();
        
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        return NoContent();
    }
    
}