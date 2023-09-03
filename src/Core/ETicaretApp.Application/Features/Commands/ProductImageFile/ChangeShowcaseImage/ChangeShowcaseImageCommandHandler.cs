using ETicaretApp.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApp.Application.Features.Commands.ProductImageFile.ChangeShowcaseImage
{
    public class ChangeShowcaseImageCommandHandler : IRequestHandler<ChangeShowcaseImageCommandRequest, ChangeShowcaseImageCommandResponse>
    {
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

        public ChangeShowcaseImageCommandHandler(IProductImageFileWriteRepository productImageFileWriteRepository)
        {
            _productImageFileWriteRepository = productImageFileWriteRepository;
        }

        public async Task<ChangeShowcaseImageCommandResponse> Handle(ChangeShowcaseImageCommandRequest request, CancellationToken cancellationToken)
        {
            var query = _productImageFileWriteRepository.Table
                      .Include(p => p.Products)
                      .SelectMany(p => p.Products, (ProductImageFile, Product) => new
                      {
                          ProductImageFile,
                          Product
                      });

            var data = await query.FirstOrDefaultAsync(p => p.Product.Id == Guid.Parse(request.ProductId) && p.ProductImageFile.Showcase);
            if (data is not null)
                data.ProductImageFile.Showcase = false;

            var image = await query.FirstOrDefaultAsync(p => p.ProductImageFile.Id == Guid.Parse(request.ImageId));
            if (image is not null)
                image.ProductImageFile.Showcase = true;

            await _productImageFileWriteRepository.SaveAsync();

            return new();
        }
    }
}
