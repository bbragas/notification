namespace Notification.Api.Messages.Abstractions.Pagination
{
    public record Paging
    {
        private const int DefaultPerPage = 50;
        private int _perPage;
        private int _page;

        public int PerPage
        {
            get => _perPage > 0 ? _perPage : DefaultPerPage;
            set => _perPage = value;
        }

        public int Page
        {
            get => _page - 1;
            set => _page = value;
        }
    }
}
