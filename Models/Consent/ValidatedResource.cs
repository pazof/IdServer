// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdServer.Models
{
    public class ValidatedResources
    {
        public Resources Resources { get; set; }
        public ParsedScopes ParsedScopes { get; internal set; }
        public bool OfflineAccess { get; internal set; }

    }
}