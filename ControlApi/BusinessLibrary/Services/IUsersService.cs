using SharedLibrary.DataTypes.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Services
{
    /// <summary>
    /// Service that provides users manipulation.
    /// </summary>
    public interface IUsersService
    {
        /// <summary>
        /// Gets all the users in the system.
        /// </summary>
        /// <returns></returns>
        IEnumerable<UserDataType> GetAll();

        /// <summary>
        /// Gets a user from the sistem that has the passed Id.
        /// </summary>
        /// <param name="Id">The Id of the user.</param>
        /// <returns>The user with the passed Id.</returns>
        Task<UserDataType> GetById(string Id);

        /// <summary>
        /// Crates a new User in the system.
        /// </summary>
        /// <param name="Data">The Data of the user to create.</param>
        /// <returns></returns>
        Task<UserDataType> Create(CreateUserRequestDataType Data);

        Task Delete(string Id);
    }
}
