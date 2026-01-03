using System;
using System.Collections.Generic;
using System.Linq;

namespace Advantage.API
{
    public class PaginatedResponse<T>
    {
        public PaginatedResponse(IEnumerable<T> data, int pageIndex, int pageSize)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            
            if (pageIndex < 1)
                throw new ArgumentException("Page index must be greater than 0", nameof(pageIndex));
            
            if (pageSize < 1)
                throw new ArgumentException("Page size must be greater than 0", nameof(pageSize));

            var dataList = data.ToList();
            Total = dataList.Count;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(Total / (double)pageSize);
            
            Data = dataList.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public int Total { get; }
        public int PageIndex { get; }
        public int PageSize { get; }
        public int TotalPages { get; }
        public IEnumerable<T> Data { get; }
    }
}