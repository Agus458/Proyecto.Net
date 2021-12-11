using AutoMapper;
using DataAccessLibrary;
using DataAccessLibrary.Contexts;
using DataAccessLibrary.Entities;
using DataAccessLibrary.Stores;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SharedLibrary.DataTypes.Assignment;
using SharedLibrary.DataTypes.Doors;
using SharedLibrary.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Services.ServicesImplementation
{
    public class AssignmentsService : IAssignmentsService
    {
        private readonly IAssignmentsStore Store;
        private readonly IStoreByBuilding<Door> DoorStore;
        private readonly UserManager<User> UserManager;
        private readonly HttpContext HttpContext;
        private readonly IMapper Mapper;
        private readonly ApiDbContext ApiContext;

        public AssignmentsService(IAssignmentsStore Store, UserManager<User> UserManager, IHttpContextAccessor HttpContextAccessor, IMapper Mapper, IStoreByBuilding<Door> DoorStore, ApiDbContext ApiContext)
        {
            this.Store = Store;
            this.UserManager = UserManager;
            this.HttpContext = HttpContextAccessor.HttpContext;
            this.Mapper = Mapper;
            this.DoorStore = DoorStore;
            this.ApiContext = ApiContext;
        }

        public async Task<AssignmentDataType> Create(CreateAssignmentRequestDataType Data)
        {
            using (var Transaction = await this.ApiContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var Id = this.HttpContext.User.FindFirst(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
                    if (Id == null) throw new ApiError("Usuario no ingresado", (int)HttpStatusCode.BadRequest);

                    var User = await this.UserManager.FindByIdAsync(Id);
                    if (User == null) throw new ApiError("Usuario no encontrado", (int)HttpStatusCode.NotFound);

                    if (Data.DoorId == Guid.Empty) throw new ApiError("Puerta no ingresado", (int)HttpStatusCode.BadRequest);
                    var Door = this.DoorStore.GetById(Data.DoorId, new string[] { "ActualUser" });
                    if (Door == null) throw new ApiError("Puerta no encontrada", (int)HttpStatusCode.NotFound);

                    if (Door.BuildingId != User.BuildingId) throw new ApiError("La puerta no pertenece a tu edificio", (int)HttpStatusCode.BadRequest);

                    if (Door.ActualUser != null) throw new ApiError("La puerta ya se encuentra asignada", (int)HttpStatusCode.BadRequest);

                    Assignment NewAssignment = new() { DoorId = Data.DoorId, UserId = User.Id };

                    User.Door = Door;

                    await this.UserManager.UpdateAsync(User);

                    this.Store.Create(NewAssignment);

                    Transaction.Commit();

                    return Mapper.Map<AssignmentDataType>(NewAssignment);
                }
                catch (Exception)
                {
                    Transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task<PaginationDataType<AssignmentDataType>> GetAll(int Skip, int Take)
        {
            var Id = this.HttpContext.User.FindFirst(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
            if (Id == null) throw new ApiError("Usuario no ingresado", (int)HttpStatusCode.BadRequest);

            var User = await this.UserManager.FindByIdAsync(Id);
            if (User == null) throw new ApiError("Usuario no encontrado", (int)HttpStatusCode.NotFound);

            var Result = this.Store.GetAll(Skip, Take, Id, new string[] { "Door" });

            return new PaginationDataType<AssignmentDataType>()
            {
                Collection = Result.Collection.Select(Assignment => Mapper.Map<AssignmentDataType>(Assignment)),
                Size = Result.Size
            };
        }

        public async Task<AssignmentDataType> GetById(Guid Id)
        {
            if (Id == Guid.Empty) throw new ApiError("Id Invalido", (int)HttpStatusCode.BadRequest);

            var UserId = this.HttpContext.User.FindFirst(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
            if (UserId == null) throw new ApiError("Usuario no ingresado", (int)HttpStatusCode.BadRequest);

            var User = await this.UserManager.FindByIdAsync(UserId);
            if (User == null) throw new ApiError("Usuario no encontrado", (int)HttpStatusCode.NotFound);

            var Assignment = this.Store.GetById(Id, UserId, new string[] { "Door" });
            if (Assignment == null) throw new ApiError("Assignacion no encontrada", (int)HttpStatusCode.NotFound);

            return Mapper.Map<AssignmentDataType>(Assignment);
        }

        public async Task<PaginationDataType<DoorDataType>> GetDoors(int Skip, int Take)
        {
            var Id = this.HttpContext.User.FindFirst(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
            if (Id == null) throw new ApiError("Usuario no ingresado", (int)HttpStatusCode.BadRequest);

            var User = await this.UserManager.FindByIdAsync(Id);
            if (User == null) throw new ApiError("Usuario no encontrado", (int)HttpStatusCode.NotFound);

            if (User.BuildingId == null) throw new ApiError("Usuario sin edificio", (int)HttpStatusCode.BadRequest);

            var Result = this.DoorStore.GetAll(Skip, Take, User.BuildingId.Value, new string[] { "ActualUser" });

            return new PaginationDataType<DoorDataType>()
            {
                Collection = Result.Collection.Select(Assignment => Mapper.Map<DoorDataType>(Assignment)),
                Size = Result.Size
            };
        }

        public async Task Delete(Guid Id)
        {
            using (var Transaction = await this.ApiContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var UserId = this.HttpContext.User.FindFirst(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
                    if (UserId == null) throw new ApiError("Usuario no ingresado", (int)HttpStatusCode.BadRequest);

                    var User = await this.UserManager.FindByIdAsync(UserId);
                    if (User == null) throw new ApiError("Usuario no encontrado", (int)HttpStatusCode.NotFound);

                    var Assignment = this.Store.GetById(Id, UserId);
                    if (Assignment == null) throw new ApiError("Assignacion no encontrada", (int)HttpStatusCode.NotFound);

                    var LastAssignment = this.Store.GetLast(UserId);

                    if (LastAssignment != null && LastAssignment.Id == Assignment.Id)
                    {
                        User.DoorId = null;

                        await this.UserManager.UpdateAsync(User);
                    }

                    this.Store.Delete(Assignment);

                    Transaction.Commit();
                }
                catch (Exception)
                {
                    Transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
