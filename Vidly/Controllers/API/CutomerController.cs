using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    public class CustomerController : ApiController
    {
        private ApplicationDbContext _db;
        public CustomerController()
        {
            _db = new ApplicationDbContext();
        }


        //Get /api/Customer
        public IHttpActionResult GetCustomers(string query = null)
        {
            var CustomerQuery = _db.Customers
                .Include(c => c.MembershipType);

            if (!string.IsNullOrWhiteSpace(query))
                CustomerQuery = CustomerQuery.Where(c => c.Name.Contains(query));

            var customerDtos = CustomerQuery
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>); // linQ expression



            return Ok(customerDtos);
        }


        //Get /api/Customer/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _db.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();
            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        //POST /api/Customer
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);

            _db.Customers.Add(customer);
            _db.SaveChanges();

            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto); // uniform resource identifire
        }

        //PUT /api/Customer/1
        [HttpPut]
        public void EditCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var cusomerInDb = _db.Customers.SingleOrDefault(c => c.Id == id);

            if (cusomerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map<CustomerDto, Customer>(customerDto, cusomerInDb); // mapped the attributes of both classes

            _db.SaveChanges();
        }

        //DELETE /Api/Customer/1
        [HttpDelete]
        public void RemoveCustomer(int id)
        {
            var customerInDb = _db.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _db.Customers.Remove(customerInDb);
            _db.SaveChanges();
        }
    }
}
