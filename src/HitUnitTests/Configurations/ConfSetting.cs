namespace HitUnitTests.Configurations
{
    public abstract class ConfSetting
    {
        public string Name { get; set; }

        public override string ToString() => $"{GetType().Name}_{Name}";
    }
}
