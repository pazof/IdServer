// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using IdentityServer4.Models;

namespace IdentityServerHost.Quickstart.UI
{
    public class ParsedScopes 
    {
        public ParsedScopes(ParsedSecret secret)
        {
            RawValue = secret.Properties.ContainsKey(KEY_SCOPES) ? null : secret.Properties[KEY_SCOPES];
            Emphasize = secret.Properties.ContainsKey(KEY_OL);
            if (secret.Properties.ContainsKey(KEY_SCOPES)) Scopes = secret.Properties[KEY_SCOPES].Split(',');
        }

        public const string KEY_SCOPES = "scopes";
        public const string KEY_OL = "ol";
        public string RawValue { 
            get ;
            }


        public string[] Scopes { get ; protected set; }
        public bool Emphasize { get;  }
    }
}