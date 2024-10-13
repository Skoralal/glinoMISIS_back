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
            builder.MapPost("Register", Register);
            builder.MapPost("Login", Login);
            builder.MapGet("GetCompartments", GetCompartments);
            builder.MapPost("AddCompartment", AddCompartment);
            return builder;
        }
        public static async Task<IResult> Register(UserService userService, RegisterRequest request)
        {
            await userService.Register(request.Employee, request.password);
            return await Login(new() { login = request.Employee.Login, password = request.password}, userService);
            
        }
        public static async Task<IResult> Login( LoginRequest loginRequest, UserService userService)
        {
            var token = await userService.Login(loginRequest.login, loginRequest.password);
            return Results.Ok(token);
        }
        public static async Task<IResult> GetCompartments(UserService userService)
        {
            var Compartments = await userService.GetAllCompartments();
            return Results.Ok(Compartments);
        }
        public static async Task<IResult> AddCompartment([FromBody] Compartment compartment, UserService userService)
        {
            await userService.AddCompartment(compartment);
            return Results.Ok();
        }
    }
}
