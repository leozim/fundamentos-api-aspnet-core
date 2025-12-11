using ApiFuncional.Data;
using ApiFuncional.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiFuncional.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly ApiDbContext _context;

    public ProductsController(ApiDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<Product>>> Get()
    {
        if (_context.Products == null) return NotFound();
        
        return await _context.Products.ToListAsync();
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Product>> Get(int id)
    {
        if (_context.Products == null) return NotFound();
        
        var product = await _context.Products.FindAsync(id);
        
        return product == null ?  NotFound() :  product;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Product>> Post(Product product)
    {
        if (_context.Products == null) 
            return Problem("Error ao criar o produto, contete o suporte!");
        
        if (!ModelState.IsValid)
        {
            // return BadRequest(ModelState);

            // return ValidationProblem(ModelState);

            return ValidationProblem(new ValidationProblemDetails(ModelState)
            {
                Title = "Um ou mais erros de validação ocorreram!",
            });
        }
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        
        // retorna um 201Created e gera um link dando a opção para o user acessar o recurso através da API
        return CreatedAtAction("Get", new Product {Id = product.Id}, product);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Put(int id, Product product)
    {
        if (id != product.Id) return BadRequest();
        
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        
        // Abaixo minha validação
        /*
        var productToUpdate = await _context.Products.FindAsync(id);
        if (productToUpdate == null)
        {
            return NotFound();
        }
        */

        _context.Entry(product).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        
        // _context.Products.Update(product);
        
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(int id)
    {
        if (_context.Products == null) return NotFound();
        
        var productToDelete = await _context.Products.FindAsync(id);
        
        if (productToDelete == null)
        {
            return NotFound();
        }
        
        _context.Products.Remove(productToDelete);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool ProductExists(int id)
    {
        return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}