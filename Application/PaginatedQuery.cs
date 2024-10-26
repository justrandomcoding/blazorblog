namespace Application;

public abstract class PaginatedQuery
{
    public required int PageIndex { get; init; }
    public required int PageSize { get; init; }
}