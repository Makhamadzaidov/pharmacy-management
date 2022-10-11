namespace PharmacyAppExam.WebApi.Commons.Utils
{
    public class PaginationParams
    {
        private int _pageSize;
        private int _pageIndex;

        public int PageIndex
        {
            get => _pageIndex;
            set => _pageIndex = value < 1 ? 1 : value;
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value is < 0 or > 100 ? 100 : value;
        }
    }
}
