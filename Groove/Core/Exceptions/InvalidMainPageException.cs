namespace Groove.Core;

public class InvalidMainPageException : Exception
{
    public override string Message { get; } = Resources.Exceptions.InvalidMainPage;
}