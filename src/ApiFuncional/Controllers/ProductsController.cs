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
    public async Task<ActionResult<IEnumerable<Product>>> Get()
    {
        return await _context.Products.ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> Get(int id)
    {
        return await _context.Products.FindAsync(id);
    }
    
    [HttpPost]
    public async Task<ActionResult<Product>> Post(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        
        // retorna um 201Created e gera um link dando a opção para o user acessar o recurso através da API
        return CreatedAtAction("Get", new Product {Id = product.Id}, product);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, Product product)
    {
        var productToUpdate = await _context.Products.FindAsync(id);
        if (productToUpdate?.Id != product.Id)
        {
            return NotFound();
        }
        
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var productToDelete = await _context.Products.FindAsync(id);
        if (productToDelete == null)
        {
            return NotFound();
        }
        
        _context.Products.Remove(productToDelete);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}