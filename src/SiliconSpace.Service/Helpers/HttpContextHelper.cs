﻿using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace SiliconSpace.Service.Helpers
{
    public static class HttpContextHelper
    {
        public static IHttpContextAccessor Accessor { get; set; }
        public static HttpContext HttpContext => Accessor?.HttpContext;
        public static IHeaderDictionary ResponseHeaders => HttpContext?.Response?.Headers;
        public static long? UserId => GetClaimValueAsLong("Id");
        public static string UserRole => GetClaimValue("RoleId");
        private static long? GetClaimValueAsLong(string claimType)
        {
            if (long.TryParse(HttpContext?.User?.FindFirst(claimType)?.Value, out var value))
            {
                return value;
            }

            return null;
        }

        private static int? GetClaimValueAsInt(string claimType)
        {
            if (int.TryParse(HttpContext?.User?.FindFirst(claimType)?.Value, out var value))
            {
                return value;
            }

            return null;
        }

        private static string GetClaimValue(string claimType)
        {
            return HttpContext?.User?.FindFirst(claimType)?.Value;
        }
    }
}
