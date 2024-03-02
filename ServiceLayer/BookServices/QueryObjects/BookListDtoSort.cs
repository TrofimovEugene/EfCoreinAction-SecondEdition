// Copyright (c) 2020 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ServiceLayer.BookServices.QueryObjects
{
    public enum OrderByOptions
    {
        [Display(Name = "sort by...")] SimpleOrder = 0,
        [Display(Name = "Votes ↑")] ByVotes,
        [Display(Name = "Publication Date ↑")] ByPublicationDate,
        [Display(Name = "Price ↓")] ByPriceLowestFirst,
        [Display(Name = "Price ↑")] ByPriceHigestFirst
    }

    public static class BookListDtoSort
    {
        public static IQueryable<BookListDto> OrderBooksBy
        (this IQueryable<BookListDto> books,
            OrderByOptions orderByOptions)
        {
            switch (orderByOptions)
            {
                case OrderByOptions.SimpleOrder: //#A
                    return books.OrderByDescending( //#A
                        x => x.BookId); //#A
                case OrderByOptions.ByVotes: //#B
                    return books.OrderByDescending(x => //#B
                        x.ReviewsAverageVotes); //#B
                case OrderByOptions.ByPublicationDate: //#C
                    return books.OrderByDescending( //#C
                        x => x.PublishedOn); //#C
                case OrderByOptions.ByPriceLowestFirst: //#D
                    return books.OrderBy(x => x.ActualPrice); //#D
                case OrderByOptions.ByPriceHigestFirst: //#D
                    return books.OrderByDescending( //#D
                        x => x.ActualPrice); //#D
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(orderByOptions), orderByOptions, null);
            }
        }

        /************************************************************
        #A Because of paging we always need to sort. I default to showing latest entries first
        #A Из-за разбиения на страницы всегда нужно выполнить сортировку. По умолчанию сортировка выполняется по первичному ключу. Это делается быстро.
        #B This orders the book by votes. Books without any votes (null return) go at the bottom
        #B Упорядочение книг в зависимости от голосов. Книг, у которых нет голосов (возвращается null) идут внизу.
        #C Order by publication date - latest books at the top
        #C Упорядочивание книг по дате публикации, вверху - самые свежие книги.
        #D Order by actual price, which takes into account any promotional price - both lowest first and highest first
        #D Упорядочение книг по актуальной цене с учетом рекламной цены (по возврастанию и по убыванию цены)
         * ********************************************************/
    }
}