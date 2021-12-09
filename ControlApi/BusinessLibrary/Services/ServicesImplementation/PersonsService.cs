using AutoMapper;
using DataAccessLibrary;
using DataAccessLibrary.Entities;
using DataAccessLibrary.Stores;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SharedLibrary.Configuration.FacePlusPlus;
using SharedLibrary.DataTypes.Persons;
using SharedLibrary.Error;
using SharedLibrary.Extensions;
using SharedLibrary.Helpers;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BusinessLibrary.Services.ServicesImplementation
{
    public class PersonsService : IPersonsService
    {
        private readonly IWebHostEnvironment Environment;
        private readonly IStore<Person> Store;
        private readonly IStoreByBuilding<Entry> EntryStore;
        private readonly IMapper Mapper;
        private readonly ITenantsStore TenantsStore;
        private readonly HttpContext Context;
        private readonly FacePlusPlus FaceApi;
        private readonly INotificationService NotificationService;
        private readonly UserManager<User> UserManager;

        public PersonsService(IStore<Person> Store, IMapper Mapper, ITenantsStore TenantsStore, IStoreByBuilding<Entry> EntryStore, IHttpContextAccessor Context, IWebHostEnvironment Environment, FacePlusPlus FaceApi, INotificationService NotificationService, UserManager<User> UserManager)
        {
            this.Store = Store;
            this.Mapper = Mapper;
            this.Context = Context.HttpContext;
            this.TenantsStore = TenantsStore;
            this.Environment = Environment;
            this.FaceApi = FaceApi;
            this.NotificationService = NotificationService;
            this.UserManager = UserManager;
            this.EntryStore = EntryStore;
        }

        public async Task<PersonDataType> Create(CreatePersonRequestDataType Data)
        {
            var TenantId = this.Context.GetTenant();
            if (TenantId == Guid.Empty) throw new ApiError("No se ingreso la institucion", (int)HttpStatusCode.BadRequest);

            if (this.TenantsStore.GetById(TenantId) == null) throw new ApiError("Institucion Invalida", (int)HttpStatusCode.BadRequest);

            Guid Id = Guid.NewGuid();
            string Image = "";

            if (Data.FileImage != null && Data.FileImage.Length > 0 && Data.FileImage.ContentType == "image/jpeg")
            {
                await FaceApi.UploadImage(Data.FileImage, Id.ToString());

                Image = FileHelper.Upload(Data.FileImage, this.Environment, Id.ToString());
            }

            var NewPerson = new Person() { Id = Id, Image = Image };
            Mapper.Map(Data, NewPerson);

            this.Store.Create(NewPerson);

            return Mapper.Map<PersonDataType>(NewPerson);
        }

        public void Delete(Guid Id)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalid Id", (int)HttpStatusCode.BadRequest);

            var Person = this.Store.GetById(Id);
            if (Person == null) throw new ApiError("Person Not Found", (int)HttpStatusCode.NotFound);

            this.Store.Delete(Person);
        }

        public async Task<PersonDataType> Identify(IFormFile fileImage)
        {
            var UserId = this.Context.User.FindFirst(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
            if (UserId == null) throw new ApiError("Usuario no ingresado");

            var User = await this.UserManager.FindByIdAsync(UserId);
            if (User == null) throw new ApiError("Usuario no encontrado");

            var BuildingId = User.BuildingId;

            var result = await this.FaceApi.Identify(fileImage);

            if (result.results != null && result.results.Count() > 0)
            {
                if (result.results[0].confidence >= 80)
                {
                    if (Guid.TryParse(result.results[0].user_id, out var Id))
                    {
                        var Person = this.Store.GetById(Id);
                        if (Person != null)
                        {
                            Entry NewEntry = new Entry()
                            {
                                Id = Guid.NewGuid(),
                                PersonId = Person.Id,
                                BuildingId = BuildingId.Value
                            };

                            this.EntryStore.Create(NewEntry);

                            return Mapper.Map<PersonDataType>(Person);
                        }
                    }
                }
            }

            await this.NotificationService.SendNotification("Person Not Found", BuildingId.Value);

            throw new ApiError("Person Not Found", (int)HttpStatusCode.NotFound);
        }

        public PaginationDataType<PersonDataType> GetAll(int Skip, int Take)
        {
            var Result = this.Store.GetAll(Skip, Take);

            return new PaginationDataType<PersonDataType>()
            {
                Collection = Result.Collection.Select(Person => Mapper.Map<PersonDataType>(Person)),
                Size = Result.Size
            };
        }

        public PersonDataType GetById(Guid Id)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalid Id", (int)HttpStatusCode.BadRequest);

            var Building = this.Store.GetById(Id);
            if (Building == null) throw new ApiError("Person Not Found", (int)HttpStatusCode.NotFound);

            return Mapper.Map<PersonDataType>(Building);
        }

        public void Update(Guid Id, UpdatePersonRequestDataType Data)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);

            var Person = this.Store.GetById(Id);
            if (Person == null) throw new ApiError("Person Not Found", (int)HttpStatusCode.NotFound);

            Mapper.Map(Data, Person);

            this.Store.Update(Person);
        }
    }
}
