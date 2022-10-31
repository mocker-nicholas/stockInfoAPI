using MediatR;
using stockInfoApi.DAL.Models.DboModels;

namespace stockInfoApi.DAL.Queries.Accounts
{
    public record DeleteAccountByIdQuery(Guid Id) : IRequest<AccountDbo>;
}
