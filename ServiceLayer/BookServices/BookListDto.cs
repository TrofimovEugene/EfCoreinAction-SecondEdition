// Copyright (c) 2020 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System;

namespace ServiceLayer.BookServices
{
    public class BookListDto
    {
        public int BookId { get; set; } //#A
        public string Title { get; set; }
        public DateTime PublishedOn { get; set; } //#B
        public decimal Price { get; set; } //#C

        public decimal
            ActualPrice { get; set; } //#D

        public string
            PromotionPromotionalText { get; set; } //#E

        public string AuthorsOrdered { get; set; } //#F

        public int ReviewsCount { get; set; } //#G

        public double?
            ReviewsAverageVotes { get; set; } //#H

        public string[] TagStrings { get; set; } //#I
		/******************************************************
        #A I need the Primary Key if the customer clicks the entry to buy the book
        #A Вам понадобится первичный ключ, если клиент нажимает на запись, чтобы купить книгу
        #B While the publish date isn't shown we will want to sort by it, so we have to include it
        #B Хотя дата публикации не отображается, нужно будет выполнить сортировку по ней, поэтому надо включить её
        #C This is the normal Price
        #C Обычная цена книги
        #D This is the selling price - either the normal price, or the promotional.NewPrice if present
        #D Цена - обычная или promotional.NewPrice, если есть
        #E The promotional text to show if there is a new price
        #E Рекламный текст показывающий, есть ли новая цена
        #F An array of the authors' names in the right order
        #F Строка для хранения списка авторов, разделённых запятыми
        #G The number of people who reviewed the book
        #G Кол-во людей, оставивших отзывы по книге
        #H The average of all the Votes - null if no votes
        #H Среднее значение всех голосов либо null, если голосов нет
        #I The Tag names (that is the categories) for this book
        #I Тэги (т.е. категории) для этой книги
         * ***************************************************/
	}
}