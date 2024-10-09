using FluentValidation.Results;

namespace SeCumple.CrossCutting.Exceptions;

public class ValidationException() : ApplicationException("Validation Errors")
{
    public readonly IDictionary<string, string[]> Errors = new Dictionary<string, string[]>();

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures.GroupBy(x => x.PropertyName, x => x.ErrorMessage).ToDictionary(y => y.Key, y => y.ToArray());
    }
}