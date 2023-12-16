using ETicaretApp.Application.RequestParameters;
using MediatR;

namespace ETicaretApp.Application.Features.Queries.Role.GetRoles
{
    public class GetRolesQueryRequest : IRequest<GetRolesQueryResponse>
    {
        public GetRolesQueryRequest(Pagination pagination)
        {
            Pagination = pagination;
        }

        public Pagination Pagination { get; set; }
    }
}