using System;
using System.Collections.Generic;
using System.Linq;

namespace EffectiveUnitTest.Fixed
{
    public class Customer
    {
        public Customer(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public List<Rental> Rentals { get; } = new List<Rental>();

        public void AddRental(Rental rental)
        {
            Rentals.Add(rental);
        }

        public string Statement
        {
            get
            {
                var result =
                    "Rental record for " + Name + "\n";

                foreach (var rental in Rentals)
                {
                    result +=
                        "\t" + rental.LineItem + "\n";
                }

                result +=
                    "Amount owed is " + TotalCharge.ToString("F1") +
                    "\n" + "You earned " +
                    TotalPoints +
                    " frequent renter points";
                return result;
            }
        }

        public string HtmlStatement
        {
            get
            {
                var result =
                    "<h1>Rental record for <em>" +
                    Name + "</em></h1>\n";

                foreach (var rental in Rentals)
                {
                    result += "<p>" + rental.LineItem +
                              "</p>\n";
                }

                result +=
                    "<p>Amount owed is <em>" +
                    TotalCharge.ToString("F1") + "</em></p>\n" +
                    "<p>You earned <em>" +
                    TotalPoints +
                    " frequent renter points</em></p>";
                return result;
            }
        }

        public double TotalCharge => Rentals.Sum(rental => rental.Charge);

        public int TotalPoints => Rentals.Sum(rental => rental.Points);
    }

    public class Rental
    {
        public Rental(Movie movie, int daysRented)
        {
            this.Movie = movie;
            this.DaysRented = daysRented;
        }

        public Movie Movie { get; }

        public int DaysRented { get; }

        public double Charge => Movie.GetCharge(DaysRented);

        public int Points => Movie.GetPoints(DaysRented);

        public string LineItem => Movie.Title+ " " + Charge.ToString("F1");
    }

    public class Movie
    {
        public enum Type
        {
            REGULAR,
            NEW_RELEASE,
            CHILDREN,
            UNKNOWN
        }

        private Price _price;

        public Movie(string title, Type priceCode)
        {
            Title = title;
            PriceCode = priceCode;
        }

        public string Title { get; }

        private Type PriceCode
        {
            set
            {
                switch (value)
                {
                    case Type.CHILDREN:
                        _price = new ChildrensPrice();
                        break;
                    case Type.NEW_RELEASE:
                        _price = new NewReleasePrice();
                        break;
                    case Type.REGULAR:
                        _price = new RegularPrice();
                        break;
                    default:
                        throw new ArgumentException("invalid price code");
                }
            }
        }

        public double GetCharge(int daysRented)
        {
            return _price.GetCharge(daysRented);
        }

        public int GetPoints(int daysRented)
        {
            return _price.GetPoints(daysRented);
        }
    }

    public abstract class Price
    {
        public abstract double GetCharge(int daysRented);

        public virtual int GetPoints(int daysRented)
        {
            return 1;
        }
    }

    public class ChildrensPrice : Price
    {
        public override double GetCharge(int daysRented)
        {
            var amount = 1.5;
            if (daysRented > 3)
            {
                amount += (daysRented - 3)*1.5;
            }
            return amount;
        }
    }

    public class RegularPrice : Price
    {
        public override double GetCharge(int daysRented)
        {
            double amount = 2;
            if (daysRented > 2)
            {
                amount += (daysRented - 2)*1.5;
            }
            return amount;
        }
    }

    public class NewReleasePrice : Price
    {
        public override double GetCharge(int daysRented)
        {
            return daysRented*3;
        }

        public override int GetPoints(int daysRented)
        {
            if (daysRented > 1)
            {
                return 2;
            }
            return 1;
        }
    }
}