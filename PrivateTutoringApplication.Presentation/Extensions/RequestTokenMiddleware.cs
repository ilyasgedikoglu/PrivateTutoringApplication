﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using PrivateTutoringApplication.Presentation.Middlewares;

namespace PrivateTutoringApplication.Presentation.Extensions
{
    public static class RequestTokenMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestToken(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TokenKontrolMiddleware>();
        }
    }
}
