using Contracts;
using Entities;
using Entities.Helpers;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;

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
                                o.DateOfBirth.Year <= ownerParameters.MaxYearOfBirth);

            SearchByName(ref owners, ownerParameters.Name);

            ApplySort(ref owners, ownerParameters.OrderBy);

            return PagedList<Owner>.ToPagedList(owners,
                ownerParameters.PageNumber,
                ownerParameters.PageSize);

        }

        //Searching Method
        private void SearchByName(ref IQueryable<Owner> owners, string ownerName)
        {
            if (!owners.Any() || string.IsNullOrWhiteSpace(ownerName))
                return;
            owners = owners.Where(o => o.Name.ToLower().Contains(ownerName.Trim().ToLower()));
        }

        //sorting Method
        private void ApplySort(ref IQueryable<Owner> owners, string orderByQueryString)
        {
            if (!owners.Any())
                return;
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                owners = owners.OrderBy(x => x.Name);
                return;
            }

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Owner).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;
                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
                if (objectProperty == null)
                    continue;
                var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {sortingOrder}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');


            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                owners = owners.OrderBy(x => x.Name);
                return;
            }

            owners = owners.OrderBy(orderQuery);
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
