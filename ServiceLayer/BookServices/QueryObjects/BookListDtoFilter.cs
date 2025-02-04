﻿// Copyright (c) 2020 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace ServiceLayer.BookServices.QueryObjects
{
    public enum BooksFilterBy
    {
        [Display(Name = "All")] NoFilter = 0,
        [Display(Name = "By Votes...")] ByVotes,
        [Display(Name = "By Categories...")] ByTags,
        [Display(Name = "By Year published...")]
        ByPublicationYear
    }

    public static class BookListDtoFilter
    {
        public const string AllBooksNotPublishedString = "Coming Soon";

        public static IQueryable<BookListDto> FilterBooksBy(
            this IQueryable<BookListDto> books,
            BooksFilterBy filterBy, string filterValue) //#A
        {
            if (string.IsNullOrEmpty(filterValue)) //#B
                return books; //#B

            switch (filterBy)
            {
                case BooksFilterBy.NoFilter:                       //#C
                    return books;                                  //#C
                case BooksFilterBy.ByVotes:
                    var filterVote = int.Parse(filterValue);       //#D
                    return books.Where(x =>                        //#D
                        x.ReviewsAverageVotes > filterVote);       //#D
                case BooksFilterBy.ByTags:
                    return books.Where(x => x.TagStrings  //#E
                        .Any(y => y == filterValue)); //#E
                case BooksFilterBy.ByPublicationYear:
                    if (filterValue == AllBooksNotPublishedString) //#F
                        return books.Where(                        //#F
                            x => x.PublishedOn > DateTime.UtcNow); //#F

                    var filterYear = int.Parse(filterValue);       //#G
                    return books.Where(                            //#G
                        x => x.PublishedOn.Year == filterYear      //#G
                             && x.PublishedOn <= DateTime.UtcNow); //#G
                default:
                    throw new ArgumentOutOfRangeException
                        (nameof(filterBy), filterBy, null);
            }
        }

		/***************************************************************
        #A The method is given both the type of filter and the user selected filter value
        #A В метод передается тип фильтра и значение фильтра, выбранное пользователем
        #B If the filter value isn't set then it returns the IQueryable with no change
        #B Если значение фильтра не задано, возвращаем IQueryable без изменений
        #C Same for no filter selected - it returns the IQueryable with no change
        #C Если фильтр не выбран, возвращаем IQueryable без изменений
        #D The filter by votes is a value and above, e.g. 3 and above. Note: not reviews returns null, and the test is always false
        #D Фильтр по голосам возвращает только книги со средней оценкой выше значения filterVote. Если на книгу нет отзывов, то свойство ReviewsAverageVotes будет иметь
        #D значение null, а проверка всегда возвращать false.
        #E This will select any books that have a Tag category that matches the filterValue
        #E Выбирает любые книги с категорией Tag, соответствующей filterValue
        #F If the "coming soon" was picked then we only return books not yet published
        #F Если был выбран вариант "Готовится к выходу", возвращаются только те книги, которые ещё не опубликованы
        #G If we have a specific year we filter on that. Note that we also remove future books (in case the user chose this year's date)
        #G Если у нас есть конкретный год, то фильтруем по нему. Обратите внимание, что мы так же удаляем будущие книги (если пользователь выбрал дату в этом году).
         * ************************************************************/
	}
}