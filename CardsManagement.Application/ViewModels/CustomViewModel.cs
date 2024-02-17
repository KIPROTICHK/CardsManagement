namespace CardsManagement.Application.ViewModels
{
    public class GeneralViewModel
    {
        public string Message { get; set; }
    }
    public abstract class CustomParameters
    {
        private const int maxPageSize = int.MaxValue;

        private int _pageSize = 10;

        public int PageNumber { get; set; } = 1;


        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value > int.MaxValue ? int.MaxValue : value;
            }
        }

        public string SearchTerm { get; set; } = string.Empty;


        public string SortColumn { get; set; } = string.Empty;


        public string SortColumnDirection { get; set; } = "asc";

    }
    public class MetaDataViewModel
    {
        public MetaData MetaData { get; set; }
    }
    public class MetaData
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public int OriginalCount { get; set; }

        public bool HasPrevious => CurrentPage > 1;
    }

    public class PagedList<T> : List<T>
    {
        public MetaData MetaData { get; set; }

        public PagedList(List<T> items, int count, int pageNumber, int pageSize, int OriginlCount)
        {
            MetaData = new MetaData
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                OriginalCount = OriginlCount
            };
            AddRange(items);
        }

        public static PagedList<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize, int OriginlCount = 0)
        {
            int count = source.Count();
            return new PagedList<T>(source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(), count, pageNumber, pageSize, OriginlCount);
        }


    }
}