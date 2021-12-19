using Contracts;
using Entities;
using Entities.Helpers;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Repository
{
    public class OwnerRepository : RepositoryBase<Owner>, IOwnerRepository
    {
        public OwnerRepository(RepoContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public PagedList<Owner> GetOwners(OwnerParameters ownerParameters)
        {
            var owners = FindByCondition(o => o.DateOfBirth.Year >= ownerParameters.MinYearOfBirth &&
                                o.DateOfBirth.Year <= ownerParameters.MaxYearOfBirth)
                            .OrderBy(on => on.Name);

            return PagedList<Owner>.ToPagedList(owners,
                ownerParameters.PageNumber,
                ownerParameters.PageSize);

        }

        public Owner GetOwnerById(Guid ownerId)
        {
            return FindByCondition(owner => owner.Id.Equals(ownerId))
                    .FirstOrDefault();
        }

        public Owner GetOwnerWithDetails(Guid ownerId)
        {
            return FindByCondition(owner => owner.Id.Equals(ownerId))
                .Include(ac => ac.Accounts)
                .FirstOrDefault();
        }

        //Create owner 
        public void CreateOwner(Owner owner)
        {
            Create(owner);
        }

        //Update
        public void UpdateOwner(Owner owner)
        {
            Update(owner);
        }

        //delete
        public void DeleteOwner(Owner owner)
        {
            Delete(owner);
        }

    }
}
