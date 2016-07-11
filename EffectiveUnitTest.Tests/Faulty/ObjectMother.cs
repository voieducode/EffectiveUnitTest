using EffectiveUnitTest.Faulty;

namespace EffectiveUnitTest.Tests.Faulty
{
    public class ObjectMother
    {
        public static Customer CustomerWithOneOfEachRentalType(
            string name)
        {
            var result =
                CustomerWithOneNewReleaseAndOneRegular(
                    name);
            result.AddRental(
                new Rental(
                    new Movie("Lion King", Movie.Type.CHILDREN), 3));
            return result;
        }

        public static Customer CustomerWithOneNewReleaseAndOneRegular(
            string n)
        {
            var result =
                CustomerWithOneNewRelease(n);
            result.AddRental(
                new Rental(
                    new Movie("Scarface", Movie.Type.REGULAR), 3));
            return result;
        }

        public static Customer CustomerWithOneNewRelease(string name)
        {
            var result =
                CustomerWithNoRentals(name);
            result.AddRental(
                new Rental(
                    new Movie(
                        "Godfather 4", Movie.Type.NEW_RELEASE), 3));
            return result;
        }

        public static Customer CustomerWithNoRentals(string name)
        {
            return new Customer(name);
        }
    }
}