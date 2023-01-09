using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ECommerce.Domain.Migrations
{
    public partial class CreateProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authorization",
                columns: table => new
                {
                    Code = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(127)", maxLength: 127, nullable: false),
                    Descripton = table.Column<string>(type: "character varying(511)", maxLength: 511, nullable: false),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Deleted = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletionZamani = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authorization", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(127)", maxLength: 127, nullable: false),
                    Description = table.Column<string>(type: "character varying(511)", maxLength: 511, nullable: false),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Deleted = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletionZamani = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(63)", maxLength: 63, nullable: false),
                    Surname = table.Column<string>(type: "character varying(63)", maxLength: 63, nullable: false),
                    UserName = table.Column<string>(type: "character varying(127)", maxLength: 127, nullable: false),
                    Password = table.Column<string>(type: "character varying(63)", maxLength: 63, nullable: false),
                    PasswordAgain = table.Column<string>(type: "character varying(63)", maxLength: 63, nullable: false),
                    TuzlamaDegeri = table.Column<string>(type: "character varying(63)", maxLength: 63, nullable: false),
                    EMail = table.Column<string>(type: "character varying(127)", maxLength: 127, nullable: false),
                    Telephone = table.Column<string>(type: "character varying(31)", maxLength: 31, nullable: false),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Deleted = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletionZamani = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleAuthorization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    AuthorizationId = table.Column<int>(type: "integer", nullable: false),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Deleted = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletionZamani = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleAuthorization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleAuthorization_Authorization_AuthorizationId",
                        column: x => x.AuthorizationId,
                        principalTable: "Authorization",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleAuthorization_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Deleted = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletionZamani = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Authorization_Guid",
                table: "Authorization",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Authorization_Name_Deleted",
                table: "Authorization",
                columns: new[] { "Name", "Deleted" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_Guid",
                table: "Role",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_Name_Deleted",
                table: "Role",
                columns: new[] { "Name", "Deleted" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleAuthorization_AuthorizationId",
                table: "RoleAuthorization",
                column: "AuthorizationId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleAuthorization_Guid",
                table: "RoleAuthorization",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleAuthorization_RoleId_AuthorizationId_Deleted",
                table: "RoleAuthorization",
                columns: new[] { "RoleId", "AuthorizationId", "Deleted" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Guid",
                table: "User",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName_Deleted",
                table: "User",
                columns: new[] { "UserName", "Deleted" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_Guid",
                table: "UserRole",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId_RoleId_Deleted",
                table: "UserRole",
                columns: new[] { "UserId", "RoleId", "Deleted" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleAuthorization");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Authorization");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
