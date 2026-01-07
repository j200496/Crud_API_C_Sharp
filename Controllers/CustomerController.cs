using Crud_Api.Dtos;
using Crud_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crud_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly UserContext _context;
        public CustomerController(UserContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<Customers>> GetCustomers()
        {
            return  await _context.Customers.ToListAsync();
        }
        [HttpGet("CustActive")]
        public async Task<IEnumerable<Customers>>GetOnlyCustomersActive()
        {
            var cust = await _context.Customers.Where(c => c.IsActive).ToListAsync();
            return cust;

        }
        [HttpGet("GetOneCust{id}")]
        public async Task<ActionResult<Customers>>GetOneCustomer(int id)
        {
            var find = await _context.Customers.FindAsync(id);
            if(find == null || find.IsActive == false)
            {
                return NotFound(new {message = "The customer is inactive or does not exist"});
            }
            return find;
        }
        [HttpPost]
        public async Task<ActionResult<Customers>> AddCustomers([FromBody] AddCustomer dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = new Customers
            {
                Name = dto.Name,
                Address = dto.Address,
                Phone = dto.Phone,
                Gender = dto.Gender,
                Age = dto.Age,
                IsActive = true
            };
            _context.Customers.Add(customer);
             await _context.SaveChangesAsync();
            return Ok("Customer added successfully!");

        }
        [HttpGet("Cantidad-por-Edad")]
        public async Task<ActionResult> GetCantiPoredad()
        {
            var datos = await _context.Customers.Where(c => c.IsActive).GroupBy(c => c.Age).Select(g => new
            {
                Edad = g.Key,
                Cantidad = g.Count()
            }).OrderBy(o => o.Edad).ToListAsync();
            return Ok(datos);
           
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] AddCustomer dto)
        {
            var findcust = await _context.Customers.FindAsync(id);
            if (findcust == null)
            {
                return NotFound();
            }
            findcust.Name = dto.Name;
            findcust.Address = dto.Address;
            findcust.Phone = dto.Phone;
            findcust.Gender = dto.Gender;
            await _context.SaveChangesAsync();
            return Ok("Customer Updated successfully");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult>DeleteCustomer(int id)
        {
            var find = await _context.Customers.FindAsync(id);
            if (find == null)
            {
                return NotFound();
            }
            find.IsActive = false;
            await _context.SaveChangesAsync();
            return Ok("Customer Deleted!");

        }

    }
}
