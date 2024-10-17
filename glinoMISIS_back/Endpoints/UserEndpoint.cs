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
            builder.MapGet("GetPublicEmployee", GetPublicEmployee);
            builder.MapGet("GetFellas", GetFellas);
            builder.MapPost("UpdateMyself", UpdateMyself);
            builder.MapPost("UpdateProfilePic", UpdateProfilePic);
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
                if(aboba == null)
                {
                    return Results.Unauthorized();
                }
                PrivateEmployee employee = await userService.GetPrivateByLogin(aboba);
                string json = JsonSerializer.Serialize(employee);
                return Results.Ok(json);
            }
            else
            {
                return Results.Unauthorized();
            }
        }
        public static async Task<IResult> GetPublicEmployee(UserService userService, HttpContext context, [FromQuery] string login)
        {
            PublicEmployee employee = await userService.GetPublicByLogin(login);
            string json = JsonSerializer.Serialize(employee);
            return Results.Ok(json);
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
        public static async Task<IResult> UpdateMyself([FromBody] Employee employee, UserService userService, HttpContext context)
        {
            
            if (context.Request.Cookies.ContainsKey("notjwttoken"))
            {
                var cock = context.Request.Cookies["notjwttoken"];
                string? aboba = ApiExtentions.DecipherJWT(cock!);
                if (aboba == null)
                {
                    return Results.Unauthorized();
                }
                employee.Login = aboba;
                var result = await userService.UpdateEmployee(employee);
                if (result)
                {
                    return Results.Ok(result);

                }
                return Results.Unauthorized();
            }
            else
            {
                return Results.Unauthorized();
            }
        }
        public static async Task<IResult> GetFellas(UserService userService, HttpContext context)
        {
            if (context.Request.Cookies.ContainsKey("notjwttoken"))
            {
                var cock = context.Request.Cookies["notjwttoken"];
                string? aboba = ApiExtentions.DecipherJWT(cock!);
                if (aboba == null)
                {
                    return Results.Unauthorized();
                }
                PrivateEmployee employee = await userService.GetPrivateByLogin(aboba);
                int compartmentID = employee.CurrentConpartmentID;
                List<PublicEmployee> querry = await userService.GetFellasFromCompartment(compartmentID);
                return Results.Ok(JsonSerializer.Serialize(querry));
            }
            else
            {
                return Results.Unauthorized();
            }
        }
        [IgnoreAntiforgeryToken]
        public static async Task<IResult> UpdateProfilePic([FromForm] Pic model, UserService userService, HttpContext context)
        {
            if (context.Request.Cookies.ContainsKey("notjwttoken"))
            {
                var cock = context.Request.Cookies["notjwttoken"];
                string? aboba = ApiExtentions.DecipherJWT(cock!);
                if (aboba == null)
                {
                    return Results.Unauthorized();
                }
                if (model.ImageFile == null || model.ImageFile.Length == 0)
                    return Results.BadRequest("No image file received.");

                // Define where to save the image (e.g., "wwwroot/images" folder)
                var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                // Ensure the directory exists
                if (!Directory.Exists(savePath))
                    Directory.CreateDirectory(savePath);

                // Create a unique file name
                var fileName = aboba + Path.GetExtension(model.ImageFile.FileName);

                // Combine the save path with the file name
                var fullPath = Path.Combine(savePath, fileName);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                // Save the file to the server
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                // Return the image URL or success message
                var imageUrl = Path.Combine("images", fileName); // Relative URL
                await userService.UpdateProfilePic(aboba, imageUrl);
                return Results.Ok(new { imageUrl });
            }
            else
            {
                return Results.Unauthorized();
            }
        }
    }
}
