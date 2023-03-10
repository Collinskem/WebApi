using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dtos;
using WebApi.Models;
using WebApi.services;

namespace WebApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(AppDbContext context, IProductService productService, IMapper mapper)
        {
            _context = context;
            _productService = productService;
            _mapper = mapper;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productService.GetAllProducts();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5


        //[Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, [FromBody] ProductDto productDto)
        {
            var product = _productService.GetProduct(id);
            if (product != null)
            {
                var result = _mapper.Map<Product>(productDto);
                await _productService.PutProduct(id, result);
                return Ok(result);
            }
            return BadRequest();

            
        }

        // POST: api/Products
        [Authorize (Roles =UserRoles.Admin)]
        [HttpPost]
        public ActionResult<Product> PostProduct([FromBody]ProductDto product)
        {
            if (product != null)
            {
                var p = _mapper.Map<Product>(product);
                var result = _productService.CreateProduct(p);
                
                return CreatedAtAction(nameof(GetProduct), new { id = result.id }, result);
            }
            return BadRequest();
           
        }
        //[Authorize(Roles = UserRoles.Admin)]

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var p = await _productService.GetProduct(id);
            if (p != null)
            {
                return Ok(_productService.DeleteProduct(id));
            }
            return NotFound();
           
        }

        //private bool ProductExists(int id)
        //{
        //    return _context.Products.Any(e => e.id == id);
        //}
    }
}
