using AutoMapper;
using BLL.Base;
using Contracts.BLL.App;
using Contracts.DAL.App;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBll
    {
        protected IMapper Mapper;

        public AppBLL(IAppUnitOfWork uow) : base(uow)
        {
        }
        //TODO: Näude
        //public IDimensionService Dimensions =>
        //    GetService<IDimensionService>(() => new DimensionService(Uow, Uow.Dimensions, Mapper));

    }
}