namespace BlogProject.Server.MinimalApi.Filter
{
    public class ConflictException : Exception
    {
        public ConflictException(string message) : base(message) { }
    }
}
