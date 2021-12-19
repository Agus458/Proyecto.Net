using DataAccessLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Stores
{
    /// <summary>
    /// Generic Store interface that implements basic CRUD operations.
    /// </summary>
    /// <typeparam name="Target">Target entity classs in the database.</typeparam>
    public interface IStore<Target> where Target : BaseEntity
    {
        /// <summary>
        /// Returns All the entities that corrispond with the target class.
        /// </summary>
        /// <returns>A collection of the target entity.</returns>
        PaginationDataType<Target> GetAll(int Skip, int Take, [Optional] string[] Relations);

        /// <summary>
        /// Gets the target entity with the passed Id.
        /// </summary>
        /// <param name="Id">The Id of the target entity.</param>
        /// <returns>The entity with the passed Id.</returns>
        Target GetById(Guid Id, [Optional] string[] Relations);

        /// <summary>
        /// Creates the given entity.
        /// </summary>
        /// <param name="Entity">he entity to create.</param>
        /// <returns></returns>
        void Create(Target Entity);

        /// <summary>
        /// Updates the given entity.
        /// </summary>
        /// <param name="Entity">The entity to update.</param>
        /// <returns></returns>
        void Update(Target Entity);

        /// <summary>
        /// Deletes thr given entity.
        /// </summary>
        /// <param name="Id">The entity to delete</param>
        /// <returns></returns>
        void Delete(Target Entity);

        IEnumerable<Target> Get([Optional] string[] Relations);

        int Count();
    }
}
