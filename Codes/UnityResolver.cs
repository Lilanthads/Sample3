using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using IJSE.POS.Common.Models;
using IJSE.POS.Common.Logs;
using IJSE.POS.DataAccess.DAL.Contracts;
using IJSE.POS.DataAccess.DAL;
using IJSE.POS.BusinessService.Contracts;
using IJSE.POS.BusinessService;

using Microsoft.Practices.Unity;


namespace IJSE.POS.Common.Ioc
{
    public static class UnityResolver
    {
        private static readonly IUnityContainer Container = new UnityContainer();

        public static void Register()
        {
            Container.RegisterType<IRepository, Repository>();
            Container.RegisterType<ILogger, LoggerB>();

            Container.RegisterType<IInvoiceManger, InvoiceManger>();
        }

        public static T Resolve<T>()
        {
            T defaultT = default(T);
            var resolved = Container.Resolve<T>();
            return (resolved == null) ? defaultT : resolved;
        }

        public static IUnityContainer GetContainer()
        {
            return Container;
        }
    }
}
