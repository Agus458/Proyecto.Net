using AutoMapper;
using DataAccessLayer.Entities;
using DataAccessLayer.Extensions;
using DataAccessLayer.RepositoriesInterfaces;
using Shared.DataTypes.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly List<User> Users = new()
        {
            new User() { Email = "mail@mail.com", Id = Guid.NewGuid() },
            new User() { Email = "mail2@mail.com", Id = Guid.NewGuid() },
            new User() { Email = "mail3@mail.com", Id = Guid.NewGuid() }
        };

        public UserDataType CreateUser(CreateUserDataType Data)
        {
            var User = new User()
            {
                Id = Guid.NewGuid(),
                Email = Data.Email,
                Password = Data.Password,
                UserName = Data.UserName
            };

            this.Users.Add(User);

            return User.GetDataType();
        }

        public UserDataType GetUser(Guid Id)
        {
            var User = this.Users.Where(User => User.Id.Equals(Id)).SingleOrDefault();

            if (User == null) return null;

            return User.GetDataType();
        }

        public IEnumerable<UserDataType> GetUsers()
        {
            return this.Users.Select(Element => Element.GetDataType());
        }

        public void UpdateUser(Guid Id, UpdateUserDataType Data)
        {
            var User = this.Users.FirstOrDefault(ExistingUser => ExistingUser.Id == Id);

            if (User != null)
            {
                var Index = this.Users.FindIndex(ExistingUser => ExistingUser.Id == Id);
                this.Users[Index] = User.AsignDataType(Data);
            }
        }
    }
}
