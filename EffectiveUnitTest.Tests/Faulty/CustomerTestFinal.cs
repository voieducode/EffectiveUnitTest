using EffectiveUnitTest.Faulty;
using Xunit;

namespace EffectiveUnitTest.Tests.Faulty
{
    public class CustomerTestFinal
    {
        [Fact]
        public void AllRentalTypesStatement()
        {
            Assert.Equal(
                "Rental record for Pat\n" +
                "\tGodfather 4 9.0\n" +
                "\tScarface 3.5\n" +
                "\tLion King 1.5\n" +
                "Amount owed is 14.0\n" +
                "You earned 4 frequent renter points",
                a.customer
                 .w("Pat")
                 .w(a.rental
                     .w(a.movie
                         .w(Movie.Type.NEW_RELEASE)),
                    a.rental
                     .w(a.movie
                         .w("Scarface")
                         .w(Movie.Type.REGULAR)),
                    a.rental
                     .w(a.movie
                         .w("Lion King")
                         .w(Movie.Type.CHILDREN))).build()
                 .Statement
                );
        }

        [Fact]
        public void GetName()
        {
            Assert.Equal("John", a.customer.w("John").build().Name);
        }

        [Fact]
        public void NewReleaseAndRegularStatement()
        {
            Assert.Equal(
                "Rental record for Steve\n" +
                "\tGodfather 4\t9\n" +
                "\tScarface 3.5\n" +
                "Amount owed is 12.5\n" +
                "You earned 3 frequent renter points",
                a.customer
                 .w("Steve")
                 .w(a.rental
                     .w(a.movie
                         .w(Movie.Type.NEW_RELEASE)),
                    a.rental
                     .w(a.movie
                         .w("Scarface")
                         .w(Movie.Type.REGULAR))).build()
                 .Statement
                );
        }

        [Fact]
        public void NoRentalsHtmlStatement()
        {
            Assert.Equal(
                "<h1>Rental record for <em>David</em></h1>\n" +
                "<p>Amount owed is <em>0.0</em></p>\n" +
                "<p>You earned <em>0 frequent renter points</em></p>",
                a.customer
                 .w("David").build()
                 .HtmlStatement
                );
        }

        [Fact]
        public void NoRentalsStatement()
        {
            Assert.Equal(
                "Rental record for David\n" +
                "Amount owed is 0.0\n" +
                "You earned 0 frequent renter points",
                a.customer
                 .w("David").build()
                 .Statement
                );
        }

        [Fact]
        public void OneNewReleaseHtmlStatement()
        {
            Assert.Equal(
                "<h1>Rental record for <em>John</em></h1>\n" +
                "<p>Godfather 4 9.0</p>\n" +
                "<p>Amount owed is <em>9.0</em></p>\n" +
                "<p>You earned <em>2 frequent renter points</em></p>",
                a.customer
                 .w("John")
                 .w(a.rental
                     .w(a.movie
                         .w(Movie.Type.NEW_RELEASE)))
                 .build()
                 .HtmlStatement
                );
        }

        [Fact]
        public void OneNewReleaseStatement()
        {
            Assert.Equal(
                "Rental record for John\n" +
                "\tGodfather 4 9.0\n" +
                "Amount owed is 9.0\n" +
                "You earned 2 frequent renter points",
                a.customer
                 .w("John")
                 .w(a.rental
                     .w(a.movie
                         .w(Movie.Type.NEW_RELEASE))).build()
                 .Statement
                );
        }
    }
}