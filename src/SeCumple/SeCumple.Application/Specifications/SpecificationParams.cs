namespace SeCumple.Application.Specifications;

public class SpecificationParams
{
    private const int MaxPageSize = 50;
    private readonly int _pageSize = 5;
    public string? Sort { get; init; }
    public int PageIndex { get; init; } = 1;
    public Dictionary<string, string>? Filters { get; set; }

    public int PageSize
    {
        get => _pageSize;
        init => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }
}