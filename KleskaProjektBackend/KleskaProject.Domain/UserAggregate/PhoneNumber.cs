using System.Text.RegularExpressions;

namespace KleskaProject.Domain.UserAggregate
{
    public class PhoneNumber
    {
        public string CountryCode { get; }
        public string Number { get; }

        public PhoneNumber(string countryCode, string number)
        {
            if (string.IsNullOrWhiteSpace(countryCode))
                throw new ArgumentException("Country code cannot be empty.", nameof(countryCode));
            if (string.IsNullOrWhiteSpace(number) || !IsValidPhoneNumber(number))
                throw new ArgumentException("Invalid phone number.", nameof(number));

            CountryCode = countryCode;
            Number = number;
        }

        private bool IsValidPhoneNumber(string number)
        {
            return Regex.IsMatch(number, @"^\d{10}$");
        }

        public override string ToString()
        {
            return $"+{CountryCode} {Number}";
        }
    }
}