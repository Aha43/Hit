using Items.Domain.Param;
using Items.Specification;

namespace Items.Application.Controllers
{
    public class UpdateItemController : AbstractHitController<UpdateItemParam>
    {
        public UpdateItemController(IItemsRepository repository) : base(repository.UpdateAsync) { }
    }
}
