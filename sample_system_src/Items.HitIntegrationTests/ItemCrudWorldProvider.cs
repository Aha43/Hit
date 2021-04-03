using Hit.Specification.User;

namespace Items.HitIntegrationTests
{
    public class ItemCrudWorldProvider : IWorldProvider<ItemCrudWorld>
    {
        public ItemCrudWorld Get() => new ItemCrudWorld();
    }
}
