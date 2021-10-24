﻿using SharedLibrary.DataTypes.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Services
{
    public interface ITenantsService
    {
        IEnumerable<TenantDataType> GetAll();
        
        TenantDataType GetById(Guid Id);

        TenantDataType Create(CreateTenantRequestDataType Data);

        Task Delete(Guid Id);

        void Update(Guid Id, UpdateTenantRequestDataType Data);
    }
}
