﻿namespace RuiChen.AbpPro.OpenIddict
{
    public static class OpenIddictApplicationErrorCodes
    {
        private const string Namespace = "OpenIddict:";

        public static class Applications
        {
            public const string Prefix = Namespace + ":001";
            public const string ClientIdExisted = Prefix + "001";
        }

        public static class Scopes
        {
            public const string Prefix = Namespace + ":002";
            public const string NameExisted = Prefix + "001";
        }

        public static class Authorizations
        {
            public const string Prefix = Namespace + ":003";
        }

        public static class Tokens
        {
            public const string Prefix = Namespace + ":004";
        }
    }

}


