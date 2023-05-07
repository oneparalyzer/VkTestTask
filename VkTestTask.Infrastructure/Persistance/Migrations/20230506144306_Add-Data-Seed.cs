using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VkTestTask.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "user_groups",
                columns: new[] { "id", "Code", "Description" },
                values: new object[,]
                {
                    { new Guid("996f41e9-1c72-4320-900a-0ff55297094f"), "Admin", "Описание роли пользователя 'Admin'." },
                    { new Guid("b5795342-a7e7-418f-b500-dea8b6c7224d"), "User", "Описание роли пользователя 'User'." }
                });

            migrationBuilder.InsertData(
                table: "user_states",
                columns: new[] { "id", "Code", "Description" },
                values: new object[,]
                {
                    { new Guid("72c6bb19-9995-4433-8e25-344dfd82f0d4"), "Blocked", "Описание статуса пользователя 'Blocked'." },
                    { new Guid("af524889-170b-4cb2-94c0-6831a14c3d25"), "Active", "Описание статуса пользователя 'Active'." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "user_groups",
                keyColumn: "id",
                keyValue: new Guid("996f41e9-1c72-4320-900a-0ff55297094f"));

            migrationBuilder.DeleteData(
                table: "user_groups",
                keyColumn: "id",
                keyValue: new Guid("b5795342-a7e7-418f-b500-dea8b6c7224d"));

            migrationBuilder.DeleteData(
                table: "user_states",
                keyColumn: "id",
                keyValue: new Guid("72c6bb19-9995-4433-8e25-344dfd82f0d4"));

            migrationBuilder.DeleteData(
                table: "user_states",
                keyColumn: "id",
                keyValue: new Guid("af524889-170b-4cb2-94c0-6831a14c3d25"));
        }
    }
}
