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
    /// Store Interface to manage the institutions in the database.
    /// </summary>
    public interface ITenantsStore
    {
        /// <summary>
        /// Returns All the entities that corrispond with the target class.
        /// </summary>
        /// <returns>A collection of the target entity.</returns>
        PaginationDataType<Tenant> GetAll([Optional] string[] Relations, int Skip = 0, int Take = 10);

        /// <summary>
        /// Gets the target entity with the passed Id.
        /// </summary>
        /// <param name="Id">The Id of the target entity.</param>
        /// <returns>The entity with the passed Id.</returns>
        Tenant GetById(Guid Id, [Optional] string[] Relations);

        /// <summary>
        /// Creates the given entity.
        /// </summary>
        /// <param name="Entity">he entity to create.</param>
        /// <returns></returns>
        Tenant Create(Tenant Entity);

        /// <summary>
        /// Updates the given entity.
        /// </summary>
        /// <param name="Entity">The entity to update.</param>
        /// <returns></returns>
        void Update(Tenant Entity);

        /// <summary>
        /// Deletes thr given entity.
        /// </summary>
        /// <param name="Id">The entity to delete</param>
        /// <returns></returns>
        Task Delete(Tenant Entity);

        Tenant GetByRut(string Rut);

        Tenant GetBySocialReason(string Rut);

        IEnumerable<Tenant> Get();
    }
}
