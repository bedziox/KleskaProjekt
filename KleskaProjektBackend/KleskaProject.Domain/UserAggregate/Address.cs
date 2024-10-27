using System.Text.RegularExpressions;

namespace KleskaProject.Domain.UserAggregate
{
    public partial class Address
    {
        public string Street { get; }
        public string City { get; }
        public string State { get; }
        public string PostalCode { get; }
        public string Country { get; }

        public Address(string street, string city, string state, string postalCode, string country)
        {
            if (string.IsNullOrWhiteSpace(street))
                throw new ArgumentException("Street cannot be empty.", nameof(street));
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City cannot be empty.", nameof(city));
            if (string.IsNullOrWhiteSpace(state))
                throw new ArgumentException("State cannot be empty.", nameof(state));
            if (string.IsNullOrWhiteSpace(postalCode) || !IsValidPostalCode(postalCode))
                throw new ArgumentException("Invalid postal code.", nameof(postalCode));
            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentException("Country cannot be empty.", nameof(country));

            Street = street;
            City = city;
            State = state;
            PostalCode = postalCode;
            Country = country;
        }
        private bool IsValidPostalCode(string postalCode)
        {
            return Regex.IsMatch(postalCode, @"^\\d{2}[- ]{0,1}\\d{3}$");
        }

        public override string ToString()
        {
            return $"{Street}, {City}, {State} {PostalCode}, {Country}";
        }

    }
}