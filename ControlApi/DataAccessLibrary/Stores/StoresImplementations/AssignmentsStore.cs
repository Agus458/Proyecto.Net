using DataAccessLibrary.Contexts;
using DataAccessLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Stores.StoresImplementations
{
    public class AssignmentsStore: Store<Assignment>, IAssignmentsStore
    {
        private readonly ApiDbContext Context;

        public AssignmentsStore(ApiDbContext Context): base(Context)
        {
            this.Context = Context;
        }

        public PaginationDataType<Assignment> GetAll(int Skip, int Take, string UserId, string[] Relations)
        {
            var RelationsArray = new List<string>() { };
            if (Relations != null) RelationsArray = RelationsArray.Concat(Relations).ToList();

            var Collection = this.Context.Set<Assignment>().GetAllIncluding(RelationsArray.ToArray()).Where(Entity => Entity.UserId == UserId).OrderByDescending(Entity => Entity.CreatedDate);
            return new PaginationDataType<Assignment> { Collection = Collection.Skip(Skip).Take(Take > 0 ? Take : 10).AsEnumerable(), Size = Collection.Count() };
        }

        public Assignment GetById(Guid Id, string UserId, string[] Relations)
        {
            var RelationsArray = new List<string>() { };
            if (Relations != null) RelationsArray = RelationsArray.Concat(Relations).ToList();

            return this.Context.Set<Assignment>().GetAllIncluding(RelationsArray.ToArray()).Where(Entity => Entity.UserId == UserId).SingleOrDefault(Entity => Entity.Id == Id);
        }

        public Assignment GetLast(string UserId, [Optional] string[] Relations)
        {
            var RelationsArray = new List<string>() { };
            if (Relations != null) RelationsArray = RelationsArray.Concat(Relations).ToList();

            return this.Context.Set<Assignment>().GetAllIncluding(RelationsArray.ToArray()).Where(Entity => Entity.UserId == UserId).OrderByDescending(Entity => Entity.CreatedDate).First();
        }
    }
}
