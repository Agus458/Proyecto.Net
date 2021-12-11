using AutoMapper;
using BusinessLibrary.SignalR;
using DataAccessLibrary.Entities;
using DataAccessLibrary.Stores;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using SharedLibrary.DataTypes;
using SharedLibrary.DataTypes.Notifications;
using SharedLibrary.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Services.ServicesImplementation
{
    public class NotificationService : INotificationService
    {
        private readonly IStore<Notification> Store;
        private readonly IHubContext<BroadcastHub, IHubClient> HubContext;
        private readonly UserManager<User> UserManager;
        private readonly HttpContext HttpContext;
        private readonly IMapper Mapper;

        public NotificationService(IStore<Notification> Store, IHubContext<BroadcastHub, IHubClient> HubContext, UserManager<User> UserManager, IHttpContextAccessor HttpContextAccessor, IMapper Mapper)
        {
            this.Store = Store;
            this.HubContext = HubContext;
            this.HttpContext = HttpContextAccessor.HttpContext;
            this.Mapper = Mapper;
            this.UserManager = UserManager;
        }

        public async Task<PaginationDataType<NotificationDataType>> GetAll(int Skip, int Take)
        {
            var Id = this.HttpContext.User.FindFirst(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
            if (Id == null) throw new ApiError("Usuario no ingresado");

            var User = await this.UserManager.FindByIdAsync(Id);
            if (User == null) throw new ApiError("Usuario no encontrado");

            var Result = this.Store.GetAll(Skip, Take);

            return new PaginationDataType<NotificationDataType>()
            {
                Collection = Result.Collection.Select(Door => Mapper.Map<NotificationDataType>(Door)),
                Size = Result.Size
            };
        }

        public async Task SendNotification(string Message, string UserId)
        {
            Notification NewNotification = new Notification()
            {
                Message = Message,
                UserId = UserId
            };

            this.Store.Create(NewNotification);
            await this.HubContext.Clients.All.BroadcastMessage();
        }

        public Task Clear()
        {
            throw new NotImplementedException();
        }

        public async Task Delete(Guid Id)
        {
            var UserId = this.HttpContext.User.FindFirst(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
            if (UserId == null) throw new ApiError("Usuario no ingresado");

            var User = await this.UserManager.FindByIdAsync(UserId);
            if (User == null) throw new ApiError("Usuario no encontrado");

            if (User.BuildingId != null)
            {
                var Notification = this.Store.GetById(Id);
                if (Notification == null) throw new ApiError("Notificacion no encontrado");

                this.Store.Delete(Notification);

                return;
            }

            throw new ApiError("Edificio no asignado");
        }
    }
}
