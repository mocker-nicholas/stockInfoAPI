using MediatR;
using stockInfoApi.DAL.Models.DboModels;

namespace stockInfoApi.DAL.Queries.Accounts
{
    // This query now needs a handler. Every Query will have a handler
    public record GetAccountListQuery() : IRequest<IEnumerable<AccountDbo>>;
}
