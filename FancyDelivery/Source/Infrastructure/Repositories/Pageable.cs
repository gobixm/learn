using System;
using System.Collections.Generic;

namespace Infrastructure.Repositories
{
    public class Pageable<T>
        where T : class
    {
        public Pageable(int total, int pageNumber, int pageSize, IList<T> items)
        {
            Total = total;
            PageNumber = pageNumber;
            PageSize = pageSize;
            Items = items;
        }

        public int Total { get; private set; }
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public IList<T> Items { get; private set; }
    }
}
