using System.Linq.Expressions;
using SeCumple.CrossCutting.Interfaces;

namespace SeCumple.Application.Specifications;

public class BaseSpecification<T>(Expression<Func<T, bool>> criteria) : ISpecification<T>
{
    public Expression<Func<T, bool>>? Criteria { get; } = criteria;
    public List<Expression<Func<T, object>>>? Includes { get; } = new();
    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }
    public int Take { get; private set; }
    public int Skip { get; private set; }
    public bool IsPagingEnable { get; private set; }

    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    protected void AddOrderByDescending(Expression<Func<T, object>> orderByExpression)
    {
        OrderByDescending = orderByExpression;
    }

    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnable = true;
    }

    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes!.Add(includeExpression);
    }

    protected static List<int> ParseIds(string ids)
    {
        return string.IsNullOrWhiteSpace(ids) ? new List<int>() : ids.Split(',').Select(int.Parse).ToList();
    }

    protected static List<int> SplitIds(string ids, char separator)
    {
        return string.IsNullOrWhiteSpace(ids) ? [] : ids.Split(separator).Select(int.Parse).ToList();
    }
    
    protected static List<string> SplitString(string ids, char separator)
    {
        return string.IsNullOrWhiteSpace(ids) ? [] : ids.Split(separator).ToList();
    }
}