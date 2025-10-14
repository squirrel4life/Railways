using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Railways
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "About",
                url: "about",
                defaults: new { controller = "Home", action = "About" }
                );

            routes.MapRoute(
                name: "Contact",
                url: "contact",
                defaults: new { controller = "Home", action = "Contact" }
                );

            routes.MapRoute(
                name: "Login",
                url: "login",
                defaults: new { controller = "Account", action = "Login" }
                );

            routes.MapRoute(
                name: "Register",
                url: "register",
                defaults: new { controller = "Account", action = "Register" }
                );

            routes.MapRoute(
                name: "Disassociate",
                url: "disassociate",
                defaults: new { controller = "Account", action = "Disassociate" }
                );

            routes.MapRoute(
                name: "Manage",
                url: "manage",
                defaults: new { controller = "Account", action = "Manage" }
                );

            routes.MapRoute(
                name: "ExtLog",
                url: "login/external",
                defaults: new { controller = "Account", action = "ExternalLogin" }
                );

            routes.MapRoute(
                name: "ExtLogCallback",
                url: "login/external/callback",
                defaults: new { controller = "Account", action = "ExternalLoginCallback" }
                );

            routes.MapRoute(
                name: "LinkLog",
                url: "login/link",
                defaults: new { controller = "Account", action = "LinkLogin" }
                );

            routes.MapRoute(
                name: "LinkLogCallback",
                url: "login/link/callback",
                defaults: new { controller = "Account", action = "LinkLoginCallback" }
                );

            routes.MapRoute(
                name: "ExtLogConfirm",
                url: "login/external/confirm",
                defaults: new { controller = "Account", action = "ExternalLoginConfirmation" }
                );

            routes.MapRoute(
               name: "LogOff",
               url: "logoff",
               defaults: new { controller = "Account", action = "logoff" }
               );

            routes.MapRoute(
               name: "ExternalLoginFailure",
               url: "login/external/fail",
               defaults: new { controller = "Account", action = "ExternalLoginFailure" }
               );

            routes.MapRoute(
              name: "RemoveAccountList",
              url: "accounts/list/remove",
              defaults: new { controller = "Account", action = "RemoveAccountList" }
              );

            routes.MapRoute(
                name: "Search",
                url: "find",
                defaults: new { controller = "Trains", action = "FindTrains" }
                );

            routes.MapRoute(
                name: "SearchInput",
                url: "search/input",
                defaults: new { controller = "Trains", action = "Input" }
                );

            routes.MapRoute(
                name: "StationTrains",
                url: "station/schedule",
                defaults: new { controller = "Routes", action = "StationTrains" }
                );

            routes.MapRoute(
                name: "MyProfile",
                url: "profile",
                defaults: new { controller = "Account", action = "MyProfile" }
                );

            routes.MapRoute(
                name: "EditProfile",
                url: "profile/edit",
                defaults: new { controller = "Account", action = "EditProfile" }
                );


            routes.MapRoute(
                name: "ShowCarriages",
                url: "trains/id{id}/carriages",
                defaults: new { controller = "Trains", action = "ShowCarriages", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "ShowSeats",
                url: "carriages/id{id}/seats",
                defaults: new { controller = "Carriages", action = "ShowSeats", id = UrlParameter.Optional }
                );


            routes.MapRoute(
                name: "TrainFullPath",
                url: "fullpath/{id}",
                defaults: new { controller = "Routes", action = "ShowFullPath", id = UrlParameter.Optional }
                );


            routes.MapRoute(
                name: "TrainsList",
                url: "trains",
                defaults: new { controller = "Trains", action = "Index" }
                );



            routes.MapRoute(
                name: "TrainUpdate",
                url: "trains/update/id{id}",
                defaults: new { controller = "Trains", action = "Edit", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "TrainDelete",
                url: "trains/delete/id{id}",
                defaults: new { controller = "Trains", action = "Delete", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "TrainCreate",
                url: "trains/new",
                defaults: new { controller = "Trains", action = "Create" }
                );

            routes.MapRoute(
               name: "Order",
               url: "tickets/create",
               defaults: new { controller = "Tickets", action = "Create"}
               );

            routes.MapRoute(
              name: "OrderCofirmed",
              url: "tickets/details/id{id}",
              defaults: new { controller = "Tickets", action = "Details", id = UrlParameter.Optional }
              );

            routes.MapRoute(
              name: "OrderList",
              url: "tickets",
              defaults: new { controller = "Tickets", action = "Index" }
              );

            routes.MapRoute(
              name: "OrderCancel",
              url: "tickets/cancel/{id}",
              defaults: new { controller = "Tickets", action = "Delete", id = UrlParameter.Optional }
              );

            routes.MapRoute(
                           name: "Train",
                           url: "trains/route/id{id}",
                           defaults: new { controller = "Trains", action = "Details", id = UrlParameter.Optional }
                           );

            routes.MapRoute(
                           name: "User",
                           url: "users/profile/id{id}",
                           defaults: new { controller = "Passengers", action = "Details", id = UrlParameter.Optional }
                           );

            routes.MapRoute(
                name: "UsersList",
                url: "users",
                defaults: new { controller = "Passengers", action = "Index" }
                );



            routes.MapRoute(
                name: "UserUpdate",
                url: "users/update/id{id}",
                defaults: new { controller = "Passengers", action = "Edit", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "UserDelete",
                url: "users/delete/id{id}",
                defaults: new { controller = "Passengers", action = "Delete", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "UserCreate",
                url: "users/new",
                defaults: new { controller = "Passengers", action = "Create" }
                );
        }
    }
}
