using Microsoft.AspNetCore.Mvc;

namespace PrimeiraApi.Controllers;

[ApiController]
[Route("api/minha-controller")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet("teste")]
    public IActionResult Get()
    {
        return Ok();
    }
    
    [HttpGet("{id:int}/{id2:int}")]
    public IActionResult Get2()
    {
        return Ok();
    }
}
