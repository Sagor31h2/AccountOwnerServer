using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(RepoContext repositoryContext)
            : base(repositoryContext)
        {

        }
    }
}
