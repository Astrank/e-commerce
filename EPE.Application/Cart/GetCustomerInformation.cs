using EPE.Application.Infrastructure;

namespace EPE.Application.Cart
{
    public class GetCustomerInformation
    {
        private readonly ISessionManager _sessionManager;
        public GetCustomerInformation(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
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
            var customerInfo = _sessionManager.GetCustomerInformation();

            if (customerInfo == null)
            {
                return null;
            }

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