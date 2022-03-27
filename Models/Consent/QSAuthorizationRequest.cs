// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace IdServer.OAuth
{
    internal class NHAuthorizationRequest : AuthorizationRequest
    {
        internal NHAuthorizationRequest(ValidatedAuthorizeRequest request) : base()
        {
            Client = new Client { ClientId = request.ClientId };
            RedirectUri = request.RedirectUri;
            DisplayMode = request.DisplayMode;
            UiLocales = request.UiLocales;
            IdP = request.GetIdP();
            Tenant = request.GetTenant();
            LoginHint = request.LoginHint;
            DisplayMode = request.DisplayMode;
            AcrValues = request.GetAcrValues();
            foreach (var scope in  request.RequestedScopes)
            RequestObjectValues.Add(scope, request.RequestObjectValues[scope]);
            Parameters.Add(request.Raw);
        }

    }
}