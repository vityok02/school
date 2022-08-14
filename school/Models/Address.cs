namespace school.Models;

public class Address
{
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public int PostalCode { get; set; }

    public Address(string country, string city, string street, int postalCode)
    {
        Country = country;
        City = city;
        Street = street;
        PostalCode = postalCode;
    }
}
