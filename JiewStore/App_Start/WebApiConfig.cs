using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using AutoMapper;
using JiewStore.DAL;
using JiewStore.Models;

namespace JiewStore
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // AutoMapper
            Mapper.Initialize((cfg) =>
            {
                cfg.CreateMap<Customer, CustomerDto>()
                            .ForMember(d => d.ID, opt => opt.MapFrom(s => s.ID))
                            .ForMember(d => d.Code, opt => opt.MapFrom(s => s.Code))
                            .ForMember(d => d.Facebook, opt => opt.MapFrom(s => s.Facebook))
                            .ForMember(d => d.NickName, opt => opt.MapFrom(s => s.NickName))
                            .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.FullName))
                            .ForMember(d => d.Phone, opt => opt.MapFrom(s => s.Phone))
                            .ForMember(d => d.Address, opt => opt.MapFrom(s => s.Address))
                            .ForMember(d => d.BirthDate, opt => opt.MapFrom(s => s.BirthDate))
                            .ForMember(d => d.RemainingPoint, opt => opt.Ignore())
                            .ForMember(d => d.TierName, opt => opt.Ignore());
            });

            Mapper.AssertConfigurationIsValid();

            // Use camel case for JSON data.
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
