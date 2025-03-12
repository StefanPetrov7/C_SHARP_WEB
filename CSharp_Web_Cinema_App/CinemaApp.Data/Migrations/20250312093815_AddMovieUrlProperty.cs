using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CinemaApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMovieUrlProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("2032bddd-add2-4c0b-baac-f729ac4e989b"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("2310c3d2-b705-4e7d-950b-1dcecab24e31"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("70b1abf1-cf24-4dfa-a0ed-0f0162420c1d"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("f10d4787-9291-4907-96ef-a621c502fe7e"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("960fc6ae-0a25-47cb-83d3-d22d22755dc1"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("dfc1f241-882e-4e0c-ba9c-6b42ed838f56"));

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Movies",
                type: "nvarchar(2083)",
                maxLength: 2083,
                nullable: true,
                defaultValue: "No_Image.jpeg");

            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { new Guid("3baa6fe6-bf5e-466d-8a92-3d1e9900ac2a"), "Sofia", "Cinema City" },
                    { new Guid("74b30951-b32c-47d2-b3ca-592463c577c1"), "Plovdiv", "Cinema City" },
                    { new Guid("8cc9806e-db4c-45cb-843d-10096228177e"), "Ruse", "Cinema City" },
                    { new Guid("fbce2760-9a42-4cfe-a295-0d8709932742"), "Varna", "Cinemax" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Director", "Duration", "Genre", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("3138d15d-e8a5-4cf5-a0aa-83f27ee7ae14"), "Fantasy movie about kids and magic", "Mike Newel", 157, "Fantasy", new DateTime(2005, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter" },
                    { new Guid("c3715519-36c5-43ee-a828-0b3a912cba5c"), "Fantasy movie about kids and magic", "Mike Newel", 147, "Fantasy", new DateTime(2006, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter 2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("3baa6fe6-bf5e-466d-8a92-3d1e9900ac2a"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("74b30951-b32c-47d2-b3ca-592463c577c1"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("8cc9806e-db4c-45cb-843d-10096228177e"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("fbce2760-9a42-4cfe-a295-0d8709932742"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("3138d15d-e8a5-4cf5-a0aa-83f27ee7ae14"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("c3715519-36c5-43ee-a828-0b3a912cba5c"));

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Movies");

            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { new Guid("2032bddd-add2-4c0b-baac-f729ac4e989b"), "Varna", "Cinemax" },
                    { new Guid("2310c3d2-b705-4e7d-950b-1dcecab24e31"), "Sofia", "Cinema City" },
                    { new Guid("70b1abf1-cf24-4dfa-a0ed-0f0162420c1d"), "Ruse", "Cinema City" },
                    { new Guid("f10d4787-9291-4907-96ef-a621c502fe7e"), "Plovdiv", "Cinema City" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Director", "Duration", "Genre", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("960fc6ae-0a25-47cb-83d3-d22d22755dc1"), "Fantasy movie about kids and magic", "Mike Newel", 147, "Fantasy", new DateTime(2006, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter 2" },
                    { new Guid("dfc1f241-882e-4e0c-ba9c-6b42ed838f56"), "Fantasy movie about kids and magic", "Mike Newel", 157, "Fantasy", new DateTime(2005, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter" }
                });
        }
    }
}
