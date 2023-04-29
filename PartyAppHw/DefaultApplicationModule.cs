using PartyAppHw.Services;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyAppHw
{
    public class DefaultApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DeliveryInformationRequestsService>()
                .InstancePerRequest();

            builder.RegisterType<Models.PartyEntities>()
             .InstancePerRequest();
        }
    }
}