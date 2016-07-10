namespace EffectiveUnitTest.Tests
{
    public class a
    {
        public static CustomerBuilder customer = new CustomerBuilder();
        public static RentalBuilder rental = new RentalBuilder();
        public static MovieBuilder movie = new MovieBuilder();

        public class CustomerBuilder
        {
            private readonly Rental[] rentals;
            private readonly string name;

            internal CustomerBuilder() : this("Jim", new Rental[0])
            {
            }

            private CustomerBuilder(string name, Rental[] rentals)
            {
                this.name = name;
                this.rentals = rentals;
            }

            public CustomerBuilder w(params RentalBuilder[] builders)
            {
                var rentals =
                    new Rental[builders.Length];

                for (var i = 0; i < builders.Length; i++)
                {
                    rentals[i] = builders[i].build();
                }
                return new CustomerBuilder(name, rentals);
            }

            public CustomerBuilder w(string name)
            {
                return
                    new CustomerBuilder(name, rentals);
            }

            public Customer build()
            {
                var result = new Customer(name);
                foreach (var rental in rentals)
                {
                    result.AddRental(rental);
                }
                return result;
            }
        }

        public class RentalBuilder
        {
            private readonly Movie movie;
            private readonly int days;

            internal RentalBuilder() : this(new MovieBuilder().build(), 3)
            {
            }

            private RentalBuilder(Movie movie, int days)
            {
                this.movie = movie;
                this.days = days;
            }

            public RentalBuilder w(MovieBuilder movie)
            {
                return
                    new RentalBuilder(
                        movie.build(), days);
            }

            public Rental build()
            {
                return new Rental(movie, days);
            }
        }

        public class MovieBuilder
        {
            private readonly string name;
            private readonly Movie.Type type;

            internal MovieBuilder() : this("Godfather 4", Movie.Type.NEW_RELEASE)
            {
            }

            private MovieBuilder(string name, Movie.Type type)
            {
                this.name = name;
                this.type = type;
            }

            public MovieBuilder w(Movie.Type type)
            {
                return new MovieBuilder(name, type);
            }

            public MovieBuilder w(string name)
            {
                return new MovieBuilder(name, type);
            }

            public Movie build()
            {
                return new Movie(name, type);
            }
        }
    }
}