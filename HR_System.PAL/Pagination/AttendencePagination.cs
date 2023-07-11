namespace HR_System.PAL.Pagination
{
    public class AttendencePagination<T>
    {
        public List<T> Items { get; }
        public int TotalItems { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalPages { get; }

        public AttendencePagination(IEnumerable<T> items, int totalItems, int pageNumber, int pageSize, int totalPages)
        {
            Items = items.ToList();
            TotalItems = totalItems;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = totalPages;
        }

        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }
}

