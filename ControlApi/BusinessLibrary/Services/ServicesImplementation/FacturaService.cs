using DataAccessLibrary.Entities;
using DataAccessLibrary.Stores;
using Microsoft.AspNetCore.Identity;
using SharedLibrary.Error;
using SharedLibrary.DataTypes.Factura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using AutoMapper;
using SharedLibrary.DataTypes;
using Microsoft.AspNetCore.Http;
using SharedLibrary.Extensions;

namespace BusinessLibrary.Services.ServicesImplementation
{
    public class FacturaService : IFacturaService
    {
        private readonly IStore<Factura> Store;
        private readonly IMapper Mapper;
        private readonly HttpContext Context;
        private readonly ITenantsStore TenantsStore;
        private readonly IPrecioStore PrecioStore;

        public FacturaService(IStore<Factura> Store, IMapper Mapper, ITenantsStore TenantsStore, IHttpContextAccessor Context, IPrecioStore PrecioStore)
        {
            this.Store = Store;
            this.Mapper = Mapper;
            this.Context = Context.HttpContext;
            this.TenantsStore = TenantsStore;
            this.PrecioStore = PrecioStore;
        }

        public void Delete(Guid Id)
        {
            var Factura = this.Store.GetById(Id);
            if (Factura == null) throw new ApiError("Factura no encontrada", (int)HttpStatusCode.NotFound);

            this.Store.Delete(Factura);
        }

        public PaginationDataType<FacturaDataType> GetAll(int Skip, int Take)
        {
            var Result = this.Store.GetAll(Skip, Take);

            return new PaginationDataType<FacturaDataType>()
            {
                Collection = Result.Collection.Select(Factura => Mapper.Map<FacturaDataType>(Factura)),
                Size = Result.Size
            };
        }

        public FacturaDataType GetBuId(Guid Id)
        {
            var Factura = this.Store.GetById(Id);
            if (Factura == null) throw new ApiError("Factura Not Found", (int)HttpStatusCode.NotFound);

            return Mapper.Map<FacturaDataType>(Factura);
        }

        public FacturaDataType Create(CreateFacturaRequestDataType Data)
        {
            var TenantId = this.Context.GetTenant();
            if (TenantId == Guid.Empty) throw new ApiError("No se ingreso la institucion", (int)HttpStatusCode.BadRequest);

            if (this.TenantsStore.GetById(TenantId) == null) throw new ApiError("Institucion Invalida", (int)HttpStatusCode.BadRequest);

            var NewFactura = new Factura() { Id = Guid.NewGuid(), TenantId = TenantId };
            Mapper.Map(Data, NewFactura);

            this.Store.Create(NewFactura);

            return Mapper.Map<FacturaDataType>(NewFactura);
        }

        public void Pagar(Guid IdFactura)
        {
            if (IdFactura == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);
            var Factura = this.Store.GetById(IdFactura);
            if (Factura == null) throw new ApiError("Factura Not Found", (int)HttpStatusCode.NotFound);
            //Factura.Pago = new Pago() { Id = Guid.NewGuid(), Monto = Factura.Monto };
            // var FacturaPago = new Pago() { Id = Guid.NewGuid(), Monto = Factura.Monto };

            //Factura.Pagada = true;
            this.Store.Update(Factura);




        }

        public void Update(Guid Id, UpdateFacturaRequestDataType Data)
        {
            var Factura = this.Store.GetById(Id);
            if (Factura == null) throw new ApiError("Factura no encontrada", (int)HttpStatusCode.NotFound);

            if(Factura.Pago != null) throw new ApiError("Factura pagada", (int)HttpStatusCode.NotFound);

            Mapper.Map(Data, Factura);

            this.Store.Update(Factura);
        }

        public void GenerateBills()
        {
            var Tenants = this.TenantsStore.Get(new string[] { "Product" }).ToList();

            foreach (var Tenant in Tenants)
            {
                var Precio = this.PrecioStore.GetActual(Tenant.ProductId);
                if(Precio != null)
                {
                    CreateFacturaRequestDataType Data = new CreateFacturaRequestDataType()
                    {
                        Monto = Precio.Amount,
                        Descripcion = Tenant.Product.Name
                    };

                    var NewFactura = new Factura() { Id = Guid.NewGuid(), TenantId = Tenant.Id };
                    Mapper.Map(Data, NewFactura);

                    this.Store.Create(NewFactura);
                }
            }
        }
    }
}
