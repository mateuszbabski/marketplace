namespace Application.Features.Customers.GetCustomerDetails
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }    
        public string LastName { get; set; }
        public string TelephoneNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }  
        public string PostalCode { get; set; }
    
    }
}
