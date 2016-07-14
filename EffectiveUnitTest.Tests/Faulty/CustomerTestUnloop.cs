using System;
using System.Collections.Generic;
using EffectiveUnitTest.Faulty;
using Xunit;

namespace EffectiveUnitTest.Tests.Faulty
{
    /// <summary>
    /// First simplification step = unwind the loop
    /// </summary>
    public class CustomerTestUnloop
    {
        public CustomerTestUnloop()
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

        private static string RentalInfo(
            string startsWith,
            string endsWith,
            List<Rental> rentals)
        {
            var result = "";
            foreach (var rental in rentals)
                result += $"{startsWith}{rental.Movie.Title}\t{rental.Charge}{endsWith}\n";
            return result;
        }

        private static string ExpStatement(
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
        public void DavidStatement()
        {
            Assert.Equal(
                ExpStatement(
                    "Rental record for {0}\n" +
                    "{1}" +
                    "Amount owed is {2}\n" +
                    "You earned {3} frequent renter points",
                    _david,
                    RentalInfo("\t", "", _david.Rentals)),
                _david.Statement);
        }

        [Fact]
        public void JohnStatement()
        {
            Assert.Equal(
                ExpStatement(
                    "Rental record for {0}\n" +
                    "{1}" +
                    "Amount owed is {2}\n" +
                    "You earned {3} frequent renter points",
                    _john,
                    RentalInfo("\t", "", _john.Rentals)),
                _john.Statement);
        }

        [Fact]
        public void PatStatement()
        {
            Assert.Equal(
                ExpStatement(
                    "Rental record for {0}\n" +
                    "{1}" +
                    "Amount owed is {2}\n" +
                    "You earned {3} frequent renter points",
                    _pat,
                    RentalInfo("\t", "", _pat.Rentals)),
                _pat.Statement);
        }

        [Fact]
        public void SteveStatement()
        {
            Assert.Equal(
                ExpStatement(
                    "Rental record for {0}\n" +
                    "{1}" +
                    "Amount owed is {2}\n" +
                    "You earned {3} frequent renter points",
                    _steve,
                    RentalInfo("\t", "", _steve.Rentals)),
                _steve.Statement);
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
            foreach (var customer in _customers)
            {
                Assert.Equal(
                    ExpStatement(
                        "<h1>Rental record for <em>{0}</em></h1>\n" +
                        "{1}" +
                        "<p>Amount owed is <em>{2}</em></p>\n" +
                        "<p>You earned <em>{3} frequent renter points</em></p>",
                        customer,
                        RentalInfo("<p>", "</p>", customer.Rentals)),
                    customer.HtmlStatement);
            }
        }

        [Fact]
        public void InvalidTitle()
        {
            Assert.Throws<ArgumentException>(
                () =>
                    ObjectMother
                    .CustomerWithNoRentals("Bob")
                    .AddRental(
                        new Rental(
                            new Movie("Crazy, Stupid, Love.",
                                      Movie.Type.UNKNOWN),
                            4))
                );
        }
    }
}