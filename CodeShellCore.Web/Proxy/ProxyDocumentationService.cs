using CodeShellCore.Proxy;
using CodeShellCore.Types;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeShellCore.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;

namespace CodeShellCore.Web.Proxy
{
    public class ProxyDocumentationService : IProxyDocumentationService
    {
        private readonly IApiDescriptionGroupCollectionProvider provider;
        protected readonly InstanceStore Store;
        public List<Type> _usedModels;

        public ProxyDocumentationService(IServiceProvider serviceProvider, IApiDescriptionGroupCollectionProvider provider)
        {
            this.provider = provider;
            Store = new InstanceStore(serviceProvider);
        }

        private Dictionary<string, SchemaItemDto> _generateSchemas()
        {
            var result = new Dictionary<string, SchemaItemDto>();
            _usedModels = _usedModels.OrderBy(e => e.FullName).ToList();
            foreach (var type in _usedModels)
            {
                var schemaName = $"{type.Namespace}.{type.Name}";
                if (!result.ContainsKey(schemaName))
                {
                    result[schemaName] = new SchemaItemDto();
                    result[schemaName].Name = type.Name;
                    if (type.IsEnum)
                    {
                        result[schemaName].Items = new Dictionary<string, long>();

                        foreach (var v in Enum.GetValues(type))
                        {
                            result[schemaName].Items[v.ToString()] = (long)Convert.ChangeType(v, typeof(long));
                        }
                    }
                    else if (type.IsGenericType)
                    {
                        result[schemaName].GenericArguments = type.GetGenericTypeDefinition().GetGenericArguments()
                            .Select(e => e.Name).ToList();
                    }
                    _addProperties(type, result[schemaName]);
                }
            }
            return result;
        }

        private void _addProperties(Type type, IHasProperties dto)
        {
            var props = type.GetProperties();
            if (type.IsGenericType)
            {
                props = type.GetGenericTypeDefinition().GetProperties();
            }
            foreach (var prop in props)
            {
                var schemaProp = new PropertyDto();
                _fillType(schemaProp, prop.PropertyType, false);
                if (prop.Name == "Id")
                    schemaProp.Nullable = false;
                dto.Properties[prop.Name] = schemaProp;
            }
        }

        private void _addToSchema(Type type)
        {

            if (!_usedModels.Exists(e => e == type))
            {
                _usedModels.Add(type);
                if (type.IsGenericType)
                {
                    foreach (var item in type.GetGenericArguments())
                    {
                        _addToSchema(item);
                    }
                }

                var tsService = Store.GetRequiredService<ITypeScriptGenerationService>();
                var props = type.GetProperties();

                foreach (var prop in props)
                {
                    var tsType = tsService.GetTsType(prop.PropertyType);
                    if (tsType == "reference")
                    {
                        _addToSchema(prop.PropertyType);
                    }
                    else if (prop.PropertyType.IsGenericType)
                    {
                        foreach (var item in prop.PropertyType.GetGenericArguments())
                        {
                            _addToSchema(item);
                        }
                    }
                }

            }


        }

        private void _fillType(PropertyDto parameterProp, Type type, bool isCollecting = true)
        {
            var tsService = Store.GetRequiredService<ITypeScriptGenerationService>();
            parameterProp.Type = tsService.GetTsType(type);
            parameterProp.Namespace = type.Namespace;
            parameterProp.SchemaName = $"{type.Namespace}.{type.Name}";
            parameterProp.Nullable = !tsService.IsRequired(type);
            if (type.IsGenericType)
            {
                parameterProp.GenericArguments = new List<PropertyDto>();
                foreach (var t in type.GetGenericArguments())
                {
                    var arg = new PropertyDto();
                    _fillType(arg, type.GetGenericArguments()[0], isCollecting);
                    parameterProp.GenericArguments.Add(arg);
                }
            }

            if (isCollecting && parameterProp.Type == "reference")
            {
                _addToSchema(type);

            }
        }

        private PropertyDto _getParameterPropertyDto(ApiParameterDescription parameterDesc)
        {
            var parameterProp = new PropertyDto();
            _fillType(parameterProp, parameterDesc.Type);
            parameterProp.Nullable = !parameterDesc.IsRequired;
            return parameterProp;
        }

        private ResponsePropertyDto _getResponsePropertyDto(ApiResponseType parameterDesc)
        {
            var parameterProp = new ResponsePropertyDto();
            _fillType(parameterProp, parameterDesc.Type);
            parameterProp.ContentTypes = parameterDesc.ApiResponseFormats.Select(e => e.MediaType).ToList();
            return parameterProp;
        }

        private Dictionary<string, PropertyDto> _getRouteParameter(ControllerActionDescriptor actionData, ApiDescription parameterDesc)
        {
            var result = new Dictionary<string, PropertyDto>();
            var routeParameters = parameterDesc.ParameterDescriptions.Where(e => e.Source == BindingSource.Path)
                            .ToList();

            foreach (var routeParameter in routeParameters)
            {
                var parameterProp = new PropertyDto();
                var routerParameterType = routeParameter.Type;
                if (routerParameterType == null)
                {
                    var action = actionData.MethodInfo.GetParameters();
                    routerParameterType = actionData.MethodInfo.GetParameters()
                        .Where(e => e.Name.ToLower() == routeParameter.Name.ToLower())
                        .Select(e => e.ParameterType)
                        .FirstOrDefault();
                }

                if (routerParameterType != null)
                    _fillType(parameterProp, routerParameterType);
                else
                    parameterProp.Type = "any";
                result[routeParameter.Name] = parameterProp;
            }

            return result;

        }

        private Dictionary<string, ParameterPropertyDto> _getQueryParameters(ControllerActionDescriptor actionData, ApiDescription parameterDesc)
        {
            var result = new Dictionary<string, ParameterPropertyDto>();
            var queryParameterTypes = parameterDesc.ParameterDescriptions.Where(e => e.Source == BindingSource.Query)
                            .GroupBy(e => e.ModelMetadata.ContainerType ?? e.Type)
                            .Select(e => e.Key)
                            .ToList();

            foreach (var parameterType in queryParameterTypes)
            {
                var methodParameter = actionData.MethodInfo.GetParameters().FirstOrDefault(e => e.ParameterType == parameterType);
                if (methodParameter != null)
                {
                    var parameterProp = new ParameterPropertyDto();
                    _fillType(parameterProp, parameterType);
                    _addProperties(parameterType, parameterProp);
                    result[methodParameter.Name] = parameterProp;
                }
            }
            return result;
        }

        private Dictionary<string, ResponsePropertyDto> _getResponses(ApiDescription parameterDesc)
        {
            var result = new Dictionary<string, ResponsePropertyDto>();
            foreach (var responseDesc in parameterDesc.SupportedResponseTypes)
            {
                result[responseDesc.StatusCode.ToString()] = _getResponsePropertyDto(responseDesc);
            }
            return result;
        }

        private PropertyDto _getRequestBody(ApiDescription parameterDesc)
        {
            var body = parameterDesc.ParameterDescriptions.FirstOrDefault(e => e.Source == BindingSource.Body);

            if (body != null)
            {
                return _getParameterPropertyDto(body);
            }
            return null;
        }

        public Task<DocumentDto> GetDocument()
        {
            var result = new DocumentDto();
            _usedModels = new List<Type>();

            foreach (var group in provider.ApiDescriptionGroups.Items)
            {
                foreach (var item in group.Items)
                {
                    if (item.ActionDescriptor is ControllerActionDescriptor)
                    {
                        var actionData = (ControllerActionDescriptor)item.ActionDescriptor;

                        var module = "app";
                        if (actionData.RouteValues.TryGetValue("area", out string area) && !string.IsNullOrEmpty(area))
                            module = area;
                        if (!result.Modules.ContainsKey(module))
                            result.Modules[module] = new GroupDto();
                        if (actionData.ActionName == "MakeQRCode")
                        {

                        }
                        var resultAction = new ActionDto();

                        resultAction.Path = item.RelativePath;
                        resultAction.Method = item.HttpMethod.ToLower();
                        resultAction.RouteParameters = _getRouteParameter(actionData, item);
                        resultAction.Parameters = _getQueryParameters(actionData, item);
                        resultAction.RequestBody = _getRequestBody(item);
                        resultAction.Responses = _getResponses(item);

                        if (!result.Modules[module].Services.ContainsKey(actionData.ControllerName))
                        {
                            result.Modules[module].Services[actionData.ControllerName] = new ServiceDefinitionDto
                            {
                                Namespace = actionData.ControllerTypeInfo.Namespace
                            };
                        }

                        result.Modules[module].Services[actionData.ControllerName].Actions[actionData.MethodInfo.Name] = resultAction;
                    }
                }
            }
            result.Schemas = _generateSchemas();
            return Task.FromResult(result);
        }
    }
}
