using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorMovies.Server.Migrations
{
    public partial class adminuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a729e84c-d741-4c50-99d8-f2f4e4ae38d2", 0, "786130dc-1000-48cf-8777-4c488adde371", "devgary@gmail.com", true, false, null, "devgary@gmail.com", "devgary@gmail.com", "AQAAAAEAACcQAAAAENLbY9vVr9VjWUh30rVoW7NwZ3jWsBfKo8CbzNqcbF3NleTHezekF2r+MK2XR63u4w==", null, false, "f1868e2a-de87-48dc-9cac-ff139acd7df8", false, "devgary@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { 1, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Admin", "a729e84c-d741-4c50-99d8-f2f4e4ae38d2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a729e84c-d741-4c50-99d8-f2f4e4ae38d2");
        }
    }
}
