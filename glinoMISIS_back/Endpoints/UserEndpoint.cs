using Application.Services;
using Core.Models;
using glinoMISIS_back.Contracts.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace glinoMISIS_back.Endpoints
{
    public static class UserEndpoint
    {
        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder builder)
        {
            builder.MapPost("/register", Register);
            builder.MapPost("/login", Login);
            return builder;
        }
        public static async Task<IResult> Register(UserService userService, RegisterRequest request)
        {
            await userService.Register(request.Employee, request.password);
            return Results.Ok();
        }
        public static async Task<IResult> Login( LoginRequest loginRequest, UserService userService)
        {
            var token = await userService.Login(loginRequest.login, loginRequest.password);
            return Results.Ok(token);
        }
    }
}
