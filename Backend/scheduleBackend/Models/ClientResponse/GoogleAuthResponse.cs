using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace scheduleBackend.Models.ClientResponse
{
    

        public partial class GoogleAuthResponse
        {
            [JsonProperty("Aa")]
            public string Aa { get; set; }

            [JsonProperty("tc")]
            public Tc Tc { get; set; }

            [JsonProperty("Qs")]
            public Qs Qs { get; set; }

            [JsonProperty("googleId")]
            public string GoogleId { get; set; }

            [JsonProperty("tokenObj")]
            public Tc TokenObj { get; set; }

            [JsonProperty("tokenId")]
            public string TokenId { get; set; }

            [JsonProperty("accessToken")]
            public string AccessToken { get; set; }

            [JsonProperty("profileObj")]
            public ProfileObj ProfileObj { get; set; }
        }

        public partial class ProfileObj
        {
            [JsonProperty("googleId")]
            public string GoogleId { get; set; }

            [JsonProperty("imageUrl")]
            public Uri ImageUrl { get; set; }

            [JsonProperty("email")]
            public string Email { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("givenName")]
            public string GivenName { get; set; }

            [JsonProperty("familyName")]
            public string FamilyName { get; set; }
        }

        public partial class Qs
        {
            [JsonProperty("ER")]
            public string Er { get; set; }

            [JsonProperty("Te")]
            public string Te { get; set; }

            [JsonProperty("oT")]
            public string OT { get; set; }

            [JsonProperty("kR")]
            public string KR { get; set; }

            [JsonProperty("EI")]
            public Uri Ei { get; set; }

            [JsonProperty("zt")]
            public string Zt { get; set; }
        }

        public partial class Tc
        {
            [JsonProperty("token_type")]
            public string TokenType { get; set; }

            [JsonProperty("access_token")]
            public string AccessToken { get; set; }

            [JsonProperty("scope")]
            public string Scope { get; set; }

            [JsonProperty("login_hint")]
            public string LoginHint { get; set; }

            [JsonProperty("expires_in")]
            public long ExpiresIn { get; set; }

            [JsonProperty("id_token")]
            public string IdToken { get; set; }

            [JsonProperty("session_state")]
            public SessionState SessionState { get; set; }

            [JsonProperty("first_issued_at")]
            public long FirstIssuedAt { get; set; }

            [JsonProperty("expires_at")]
            public long ExpiresAt { get; set; }

            [JsonProperty("idpId")]
            public string IdpId { get; set; }
        }

        public partial class SessionState
        {
            [JsonProperty("extraQueryParams")]
            public ExtraQueryParams ExtraQueryParams { get; set; }
        }

        public partial class ExtraQueryParams
        {
            [JsonProperty("authuser")]
            public long Authuser { get; set; }
        }
    

}
