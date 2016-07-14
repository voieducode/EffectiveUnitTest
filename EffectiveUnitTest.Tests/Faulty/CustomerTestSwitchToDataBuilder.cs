using System;
using System.Collections.Generic;
using EffectiveUnitTest.Faulty;
using Xunit;

namespace EffectiveUnitTest.Tests.Faulty
{
    /// <summary>
    ///     Third simplification step = inline
    /// </summary>
    public class CustomerTestSwitchToDataBuilder
    {
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
        public void AllRentalTypesStatement()
        {
            Assert.Equal(
                "Rental record for Pat\n" +
                "\tGodfather 4\t9.0\n" +
                "\tScarface\t3.5\n" +
                "\tLion King\t1.5\n" +
                "Amount owed is 14.0\n" +
                "You earned 4 frequent renter points",
                a.customer
                    .w("Pat").w(
                        a.rental.w(
                            a.movie
                            .w(Movie.Type.NEW_RELEASE)),
                        a.rental.w(
                            a.movie
                            .w("Scarface")
                            .w(Movie.Type.REGULAR)),
                       a.rental.w(
                            a.movie
                            .w("Lion King")
                            .w(Movie.Type.CHILDREN))).build()
                .Statement);
        }

        [Fact]
        public void GetName()
        {
            Assert.Equal("David", ObjectMother.CustomerWithNoRentals(
                "David").Name);
            Assert.Equal("John", ObjectMother.CustomerWithOneNewRelease(
                "John").Name);
            Assert.Equal("Steve", ObjectMother.CustomerWithOneNewReleaseAndOneRegular(
                "Steve").Name);
            Assert.Equal("Pat", ObjectMother.CustomerWithOneOfEachRentalType(
                "Pat").Name);
        }

        [Fact]
        public void HtmlStatement()
        {
            var customers = new[]
            {
                ObjectMother.CustomerWithNoRentals(
                    "David"),
                ObjectMother.CustomerWithOneNewRelease(
                    "John"),
                ObjectMother.CustomerWithOneNewReleaseAndOneRegular(
                    "Steve"),
                ObjectMother.CustomerWithOneOfEachRentalType(
                    "Pat")
            };

            foreach (var customer in customers)
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

        [Fact]
        public void NewReleaseAndRegularStatement()
        {
            Assert.Equal(
                "Rental record for Steve\n" +
                "\tGodfather 4\t9.0\n" +
                "\tScarface\t3.5\n" +
                "Amount owed is 12.5\n" +
                "You earned 3 frequent renter points",
                a.customer
                    .w("Steve").w(
                    a.rental.w(
                        a.movie
                        .w(Movie.Type.NEW_RELEASE)),
                    a.rental.w(
                        a.movie
                        .w("Scarface")
                        .w(Movie.Type.REGULAR))).build()
                .Statement);
        }

        [Fact]
        public void NoRentalsStatement()
        {
            Assert.Equal(
                "Rental record for David\n" +
                "Amount owed is 0\n" +
                "You earned 0 frequent renter points",
                a.customer
                 .w("David").build()
                 .Statement);
        }

        [Fact]
        public void OneNewReleaseStatement()
        {
            Assert.Equal(
                "Rental record for John\n" +
                "\tGodfather 4\t9.0\n" +
                "Amount owed is 9.0\n" +
                "You earned 2 frequent renter points",
                a.customer
                    .w("John").w(
                    a.rental.w(
                        a.movie
                            .w(Movie.Type.NEW_RELEASE))).build()
                .Statement);
        }
    }
}