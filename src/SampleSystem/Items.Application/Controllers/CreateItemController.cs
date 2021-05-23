using Items.Domain.Param;
using Items.Specification;

namespace Items.Application.Controllers
{
    public class CreateItemController : AbstractHitController<CreateItemParam>
    {
        public CreateItemController(IItemsRepository repository) : base(repository.CreateAsync) { }
    }
}
