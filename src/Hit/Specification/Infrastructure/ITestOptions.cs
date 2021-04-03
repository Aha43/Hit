namespace Hit.Specification.Infrastructure
{
    public interface ITestOptions
    {
        string Get(string name, string def = null);
        bool Equals(string name, string val, string def = null);
        bool EqualsIgnoreCase(string name, string val, string def = null);
    }
}
