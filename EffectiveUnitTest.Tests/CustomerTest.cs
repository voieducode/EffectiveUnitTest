using System;
using System.Collections.Generic;
using Xunit;

namespace EffectiveUnitTest.Tests
{
    public class CustomerTest
    {
        public CustomerTest()
        {
            _david = ObjectMother.CustomerWithNoRentals(
                _davidName);
            _john = ObjectMother.CustomerWithOneNewRelease(
                _johnName);
            _pat = ObjectMother.CustomerWithOneOfEachRentalType(
                _patName);
            _steve = ObjectMother.CustomerWithOneNewReleaseAndOneRegular(
                _steveName);
            _customers = new[] {_david, _john, _steve, _pat};
        }

        private readonly Customer _john;
        private readonly Customer _steve;
        private readonly Customer _pat;
        private readonly Customer _david;

        private readonly string _johnName = "John";

        private readonly string _steveName = "Steve";

        private readonly string _patName = "Pat";

        private readonly string _davidName = "David";

        private readonly Customer[] _customers;

        public static string RentalInfo(
            string startsWith,
            string endsWith,
            List<Rental> rentals)
        {
            var result = "";
            foreach (var rental in rentals)
                result += $"{startsWith}{rental.Movie.Title}\t{rental.Charge}{endsWith}\n";
            return result;
        }

        public static string ExpStatement(
            string formatStr,
            Customer customer,
            string rentalInfo)
        {
            return string.Format(
                formatStr,
                customer.Name,
                rentalInfo,
                customer.TotalCharge,
                customer.TotalPoints);
        }

        [Fact]
        public void GetName()
        {
            Assert.Equal(_davidName, _david.Name);
            Assert.Equal(_johnName, _john.Name);
            Assert.Equal(_steveName, _steve.Name);
            Assert.Equal(_patName, _pat.Name);
        }

        [Fact]
        public void HtmlStatement()
        {
            for (var i = 0; i < _customers.Length; i++)
            {
                Assert.Equal(
                    ExpStatement(
                        "<h1>Rental record for " +
                        "<em>{0}</em></h1>\n{1}" +
                        "<p>Amount owed is <em>{2}</em>" +
                        "</p>\n<p>You earned <em>{3}" +
                        " frequent renter points</em></p>",
                        _customers[i],
                        RentalInfo(
                            "<p>", "</p>",
                            _customers[i].Rentals)),
                    _customers[i].HtmlStatement);
            }
        }

        [Fact]
        public void InvalidTitle()
        {
            Assert.Throws<ArgumentException>(() =>
                ObjectMother
                    .CustomerWithNoRentals("Bob")
                    .AddRental(
                        new Rental(
                            new Movie("Crazy, Stupid, Love.",
                                Movie.Type.UNKNOWN),
                            4)));
        }

        [Fact]
        public void Statement()
        {
            foreach (var customer in _customers)
            {
                Assert.Equal(
                    ExpStatement(
                        "Rental record for {0}\n" +
                        "{1}Amount owed is {2}\n" +
                        "You earned {3} frequent " +
                        "renter points",
                        customer,
                        RentalInfo("\t", "", customer.Rentals)),
                    customer.Statement);
            }
        }
    }
}