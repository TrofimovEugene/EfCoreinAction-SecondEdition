// Copyright (c) 2020 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System.Linq;
using System.Threading.Tasks;
using DataLayer.EfCode;
using DataLayer.QueryObjects;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.BookServices.QueryObjects;

namespace ServiceLayer.BookServices.Concrete
{
    public class ListBooksService
    {
        private readonly EfCoreContext _context;

        public ListBooksService(EfCoreContext context)
        {
            _context = context;
        }

        public IQueryable<BookListDto> SortFilterPage
            (SortFilterPageOptions options)
        {
            var booksQuery = _context.Books //#A
                .AsNoTracking() //#B
                .MapBookToDto() //#C
                .OrderBooksBy(options.OrderByOptions) //#D
                .FilterBooksBy(options.FilterBy, //#E
                    options.FilterValue); //#E

            options.SetupRestOfDto(booksQuery); //#F

            return booksQuery.Page(options.PageNum - 1, //#G
                options.PageSize); //#G
        }
    }

	/*********************************************************
    #A This starts by selecting the Books property in the Application's DbContext 
    #A Начинает с выбора свойства Books в DbContext приложения
    #B Because this is a read-only query I add .AsNoTracking(). It makes the query faster
    #B Поскольку этот запрос с доступом только на чтения, добавляем метод .AsNoTracking(). Это сделает запрос быстрее
    #C It then uses the Select query object which will pick out/calculate the data it needs
    #C Использует объект запроса выбора, который выбирает/вычисляет нужные ему данные
    #D It then adds the commands to order the data using the given options
    #D Добавляет команды для упорядочивания данных с использованием заданных параметров
    #E Then it adds the commands to filter the data
    #E Добавляет команды для фильтрации данных
    #F This stage sets up the number of pages and also makes sure PageNum is in the right range
    #F На этом этапе настраивается количество страниц и проверяется, что PageNum находится в правильном диапазоне.
    #G Finally it applies the paging commands
    #G Применяет команды разбиения на страницы
        * *****************************************************/
}