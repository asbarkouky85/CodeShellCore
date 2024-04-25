using CodeShellCore.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;


namespace CodeShellCore.Web.Conventions
{
    public class CodeShellApplicationModelConvention : IApplicationModelConvention
    {
        static Dictionary<string, string[]> methodToActionNames = new Dictionary<string, string[]>
        {
            { "GET" ,new []{ "Get","Index","IsUnique","Has","Can","Count" } },
            { "PUT",new []{ "Put", "Update" } },
            { "DELETE",new []{ "Delete"} }
        };

        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                foreach (var action in controller.Actions)
                {
                    var method = GetHttpMethod(action);
                    AddRouteAttribute(action);
                    foreach (var param in action.Parameters)
                    {
                        if (param.Name == "id")
                        {
                            param.BindingInfo = BindingInfo.GetBindingInfo(new IBindingSourceMetadata[] {
                                new FromRouteAttribute(),
                                new FromQueryAttribute()
                            });
                        }
                        else if (!param.Attributes.Any())
                        {
                            if (IsQueryParameterType(param) || method == "GET" || method == "DELETE")
                            {
                                param.BindingInfo = BindingInfo.GetBindingInfo(new[] { new FromQueryAttribute() });
                            }
                            else
                            {
                                param.BindingInfo = BindingInfo.GetBindingInfo(new[] { new FromBodyAttribute() });
                            }
                        }

                    }
                }
            }
        }

        private string GetHttpMethod(ActionModel model)
        {
            if (model.Selectors.Any())
            {
                foreach (var selector in model.Selectors)
                {
                    var httpMethod = selector.ActionConstraints
                       .OfType<HttpMethodActionConstraint>()
                       .FirstOrDefault()?
                       .HttpMethods?
                       .FirstOrDefault();
                    if (httpMethod != null)
                        return httpMethod;
                }
            };

            string method = "POST";


            foreach (var m in methodToActionNames)
            {
                foreach (var prefix in m.Value)
                {
                    if (model.ActionName.StartsWith(prefix))
                    {
                        method = m.Key;
                        break;
                    }
                }
            }

            foreach (var param in model.Parameters)
            {
                if (param.Attributes.Any(e => e is FromBodyAttribute) && (new[] { "GET", "DELETE" }).Contains(method))
                {
                    method = "POST";
                    break;
                }
            }

            return method;
        }

        protected virtual void AddRouteAttribute(ActionModel action)
        {
            var httpMethod = GetHttpMethod(action);

            if (!action.Selectors.Any())
            {
                action.Selectors.Add(new SelectorModel
                {
                    AttributeRouteModel = CreateAbpServiceAttributeRouteModel(action.Controller.ControllerName, action, httpMethod),
                    ActionConstraints = { new HttpMethodActionConstraint(new[] { httpMethod }) }
                });
            }
            else
            {
                foreach (var selector in action.Selectors)
                {
                    httpMethod = selector.ActionConstraints
                       .OfType<HttpMethodActionConstraint>()
                       .FirstOrDefault()?
                       .HttpMethods?
                       .FirstOrDefault();

                    if (httpMethod == null)
                    {
                        httpMethod = GetHttpMethod(action);
                    }

                    if (selector.AttributeRouteModel == null)
                    {
                        selector.AttributeRouteModel = CreateAbpServiceAttributeRouteModel(action.Controller.ControllerName, action, httpMethod);
                    }

                    if (!selector.ActionConstraints.OfType<HttpMethodActionConstraint>().Any())
                    {
                        selector.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { httpMethod }));
                    }
                }
            }

        }

        protected virtual AttributeRouteModel CreateAbpServiceAttributeRouteModel(string controllerName, ActionModel action, string httpMethod)
        {
            return new AttributeRouteModel(
                new RouteAttribute(
                    RouteUrl(action)
                )
            );
        }

        private string RouteUrl(ActionModel model)
        {
            string id = model.Parameters.Any(e => e.Name.ToLower() == "id") ? "/{id?}" : "";
            string actionName = model.ActionName == "Index" ? "" : $"/{model.ActionName}";
            string controller = model.Controller.ControllerName == "Home" && model.ActionName == "Index" ? "" : $"apiAction/{model.Controller.ControllerName}";
            return $"{controller}{actionName}{id}";
        }

        private bool IsQueryParameterType(ParameterModel model)
        {

            var type = model.ParameterType.RealType();
            if (model.ParameterName.ToLower() == "id")
                return false;
            if (type.IsPrimitive)
            {
                return true;
            }

            if (type.IsEnum)
            {
                return true;
            }

            return type == typeof(string) ||
                   type == typeof(decimal) ||
                   type == typeof(DateTime) ||
                   type == typeof(DateTimeOffset) ||
                   type == typeof(TimeSpan) ||
                   type == typeof(Guid);
        }
    }
}
