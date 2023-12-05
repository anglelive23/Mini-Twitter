namespace Mini_Twitter.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key) : base($"{name} {key} is not found!")
        { }
    }
}
