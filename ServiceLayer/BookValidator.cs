using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Model;
using FluentValidation;

namespace ServiceLayer
{
    public class BookValidator: AbstractValidator<Book>
    {
        public BookValidator()
        {

            RuleFor(book => book).Must( book => book.Id > 0)
                                 .WithMessage("Invalid Id, Id should be a positive number.");
            RuleFor(book => book).Must( book => book.Price > 0)
                                 .WithMessage("Invalid Price, Price should be a positive number.");

            RuleFor(book => book.Name).NotNull().WithMessage("Name should not be Null").DependentRules(() =>
            {
                RuleFor(book => book).Must(book => book.Name.Length > 0)
                                     .WithMessage("Name should not be Empty");
                RuleFor(book => book).Must(book => book.Name.All(x => char.IsLetter(x) || x == ' '))
                                     .WithMessage("Book Name Should Have only Characters");
            });
            RuleFor(book => book.Category).NotNull().WithMessage("Category should not be Null").DependentRules(() =>
            {
                RuleFor(book => book).Must(book => book.Category.Length > 0)
                                     .WithMessage("Category should not be Empty");
                RuleFor(book => book).Must(book => book.Category.All(x => char.IsLetter(x) || x == ' '))
                                     .WithMessage("Book Category Should Have only Characters");
            });
            RuleFor(book => book.Author).NotNull().WithMessage("Author should not be Null").DependentRules(() =>
            {
                RuleFor(book => book).Must(book => book.Author.Length > 0)
                                     .WithMessage("Author should not be Empty");
                RuleFor(book => book).Must(book => book.Author.All(x => char.IsLetter(x) || x == ' '))
                                     .WithMessage("Book Author Should Have only Characters");
            });

        }
    }
}
