namespace SeCumple.CrossCutting.Exceptions;

public class BadRequestException(string message) : ApplicationException(message);