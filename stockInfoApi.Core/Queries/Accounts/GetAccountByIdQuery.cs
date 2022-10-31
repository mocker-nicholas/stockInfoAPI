using MediatR;
using stockInfoApi.DAL.Models.DboModels;

namespace stockInfoApi.DAL.Queries.Accounts
{
    public record GetAccountByIdQuery(Guid Id) : IRequest<AccountDbo>;
}
