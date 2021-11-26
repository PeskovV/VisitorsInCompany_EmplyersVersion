using System;
using AutoMapper;
using MediatR;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using VisitorsInCompany.View.Profiles;

namespace VisitorsInCompanyMain
{
   class CustomApp : MvxApplication
   {
      public override void Initialize()
      {
         RegisterCustomAppStart<AppStart>();
         
         var config = new MapperConfiguration(c =>
         {
            c.AddProfile(new VisitorProfile());
         });
         Mvx.IoCProvider.RegisterType(config.CreateMapper);
         
         CreatableTypes()
            .EndingWith("Handler")
            .AsInterfaces()
            .RegisterAsLazySingleton();
         Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IMediator, Mediator>();
         
         Mvx.IoCProvider.RegisterSingleton<ServiceFactory>((Type serviceType) =>
         {
            var resolver = Mvx.IoCProvider.Resolve<IMvxIoCProvider>();
            try
            {
               return resolver.Resolve(serviceType);
            }
            catch (Exception)
            {
               return Array.CreateInstance(serviceType.GenericTypeArguments[0], 0);
            }
         });
      }
   }
}
