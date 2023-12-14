using CQRSMediator.Models;
using MediatR;

namespace CQRSMediator.CQRS.Commands
{
    public class CreateProductCommand : IRequest<int>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
        {
            private ProductContext context;
            public CreateProductCommandHandler(ProductContext context)
            {
                this.context = context;
            }
            public async Task<int> Handle(CreateProductCommand command, CancellationToken cancellationToken)
            {
                var product = new Product();
                product.Name = command.Name;
                product.Price = command.Price;

                context.Product.Add(product);
                await context.SaveChangesAsync();
                return product.Id;
            }
        }
    }
}
