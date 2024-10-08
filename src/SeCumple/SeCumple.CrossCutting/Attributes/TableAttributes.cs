namespace SeCumple.CrossCutting.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class TableAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}