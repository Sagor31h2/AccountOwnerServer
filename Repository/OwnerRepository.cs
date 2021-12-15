using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class OwnerRepository : RepositoryBase<Owner>, IOwnerRepository
    {
        public OwnerRepository(RepoContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Owner> GetOwners(OwnerParameters ownerParameters)
        {
            return FindAll().
                   OrderBy(ow => ow.Name)
                   .Skip((ownerParameters.PageNumber - 1) * ownerParameters.PageSize)
                   .Take(ownerParameters.PageSize)
                   .ToList();

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
