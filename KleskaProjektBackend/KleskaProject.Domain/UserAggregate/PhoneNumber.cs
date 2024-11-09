using KleskaProject.Domain.Common.Models;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace KleskaProject.Domain.UserAggregate
{
    public class PhoneNumber : Entity<Guid>
    {

        public Guid UserDetailsId { get; set; }
        public string CountryCode { get; set; }
        public string Number { get; set; }

        [JsonConstructor]
        public PhoneNumber() : base(Guid.NewGuid()) { }

        public PhoneNumber(string countryCode, string number) : base(Guid.NewGuid())
        {
            if (string.IsNullOrWhiteSpace(countryCode))
                throw new ArgumentException("Country code cannot be empty.");
            if (string.IsNullOrWhiteSpace(number) || !IsValidPhoneNumber(number))
                throw new ArgumentException("Invalid phone number.");

            CountryCode = countryCode;
            Number = number;
        }


        private bool IsValidPhoneNumber(string number)
        {
            return Regex.IsMatch(number, @"^\d{9}$");
        }

        public override string ToString()
        {
            return $"+{CountryCode} {Number}";
        }
    }
}