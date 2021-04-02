using Items.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Items.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class AbstractHitController<Param>
    {
        private readonly Func<Param, CancellationToken, Task<Item>> _delegate;

        public AbstractHitController(Func<Param, CancellationToken, Task<Item>> theDelegate)
        {
            _delegate = theDelegate;
        }

        [HttpPost]
        public async Task<Item> GetAsync([FromBody] Param param) => await _delegate.Invoke(param, CancellationToken.None);

    }

}
