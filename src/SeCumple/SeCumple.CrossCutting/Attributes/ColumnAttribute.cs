namespace SeCumple.CrossCutting.Attributes;

[AttributeUsage(AttributeTargets.Property, Inherited = false)]
public class ColumnAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}