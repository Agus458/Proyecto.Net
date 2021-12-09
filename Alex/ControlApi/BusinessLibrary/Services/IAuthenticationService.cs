using SharedLibrary.DataTypes.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Services
{
    /// <summary>
    /// Service implementation to authenticate users in the system.
    /// </summary>
    public interface IAuthenticationService
    {
        Task<dynamic> Login(LoginRequestDataType Data);
    }
}
