namespace Hit.Specification.Infrastructure
{
    public interface ITestOptions
    {
        string Get(string name);
        bool GetAsBoolean(string name, bool def = false);
        int GetAsInt(string name, int def = 0);
        decimal GetAsDecimal(string name, decimal def = 0.0M);
        bool None { get; }
    }
}
