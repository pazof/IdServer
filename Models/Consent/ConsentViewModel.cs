// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;

namespace IdServer.Models
{
    public class ConsentViewModel : ConsentInputModel
    {
        public string ClientLogoUrl { get; set; }
        public string ClientUrl { get; set; }

        public IEnumerable<ScopeViewModel> ApiScopes { get; set; }

        public IEnumerable<ScopeViewModel> IdentityScopes { get; set; }
        

    }
}
