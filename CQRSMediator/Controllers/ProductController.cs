using CQRSMediator.CQRS.Commands;
using CQRSMediator.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSMediator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IMediator mediator;
        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // URL - https://localhost:44378/api/Product type POST
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        // URL - https://localhost:44378/api/Product type GET
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await mediator.Send(new GetAllProductQuery()));
        }

        // URL - https://localhost:44378/api/Product/{id} type GET
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await mediator.Send(new GetProductByIdQuery { Id = id }));
        }

        // URL - https://localhost:44378/api/Product/{id} type PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateProductCommand command)
        {
            command.Id = id;
            return Ok(await mediator.Send(command));
        }

        // URL - https://localhost:44378/api/Product/{id} type Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await mediator.Publish(new Notifications.DeleteProductNotification { ProductId = id });
            return Ok(await mediator.Send(new DeleteProductByIdCommand { Id = id }));
        }
    }
}
