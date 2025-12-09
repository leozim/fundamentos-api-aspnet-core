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
    [ProducesResponseType(typeof(Aluno), StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        return Ok(new Aluno { Id = 1, Nome = "Aluno"});
    }
    
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Aluno), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Get(int id)
    {
        return Ok(new Aluno { Id = id, Nome = "Aluno"});
    }

    [HttpPost]
    [ProducesResponseType(typeof(Aluno), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create(Aluno aluno)
    {
        return CreatedAtAction("Get", new { id = aluno.Id }, aluno);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(Aluno), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Update(int id, Aluno aluno)
    {
        if (id != aluno.Id) return BadRequest();
        
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        return NoContent();
    }
    
}