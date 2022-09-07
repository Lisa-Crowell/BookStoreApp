using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreApp.API.Migrations
{
    public partial class SeededDefaulUsersAndRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8344074e-8623-4e1a-b0c1-84fb8678x8fc", "46bdda96-d4c8-415f-adcf-7791b7da7b7b", "User", "USER" },
                    { "8344074e-8623-4e1a-b0c1-84fb8678x8gd", "905dcc59-974c-45ff-b249-a543cb5b7c10", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "8344074e-8623-4e1a-b0c1-84fb8678x8he", 0, "773a8732-8966-442a-8fe5-7989c67cfdb6", "admin@bookstore.com", false, "System", "Admin", false, null, "ADMIN@BOOKSTORE.COM", "ADMIN", "AQAAAAEAACcQAAAAEKzix3piQpXZWoB5OurM9/4iJt3eyQ23bevvtpUUIiDXq0kh2GM8Ri1R6HAofdUsiw==", null, false, "01f53cb8-0a37-434c-8e04-58e42177ad7f", false, "admin" },
                    { "8344074e-8623-4e1a-b0c1-84fb8678x8if", 0, "108cf5d2-c8da-43b2-a90b-400a177fca4a", "user@bookstore.com", false, "System", "User", false, null, "USER@BOOKSTORE.COM", "USER", "AQAAAAEAACcQAAAAEKquysY+zJ9oGOMG1VYD5/PdzsloW/MKIirJRSd41qJhLgbAiiIHaO1GR0CXRiQ88Q==", null, false, "92183256-3efd-4f9c-887a-d4fce8728761", false, "user" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "8344074e-8623-4e1a-b0c1-84fb8678x8fc", "8344074e-8623-4e1a-b0c1-84fb8678x8he" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "8344074e-8623-4e1a-b0c1-84fb8678x8gd", "8344074e-8623-4e1a-b0c1-84fb8678x8if" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8344074e-8623-4e1a-b0c1-84fb8678x8fc", "8344074e-8623-4e1a-b0c1-84fb8678x8he" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8344074e-8623-4e1a-b0c1-84fb8678x8gd", "8344074e-8623-4e1a-b0c1-84fb8678x8if" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8344074e-8623-4e1a-b0c1-84fb8678x8fc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8344074e-8623-4e1a-b0c1-84fb8678x8gd");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8344074e-8623-4e1a-b0c1-84fb8678x8he");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8344074e-8623-4e1a-b0c1-84fb8678x8if");
        }
    }
}
