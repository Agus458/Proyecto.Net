using AutoMapper;
using DataAccessLibrary;
using DataAccessLibrary.Entities;
using DataAccessLibrary.Stores;
using Microsoft.AspNetCore.Http;
using SharedLibrary.DataTypes.Persons;
using SharedLibrary.Error;
using SharedLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Services.ServicesImplementation
{
    public class PersonsService : IPersonsService
    {
        private readonly IPersonsStore Store;
        private readonly IMapper Mapper;
        private readonly ITenantsStore TenantsStore;
        private readonly HttpContext Context;

        public PersonsService(IPersonsStore Store, IMapper Mapper, ITenantsStore TenantsStore, IHttpContextAccessor Context)
        {
            this.Store = Store;
            this.Mapper = Mapper;
            this.Context = Context.HttpContext;
            this.TenantsStore = TenantsStore;
        }

        public PersonDataType Create(CreatePersonRequestDataType Data)
        {
            var TenantId = this.Context.GetTenant();
            if (TenantId == Guid.Empty) throw new ApiError("No se ingreso la institucion", (int)HttpStatusCode.BadRequest);

            if (this.TenantsStore.GetById(TenantId) == null) throw new ApiError("Institucion Invalida", (int)HttpStatusCode.BadRequest);

            var NewPerson = new Person() { Id = Guid.NewGuid() };
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
