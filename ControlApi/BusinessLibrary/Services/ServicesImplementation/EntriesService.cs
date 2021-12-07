using AutoMapper;
using DataAccessLibrary.Entities;
using DataAccessLibrary.Stores;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SharedLibrary.DataTypes;
using SharedLibrary.DataTypes.Entries;
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
    public class EntriesService : IEntriesService
    {
        private readonly IStoreByBuilding<Entry> Store;
        private readonly IMapper Mapper;
        private readonly HttpContext Context;
        private readonly UserManager<User> UserManager;
        private readonly IStore<Building> BuildingsStore;
        private readonly IStore<Person> PersonsStore;

        public EntriesService(IStoreByBuilding<Entry> Store, IMapper Mapper, IHttpContextAccessor Context, UserManager<User> UserManager, IStore<Building> BuildingsStore, IStore<Person> PersonsStore)
        {
            this.Store = Store;
            this.Mapper = Mapper;
            this.Context = Context.HttpContext;
            this.UserManager = UserManager;
            this.BuildingsStore = BuildingsStore;
            this.PersonsStore = PersonsStore;
        }

        public async Task<EntryDataType> Create(CreateEntryRequestDataType Data)
        {
            var UserId = this.Context.User.FindFirst(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
            if (UserId == null) throw new ApiError("Usuario no ingresado", (int)HttpStatusCode.BadRequest);

            var User = await this.UserManager.FindByIdAsync(UserId);
            if (User == null) throw new ApiError("Usuario no encontrado", (int)HttpStatusCode.NotFound);

            var BuildingId = User.BuildingId;

            if (BuildingId != null)
            {
                var Person = this.PersonsStore.GetById(Data.PersonId);
                if (Person == null) throw new ApiError("Persona no encontrada", (int)HttpStatusCode.NotFound);

                var NewEntry = new Entry() { Id = Guid.NewGuid(), BuildingId = BuildingId.Value };
                this.Mapper.Map(Data, NewEntry);

                this.Store.Create(NewEntry);

                return Mapper.Map<EntryDataType>(NewEntry);
            }

            throw new ApiError("El usuario no tiene edificio asignado", (int)HttpStatusCode.BadRequest);
        }

        public async Task Delete(Guid Id)
        {
            var UserId = this.Context.User.FindFirst(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
            if (UserId == null) throw new ApiError("Usuario no ingresado", (int)HttpStatusCode.BadRequest);

            var User = await this.UserManager.FindByIdAsync(UserId);
            if (User == null) throw new ApiError("Usuario no encontrado", (int)HttpStatusCode.NotFound);

            var BuildingId = User.BuildingId;
            if (BuildingId == null) throw new ApiError("El usuario no tiene edificio asignado", (int)HttpStatusCode.BadRequest);

            var Result = this.Store.GetById(Id, BuildingId.Value);
            if (Result == null) throw new ApiError("Ingreso no encontrado", (int)HttpStatusCode.NotFound);

            this.Store.Delete(Result);
        }

        public async Task<PaginationDataType<EntryDataType>> Get(int Skip, int Take)
        {
            var UserId = this.Context.User.FindFirst(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
            if (UserId == null) throw new ApiError("Usuario no ingresado", (int)HttpStatusCode.BadRequest);

            var User = await this.UserManager.FindByIdAsync(UserId);
            if (User == null) throw new ApiError("Usuario no encontrado", (int)HttpStatusCode.NotFound);

            var BuildingId = User.BuildingId;
            if (BuildingId == null) throw new ApiError("El usuario no tiene edificio asignado", (int)HttpStatusCode.BadRequest);

            var Result = this.Store.GetAll(Skip, Take, BuildingId.Value, new string[] { "Person" });

            return new PaginationDataType<EntryDataType>()
            {
                Collection = Result.Collection.Select(Entity => Mapper.Map<EntryDataType>(Entity)),
                Size = Result.Size
            };
        }

        public PaginationDataType<EntryDataType> GetAll(int Skip, int Take, Guid BuildingId)
        {
            var Building = this.BuildingsStore.GetById(BuildingId);
            if (Building == null) throw new ApiError("Edificio Invalido", (int)HttpStatusCode.BadRequest);

            var Result = this.Store.GetAll(Skip, Take, BuildingId, new string[] { "Person" });

            return new PaginationDataType<EntryDataType>()
            {
                Collection = Result.Collection.Select(Entity => Mapper.Map<EntryDataType>(Entity)),
                Size = Result.Size
            };
        }

        public async Task<EntryDataType> GetById(Guid Id)
        {
            var UserId = this.Context.User.FindFirst(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
            if (UserId == null) throw new ApiError("Usuario no ingresado", (int)HttpStatusCode.BadRequest);

            var User = await this.UserManager.FindByIdAsync(UserId);
            if (User == null) throw new ApiError("Usuario no encontrado", (int)HttpStatusCode.NotFound);

            var BuildingId = User.BuildingId;
            if (BuildingId == null) throw new ApiError("El usuario no tiene edificio asignado", (int)HttpStatusCode.BadRequest);

            var Result = this.Store.GetById(Id, BuildingId.Value, new string[] { "Person" });

            return Mapper.Map<EntryDataType>(Result);
        }
    }
}
