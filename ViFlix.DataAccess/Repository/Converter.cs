using System;
using Common.Models.Domain;

namespace ViFlix.DataAccess.Repository
{
    public class Converter
    {
        public static Customer ToModelCustomer(Entities.Customer dbCustomer)
        {
            return new Customer
            {
                Id = dbCustomer.Id,
                Name = dbCustomer.Name,
                Birthday = dbCustomer.Birthday,
                IsSubscribedToNewsLetter = dbCustomer.IsSubscribedToNewsLetter,
                MembershipTypeId = dbCustomer.MembershipTypeId,
                MembershipType = ToModelMembershipType(dbCustomer.MembershipType)
            };
        }

        public static Entities.Customer ToEntityCustomer(Customer customer)
        {
            return new Entities.Customer
            {
                Name = customer.Name,
                MembershipType = ToEntityMembershipType(customer.MembershipType),
                Birthday = customer.Birthday,
                IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter,
                MembershipTypeId = customer.MembershipTypeId
            };
        }

        public static MembershipType ToModelMembershipType(Entities.MembershipType membershipType)
        {
            return new MembershipType
            {
                Id = membershipType.Id,
                Name = membershipType.Name,
                DiscountRate = membershipType.DiscountRate,
                DurationInMonths = membershipType.DurationInMonths,
                SignUpFee = membershipType.SignUpFee
            };
        }

        public static Entities.MembershipType ToEntityMembershipType(MembershipType membershipType)
        {
            return new Entities.MembershipType
            {
                Name = membershipType.Name,
                DiscountRate = membershipType.DiscountRate,
                DurationInMonths = membershipType.DurationInMonths,
                SignUpFee = membershipType.SignUpFee
            };
        }

        public static Movie ToModelMovie(Entities.Movie dbMovie)
        {
            return new Movie()
            {
                Id = dbMovie.Id,
                Name = dbMovie.Name,
                NumberInStock = dbMovie.NumberInStock,
                ReleasedDate = dbMovie.ReleasedDate,
                NumberAvailable = dbMovie.NumberAvailable,
                DateAdded = dbMovie.DateAdded ?? DateTime.Today,
                Genre = (Genre)dbMovie.Genre
            };
        }

        public static Entities.Movie ToEntityMovie(Movie movie)
        {
            return new Entities.Movie
            {
                Name = movie.Name,
                DateAdded = movie.DateAdded,
                ReleasedDate = movie.ReleasedDate,
                NumberAvailable = movie.NumberAvailable,
                NumberInStock = movie.NumberInStock,
                Genre = (Entities.Genre)movie.Genre
            };
        }

        public static Rental ToModelRental(Entities.Rental dbRental)
        {
            return new Rental()
            {
                Id = dbRental.Id,
                Customer = ToModelCustomer(dbRental.Customer),
                Movie = ToModelMovie(dbRental.Movie),
                DateRented = dbRental.DateRented,
                DateReturned = dbRental.DateReturned,
                DateToBeReturned = dbRental.DateToBeReturned
            };
        }

        public static Entities.Rental ToEntityRental(Rental rental)
        {
            return new Entities.Rental()
            {
                Customer = ToEntityCustomer(rental.Customer),
                Movie = ToEntityMovie(rental.Movie),
                DateRented = rental.DateRented,
                DateReturned = rental.DateReturned,
                DateToBeReturned = rental.DateToBeReturned
            };
        }
    }
}