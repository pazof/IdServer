// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using IdentityServer4.Models;

namespace nuget_host.Models
{
    public class ConsentInputModel
    {
        public string Button { get; set; }
        public IEnumerable<string> ScopesConsented { get; set; }
        public bool RememberConsent { get; set; }
        public string ReturnUrl { get; set; }
        public string Description { get; set; }
        public string ClientName { get; internal set; }
        public string ClientUri { get; internal set; }
        public string LogoUri { get; internal set; }
        public bool AllowRememberConsent { get; internal set; }
        public ValidatedResources ValidatedResources { get; internal set; }
        public Client Client { get; internal set; }
    }
}