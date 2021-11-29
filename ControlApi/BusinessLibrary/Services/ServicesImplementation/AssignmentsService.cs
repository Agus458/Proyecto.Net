﻿using AutoMapper;
using DataAccessLibrary;
using DataAccessLibrary.Entities;
using DataAccessLibrary.Stores;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SharedLibrary.DataTypes.Assignment;
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
        private readonly IStore<Assignment> Store;
        private readonly IStoreByBuilding<Door> DoorStore;
        private readonly UserManager<User> UserManager;
        private readonly HttpContext HttpContext;
        private readonly IMapper Mapper;

        public AssignmentsService(IStore<Assignment> Store, UserManager<User> UserManager, IHttpContextAccessor HttpContextAccessor, IMapper Mapper, IStoreByBuilding<Door> DoorStore)
        {
            this.Store = Store;
            this.UserManager = UserManager;
            this.HttpContext = HttpContextAccessor.HttpContext;
            this.Mapper = Mapper;
            this.DoorStore = DoorStore;
        }

        public async Task<AssignmentDataType> Create(Guid DoorId)
        {
            var Id = this.HttpContext.User.FindFirst(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
            if (Id == null) throw new ApiError("Usuario no ingresado");
            
            var User = await this.UserManager.FindByIdAsync(Id);
            if(User == null) throw new ApiError("Usuario no encontrado");

            if(DoorId == Guid.Empty) throw new ApiError("Puerta no ingresado");
            var Door = this.DoorStore.GetById(DoorId);
            if (Door == null) throw new ApiError("Puerta no encontrada");

            if(Door.BuildingId != User.BuildingId) throw new ApiError("La puerta no pertenece a tu edificio");

            Assignment NewAssignment = new() { DoorId = DoorId, UserId = User.Id };

            this.Store.Create(NewAssignment);

            return Mapper.Map<AssignmentDataType>(NewAssignment);
        }

        public PaginationDataType<AssignmentDataType> GetAll(int Skip, int Take)
        {
            var Result = this.Store.GetAll(Skip, Take);

            return new PaginationDataType<AssignmentDataType>()
            {
                Collection = Result.Collection.Select(Assignment => Mapper.Map<AssignmentDataType>(Assignment)),
                Size = Result.Size
            };
        }

        public AssignmentDataType GetById(Guid Id)
        {
            if (Id == Guid.Empty) throw new ApiError("Id Invalido", (int)HttpStatusCode.BadRequest);

            var Assignment = this.Store.GetById(Id);
            if (Assignment == null) throw new ApiError("Assignacion no encontrada", (int)HttpStatusCode.NotFound);

            return Mapper.Map<AssignmentDataType>(Assignment);
        }
    }
}