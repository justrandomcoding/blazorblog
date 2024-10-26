namespace Application;

public record PaginatedResponse<T>(int Page, int PageSize, int Count, IEnumerable<T> Data) where T : class;