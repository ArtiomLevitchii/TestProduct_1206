using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestProduct.DBConnection;
using TestProduct.Models;

namespace TestProduct.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    public ProductController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("/addNewProduct")]
    public async Task<IActionResult> AddNewProduct([FromBody] Product product)
    {
        try
        {
            if(product == null)
            {
                return BadRequest("Product can't be empty");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dbContext.Products.Add(product);

            var userIp = HttpContext.Connection.RemoteIpAddress?.ToString();
     
            _dbContext.Logs.Add(new ActionLogModel { 
                Id = Guid.NewGuid(), 
                TimeStamp = DateTime.Now, 
                LogType = Enums.LogType.AddingNewProductToDatabase, 
                UserIp = $"{this.HttpContext.Connection.RemoteIpAddress}" 
            });

            await _dbContext.SaveChangesAsync();

            return Ok("Product successfully added");              
            
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");

        }
    }

    [HttpGet("GetListOfProducts")]
    public async Task<IActionResult> GetProducts()
    {
        try
        {
            var listOfProducts = await _dbContext.Products.ToListAsync();

            if (listOfProducts == null || !listOfProducts.Any())
            {
                return BadRequest("List of products is empty");
            }

            return Ok(listOfProducts);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
