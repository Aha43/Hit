using Items.Domain.Param;
using Items.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Items.Application.Controllers
{
    public class ReadItemController : AbstractHitController<ReadItemParam>
    {
        public ReadItemController(IItemsRepository repository) : base(repository.ReadAsync) { }
    }
}
