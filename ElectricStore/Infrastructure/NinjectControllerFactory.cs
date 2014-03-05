using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Store.Domain.Entities;
using Store.Domain.Abstract;
using System.Collections.Generic;
using System.Linq;
using Moq;
using System.Configuration;
using Store.Domain.Concrate;
using Store.Infrastructure.Abstract;
using Store.Infrastructure.Concrete;

namespace Store.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requereContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<IProductRepository>().To<EFProductRepository>();
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager
                  .AppSettings["Email.WriteAsFile"] ?? "true")
            };
            ninjectKernel.Bind<IOrderProcessor>()
              .To<EmailOrderProcessor>()
              .WithConstructorArgument("settings", emailSettings);
            ninjectKernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
        }
    }
}