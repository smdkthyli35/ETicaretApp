using ETicaretApp.Application.RequestParameters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApp.Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryRequest : IRequest<GetAllProductQueryResponse>
    {
        public GetAllProductQueryRequest(Pagination pagination)
        {
            Pagination = pagination;
        }

        public Pagination Pagination { get; set; }
    }
}
