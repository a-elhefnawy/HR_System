﻿namespace HR_System.PAL.Pagination
{
    public class AttendencePagination<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
        public List<T> Items { get; private set; }

        public AttendencePagination(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            PageSize = pageSize;
            TotalCount = count;
            Items = items;
        }

        public static AttendencePagination<T> Create(List<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count;
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new AttendencePagination<T>(items, count, pageIndex, pageSize);
        }
    }
}

