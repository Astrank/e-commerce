using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EPE.Domain.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace EPE.Application.Cart
{
    public class GetCustomerInformation
    {
        private ISession _session;
        public GetCustomerInformation(ISession session)
        {
            _session = session;
        }

        public class Response
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }

            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }
        }

        public Response Do()
        {
            var stringObject = _session.GetString("customer-info");

            if (String.IsNullOrEmpty(stringObject))
            {
                return null;
            }

            var customerInfo = JsonConvert.DeserializeObject<CustomerInformation>(stringObject);

            return new Response
            {
                FirstName = customerInfo.FirstName,
                LastName = customerInfo.LastName,
                Email = customerInfo.Email,
                PhoneNumber = customerInfo.PhoneNumber,
                Address1 = customerInfo.Address1,
                Address2 = customerInfo.Address2,
                City = customerInfo.City,
                PostCode = customerInfo.PostCode
            };
        }
    }   
}