using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IK.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Mg1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivationCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserProfiles_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Departmants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmantName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departmants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departmants_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PositionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RequiredEducation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiredExperience = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxSalary = table.Column<decimal>(type: "money", nullable: false),
                    MinSalary = table.Column<decimal>(type: "money", nullable: false),
                    DepartmantId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positions_Departmants_DepartmantId",
                        column: x => x.DepartmantId,
                        principalTable: "Departmants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TCKN = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    MaritalStatus = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Salary = table.Column<decimal>(type: "money", nullable: false),
                    JobType = table.Column<int>(type: "int", nullable: false),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    DepartmanId = table.Column<int>(type: "int", nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Departmants_DepartmanId",
                        column: x => x.DepartmanId,
                        principalTable: "Departmants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationStatus = table.Column<int>(type: "int", nullable: false),
                    CVFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobApplications_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CareerPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlannedPromotionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    CurrentPositionId = table.Column<int>(type: "int", nullable: false),
                    TargetPositionId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareerPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CareerPlans_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CareerPlans_Positions_CurrentPositionId",
                        column: x => x.CurrentPositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CareerPlans_Positions_TargetPositionId",
                        column: x => x.TargetPositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeQualifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Education = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EducationLevel = table.Column<int>(type: "int", nullable: false),
                    Experience = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Skills = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Languages = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Certifications = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeQualifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeQualifications_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payrolls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Period = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrossSalary = table.Column<decimal>(type: "money", nullable: false),
                    NetSalary = table.Column<decimal>(type: "money", nullable: false),
                    Bonuses = table.Column<decimal>(type: "money", nullable: false),
                    Deductions = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payrolls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payrolls_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExitTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalHours = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkHours_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "f0b61602-ec6c-4df9-a251-a3f32bd8eeb7", "Admin", "ADMIN" },
                    { 2, "72b2d2cc-0f36-4e03-b37a-c3ceff48d61f", "Employee", "EMPLOYEE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ActivationCode", "ConcurrencyStamp", "CreatedDate", "DeletedDate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UpdatedDate", "UserName" },
                values: new object[,]
                {
                    { 1, 0, new Guid("00000000-0000-0000-0000-000000000000"), "6524bf12-a4cc-4577-a9a4-a99ca1678274", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ozan@ik.com", true, false, null, "OZAN@IK.COM", "OZAN", "AQAAAAIAAYagAAAAEOImeeqml9WtW5LIHXF3eXVUBmskBDoXgJ6OtGV3TiIyJRBZGVTi2U7r4rygpunCpw==", null, false, "00f06f58-df77-42cc-be52-8679f9b860ee", 0, false, null, "ozan" },
                    { 2, 0, new Guid("00000000-0000-0000-0000-000000000000"), "2b306052-2f5f-481d-b7e2-51f7c2c02b94", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ahmet@ik.com", true, false, null, "AHMET@IK.COM", "AHMET", "AQAAAAIAAYagAAAAEBU19gpAFaIY3yRZATW6SlsyNS4Tw6eIZD1LHFIBYuUsuAqJISVRuXXEk1/Mj6/YEQ==", null, false, "1a13bf43-cb26-45df-9a0a-78cf1f5e1e16", 0, false, null, "ahmet" }
                });

            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "Id", "Address", "BranchName", "City", "CreatedDate", "DeletedDate", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "İstanbul Kadıköy", "Merkez Şube", "İstanbul", new DateTime(2025, 12, 1, 17, 56, 7, 31, DateTimeKind.Local).AddTicks(5582), null, 1, null },
                    { 2, "Ankara Etimesgut", "Ankara Şube", "Ankara", new DateTime(2025, 12, 1, 17, 56, 7, 31, DateTimeKind.Local).AddTicks(5595), null, 1, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Departmants",
                columns: new[] { "Id", "BranchId", "CreatedDate", "DeletedDate", "DepartmantName", "Description", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 12, 1, 17, 56, 7, 31, DateTimeKind.Local).AddTicks(5713), null, "İK", "İnsan Kaynakları", 1, null },
                    { 2, 2, new DateTime(2025, 12, 1, 17, 56, 7, 31, DateTimeKind.Local).AddTicks(5715), null, "Yazılım", "Yazılım Geliştirme", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "DepartmantId", "MaxSalary", "MinSalary", "PositionName", "RequiredEducation", "RequiredExperience", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 12, 1, 17, 56, 7, 31, DateTimeKind.Local).AddTicks(6071), null, 1, 12000m, 8000m, "İK Uzmanı", "Üniversite", "2 yıl", 1, null },
                    { 2, new DateTime(2025, 12, 1, 17, 56, 7, 31, DateTimeKind.Local).AddTicks(6074), null, 2, 15000m, 9000m, "Yazılım Geliştirici", "Üniversite", "3 yıl", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "AppUserId", "BirthDate", "BranchId", "CreatedDate", "DeletedDate", "DepartmanId", "Email", "EndDate", "FirstName", "Gender", "JobType", "LastName", "MaritalStatus", "PhoneNumber", "PositionId", "Salary", "StartDate", "Status", "TCKN", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "İstanbul", 1, new DateTime(2002, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2025, 12, 1, 17, 56, 7, 31, DateTimeKind.Local).AddTicks(5827), null, 1, "ozan@ik.com", null, "Ozan", 1, 1, "Erel", 1, "05000000001", 1, 10000m, new DateTime(2023, 12, 1, 17, 56, 7, 31, DateTimeKind.Local).AddTicks(5832), 1, "12345678901", null },
                    { 2, "Ankara", 2, new DateTime(1992, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 12, 1, 17, 56, 7, 31, DateTimeKind.Local).AddTicks(5837), null, 2, "ahmet@ik.com", null, "Ahmet", 1, 1, "Baykara", 2, "05000000002", 2, 9000m, new DateTime(2024, 12, 1, 17, 56, 7, 31, DateTimeKind.Local).AddTicks(5839), 1, "12345678902", null }
                });

            migrationBuilder.InsertData(
                table: "JobApplications",
                columns: new[] { "Id", "ApplicantName", "ApplicateDate", "ApplicationStatus", "CVFilePath", "CreatedDate", "DeletedDate", "Email", "PhoneNumber", "PositionId", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "Mehmet Demir", new DateTime(2025, 11, 21, 17, 56, 7, 31, DateTimeKind.Local).AddTicks(5899), 1, "CVs/MehmetDemir.pdf", new DateTime(2025, 12, 1, 17, 56, 7, 31, DateTimeKind.Local).AddTicks(5897), null, "mehmet@example.com", "05001112233", 2, 1, null },
                    { 2, "Ayşe Yılmaz", new DateTime(2025, 11, 26, 17, 56, 7, 31, DateTimeKind.Local).AddTicks(5906), 2, "CVs/AyseYilmaz.pdf", new DateTime(2025, 12, 1, 17, 56, 7, 31, DateTimeKind.Local).AddTicks(5904), null, "ayse@example.com", "05004445566", 1, 1, null }
                });

            migrationBuilder.InsertData(
                table: "CareerPlans",
                columns: new[] { "Id", "CreatedDate", "CurrentPositionId", "DeletedDate", "EmployeeId", "Notes", "PlannedPromotionDate", "Status", "TargetPositionId", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 12, 1, 17, 56, 7, 31, DateTimeKind.Local).AddTicks(5665), 1, null, 1, "Başarılı performans", new DateTime(2026, 6, 1, 17, 56, 7, 31, DateTimeKind.Local).AddTicks(5666), 1, 1, null },
                    { 2, new DateTime(2025, 12, 1, 17, 56, 7, 31, DateTimeKind.Local).AddTicks(5674), 2, null, 2, "Tecrübeyi artıracak", new DateTime(2026, 12, 1, 17, 56, 7, 31, DateTimeKind.Local).AddTicks(5675), 1, 2, null }
                });

            migrationBuilder.InsertData(
                table: "EmployeeQualifications",
                columns: new[] { "Id", "Certifications", "CreatedDate", "DeletedDate", "Education", "EducationLevel", "EmployeeId", "Experience", "Languages", "Skills", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "İK Sertifikası", new DateTime(2025, 12, 1, 17, 56, 7, 31, DateTimeKind.Local).AddTicks(5769), null, "Üniversite", 3, 1, "2 yıl", "İngilizce", "MS Office", 1, null },
                    { 2, "MCP", new DateTime(2025, 12, 1, 17, 56, 7, 31, DateTimeKind.Local).AddTicks(5772), null, "Üniversite", 3, 2, "3 yıl", "İngilizce", "C#, ASP.NET", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Payrolls",
                columns: new[] { "Id", "Bonuses", "CreatedDate", "Deductions", "DeletedDate", "EmployeeId", "GrossSalary", "NetSalary", "Period", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 500m, new DateTime(2025, 12, 1, 17, 56, 7, 31, DateTimeKind.Local).AddTicks(6021), 500m, null, 1, 10000m, 9000m, "2025-11", 1, null },
                    { 2, 300m, new DateTime(2025, 12, 1, 17, 56, 7, 31, DateTimeKind.Local).AddTicks(6024), 400m, null, 2, 9000m, 8200m, "2025-11", 1, null }
                });

            migrationBuilder.InsertData(
                table: "WorkHours",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "EmployeeId", "EntryTime", "ExitTime", "Status", "TotalHours", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 12, 1, 17, 56, 7, 31, DateTimeKind.Local).AddTicks(6109), null, 1, new DateTime(2025, 12, 1, 9, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 12, 1, 18, 0, 0, 0, DateTimeKind.Local), 1, 9m, null },
                    { 2, new DateTime(2025, 12, 1, 17, 56, 7, 31, DateTimeKind.Local).AddTicks(6119), null, 2, new DateTime(2025, 12, 1, 9, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 12, 1, 18, 0, 0, 0, DateTimeKind.Local), 1, 9m, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserProfiles_AppUserId",
                table: "AppUserProfiles",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CareerPlans_CurrentPositionId",
                table: "CareerPlans",
                column: "CurrentPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_CareerPlans_EmployeeId",
                table: "CareerPlans",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_CareerPlans_TargetPositionId",
                table: "CareerPlans",
                column: "TargetPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Departmants_BranchId",
                table: "Departmants",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeQualifications_EmployeeId",
                table: "EmployeeQualifications",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AppUserId",
                table: "Employees",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BranchId",
                table: "Employees",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmanId",
                table: "Employees",
                column: "DepartmanId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PositionId",
                table: "Employees",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_PositionId",
                table: "JobApplications",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Payrolls_EmployeeId",
                table: "Payrolls",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_DepartmantId",
                table: "Positions",
                column: "DepartmantId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkHours_EmployeeId",
                table: "WorkHours",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserProfiles");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CareerPlans");

            migrationBuilder.DropTable(
                name: "EmployeeQualifications");

            migrationBuilder.DropTable(
                name: "JobApplications");

            migrationBuilder.DropTable(
                name: "Payrolls");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "WorkHours");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Departmants");

            migrationBuilder.DropTable(
                name: "Branches");
        }
    }
}
