namespace BuildingBlocks.Domain.Exceptions;

public class UnsupportedEnumException : Exception
{
    public UnsupportedEnumException(object value)
    : base($"The value entered \"{value}\" is unsupported.")
    {
    }
}