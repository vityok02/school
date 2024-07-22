namespace SchoolManagement.Client;

public static class UriBuilder
{
    public static string BuildListUri(
        string baseAddress,
        string? searchTerm,
        string? sortColumn,
        string? sortOrder,
        int? page = 1,
        int? pageSize = 10)
    {
        var requestUri = baseAddress;

        char symbol = '?';

        if (searchTerm is not null)
        {
            requestUri += $"{symbol}{nameof(searchTerm)}={searchTerm}";
            symbol = '&';
        }
        if (sortColumn is not null)
        {
            requestUri += $"{symbol}{nameof(sortColumn)}={sortColumn}";
            symbol = '&';
            if (sortOrder is not null)
            {
                requestUri += $"{symbol}{nameof(sortOrder)}={sortOrder}";
            }
        }
        if (page is not null)
        {
            requestUri += $"{symbol}{nameof(page)}={page}";
            symbol = '&';
            requestUri += $"{symbol}{nameof(pageSize)}={pageSize}";
        }

        return requestUri;
    }
}
