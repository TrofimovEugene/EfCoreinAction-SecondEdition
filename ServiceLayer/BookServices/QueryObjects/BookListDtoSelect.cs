// Copyright (c) 2020 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System.Linq;
using DataLayer.EfClasses;

namespace ServiceLayer.BookServices.QueryObjects
{
    public static class BookListDtoSelect
    {
        public static IQueryable<BookListDto> MapBookToDto(this IQueryable<Book> books) //#A
        {
            return books.Select(book => new BookListDto
            {
                BookId = book.BookId, //#B
                Title = book.Title, //#B
                Price = book.Price, //#B
                PublishedOn = book.PublishedOn, //#B
                ActualPrice = book.Promotion == null //#C
                    ? book.Price //#C
                    : book.Promotion.NewPrice, //#C
                PromotionPromotionalText = //#D
                    book.Promotion == null //#D
                        ? null //#D
                        : book.Promotion.PromotionalText, //#D
                AuthorsOrdered = string.Join(", ", //#E
                    book.AuthorsLink //#E
                        .OrderBy(ba => ba.Order) //#E
                        .Select(ba => ba.Author.Name)), //#E
                ReviewsCount = book.Reviews.Count, //#F
                ReviewsAverageVotes = //#G
                    book.Reviews.Select(review => //#G
                        (double?) review.NumStars).Average(), //#G
                TagStrings = book.Tags          //#H
                    .Select(x => x.TagId).ToArray(),//#H
            });
        }

		/*********************************************************
		#A This method takes in IQueryable<Book> and returns IQueryable<BookListDto>
        #A Принимает IQueryable<Book> и возвращает IQueryable<BookListDto>
        #B These are simple copies of existing columns in the Books table
        #B Простые копии существующих столбцов в таблице Book
        #C This calculates the selling price, which is the normal price, or the promotion price if that relationship exists
        #C Вычисляет обычную цену или цену по рекламной акции, если такая связь существует
        #D The PromotionalText depends on whether a PriceOffer exists for this book
        #D PromotionalText зависит от того, есть ли PriceOffer для этой книги
        #E This obtains an array of Authors' names, in the right order. We are using a Client vs. Server evaluation as we want the author's names combined into one string
        #E Получает массив имен авторов в правильном порядке. Мы используем вычисления на стороне клиента, потому что хотим, чтобы имена авторов были объеденены в одну строку.
        #F We need to calculate how many reviews there are
        #F Нужно рассчитать количество отзывов
        #G To get EF Core to turn the LINQ average into the SQL AVG command I need to cast the NumStars to (double?)
        #G Чтобы EF Core превратил Average в SQL-команду AVG, нужно преобразовать NumStars в (double?)
        #H Array of Tag names (categories) for this book
        #H Строка массива тегов (категорий) для этой книги
        * *******************************************************/
	}
}