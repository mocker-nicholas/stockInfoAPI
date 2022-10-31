using MediatR;
using stockInfoApi.DAL.Models.DboModels;
using stockInfoApi.Models.AccountDtos;

namespace stockInfoApi.DAL.Queries.Accounts
{
    public record CreateAccountQuery(PostAccountDto PostAccountDto) : IRequest<AccountDbo>;
}
