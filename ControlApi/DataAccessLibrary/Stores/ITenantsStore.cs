using DataAccessLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
        IEnumerable<Tenant> GetAll();

        /// <summary>
        /// Gets the target entity with the passed Id.
        /// </summary>
        /// <param name="Id">The Id of the target entity.</param>
        /// <returns>The entity with the passed Id.</returns>
        Tenant GetById(Guid Id);

        /// <summary>
        /// Creates the given entity.
        /// </summary>
        /// <param name="Entity">he entity to create.</param>
        /// <returns></returns>
        Task CreateAsync(Tenant Entity, string Email, string Password);

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
        void Delete(Tenant Entity);

        Tenant GetByRut(string Rut);

        Tenant GetBySocialReason(string Rut);
    }
}
