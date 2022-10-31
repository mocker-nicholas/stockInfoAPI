using MediatR;
using stockInfoApi.DAL.Models.DboModels;
using stockInfoApi.Models.AccountDtos;

namespace stockInfoApi.DAL.Queries.Accounts
{
    public record UpdateAccountByIdQuery(Guid Id, PutAccountDto PutAccountDto) : IRequest<AccountDbo>;
}
