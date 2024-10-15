using Application.Services;
using Core.Models;
using glinoMISIS_back.Contracts.Users;
using glinoMISIS_back.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json;

namespace glinoMISIS_back.Endpoints
{
    public static class UserEndpoint
    {
        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder builder)
        {
            builder.MapPost("Register", Register);
            builder.MapPost("Login", Login);
            builder.MapGet("GetCompartments", GetCompartments).RequireAuthorization();
            builder.MapPost("AddCompartment", AddCompartment);
            builder.MapGet("GetAuthEmployee", GetAuthEmployee);
            return builder;
        }
        public static async Task<IResult> Register(UserService userService, RegisterRequest request, HttpContext context)
        {
            await userService.Register(request.Employee, request.password);
            return await Login(new() { login = request.Employee.Login, password = request.password}, userService, context);
            
        }
        public static async Task<IResult> Login( LoginRequest loginRequest, UserService userService, HttpContext context)
        {
            var token = await userService.Login(loginRequest.login, loginRequest.password);
            context.Response.Cookies.Append("notjwttoken", token);
            
            return Results.Ok(token);
        }
        public static async Task<IResult> GetAuthEmployee(UserService userService, HttpContext context)
        {
            if (context.Request.Cookies.ContainsKey("notjwttoken"))
            {
                var cock = context.Request.Cookies["notjwttoken"];
                string aboba = ApiExtentions.DecipherJWT(cock!);
                PrivateEmployee employee = await userService.GetPrivateByLogin(aboba);
                string json = JsonSerializer.Serialize(employee, options: new() { WriteIndented = true});
                return Results.Ok(json);
            }
            else
            {
                return Results.Unauthorized();
            }
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
