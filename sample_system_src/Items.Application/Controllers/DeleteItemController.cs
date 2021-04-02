using Items.Domain.Param;
using Items.Specification;

namespace Items.Application.Controllers
{
    public class DeleteItemController : AbstractHitController<DeleteItemParam>
    {
        public DeleteItemController(IItemsRepository repository) : base(repository.DeleteAsync) { }
    }
}
