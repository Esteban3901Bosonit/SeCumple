namespace SeCumple.CrossCutting.Exceptions;

public abstract class NotFoundException(string nombre, object valor)
    : ApplicationException($"Resource \"{nombre}\": ({valor}) not found");