// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using IdentityServer4.Validation;
using NuGet.Packaging;

namespace nuget_host.OAuth
{
    internal class NHAuthorizationRequest : AuthorizationRequest
    {
        internal NHAuthorizationRequest(ValidatedAuthorizeRequest request) : base()
        {
            ClientId = request.ClientId;
            RedirectUri = request.RedirectUri;
            DisplayMode = request.DisplayMode;
            UiLocales = request.UiLocales;
            IdP = request.GetIdP();
            Tenant = request.GetTenant();
            LoginHint = request.LoginHint;
            PromptMode = request.PromptMode;
            AcrValues = request.GetAcrValues();
            ScopesRequested = request.RequestedScopes;
            Parameters.Add(request.Raw);
            RequestObjectValues.AddRange(request.RequestObjectValues);
        }

    }
}