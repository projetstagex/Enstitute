using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Enstitute.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StreetAdress = table.Column<string>(maxLength: 100, nullable: false),
                    AppartementAdress = table.Column<string>(maxLength: 100, nullable: true),
                    ZipCode = table.Column<string>(nullable: false),
                    City = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Day",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Label = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Day", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Departement",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Label = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departement", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Label = table.Column<string>(nullable: true),
                    Code = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Grade",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Label = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grade", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Module",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(nullable: false),
                    Title = table.Column<string>(maxLength: 30, nullable: false),
                    Multiplier = table.Column<int>(nullable: false),
                    Approved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ScholarYear",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(nullable: false),
                    From = table.Column<int>(nullable: false),
                    To = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScholarYear", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Formation",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TotalYears = table.Column<int>(nullable: false),
                    Label = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    DepartementID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Formation_Departement_DepartementID",
                        column: x => x.DepartementID,
                        principalTable: "Departement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Precision",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(nullable: false),
                    ModuleID = table.Column<int>(nullable: false),
                    Label = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Precision", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Precision_Module_ModuleID",
                        column: x => x.ModuleID,
                        principalTable: "Module",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sector",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Label = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    GradeID = table.Column<int>(nullable: false),
                    FormationID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sector", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Sector_Formation_FormationID",
                        column: x => x.FormationID,
                        principalTable: "Formation",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sector_Grade_GradeID",
                        column: x => x.GradeID,
                        principalTable: "Grade",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Context",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(nullable: false),
                    Label = table.Column<string>(maxLength: 15, nullable: false),
                    PrecisionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Context", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Context_Precision_PrecisionID",
                        column: x => x.PrecisionID,
                        principalTable: "Precision",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Label = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    SectorID = table.Column<int>(nullable: false),
                    ScholarYearID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Group_ScholarYear_ScholarYearID",
                        column: x => x.ScholarYearID,
                        principalTable: "ScholarYear",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Group_Sector_SectorID",
                        column: x => x.SectorID,
                        principalTable: "Sector",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupEdit",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GroupID = table.Column<int>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Label = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupEdit", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GroupEdit_Group_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Group",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    NIC = table.Column<string>(maxLength: 15, nullable: false),
                    Username = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    is_staff = table.Column<bool>(nullable: false),
                    is_director = table.Column<bool>(nullable: false),
                    is_former = table.Column<bool>(nullable: false),
                    is_secretary = table.Column<bool>(nullable: false),
                    is_actif = table.Column<bool>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    AddressID = table.Column<int>(nullable: false),
                    GenderID = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    RegistrationNumber = table.Column<string>(maxLength: 15, nullable: true),
                    HireDate = table.Column<DateTime>(nullable: true),
                    Former_RegistrationNumber = table.Column<string>(maxLength: 30, nullable: true),
                    Former_HireDate = table.Column<DateTime>(nullable: true),
                    Secretary_RegistrationNumber = table.Column<string>(maxLength: 30, nullable: true),
                    Secretary_HireDate = table.Column<DateTime>(nullable: true),
                    InscriptionNumber = table.Column<string>(maxLength: 30, nullable: true),
                    Nationality = table.Column<string>(maxLength: 50, nullable: true),
                    Is_training = table.Column<bool>(nullable: true),
                    GroupID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                    table.ForeignKey(
                        name: "FK_User_Group_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Group",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Address_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Address",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Gender_GenderID",
                        column: x => x.GenderID,
                        principalTable: "Gender",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActionGroup",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    SecretaryID = table.Column<string>(nullable: false),
                    GroupEditedID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionGroup", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ActionGroup_GroupEdit_GroupEditedID",
                        column: x => x.GroupEditedID,
                        principalTable: "GroupEdit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActionGroup_User_SecretaryID",
                        column: x => x.SecretaryID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModuleAffectation",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GroupID = table.Column<int>(nullable: false),
                    ModuleID = table.Column<int>(nullable: false),
                    FormerID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleAffectation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ModuleAffectation_User_FormerID",
                        column: x => x.FormerID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModuleAffectation_Group_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Group",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModuleAffectation_Module_ModuleID",
                        column: x => x.ModuleID,
                        principalTable: "Module",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FormerID = table.Column<string>(nullable: true),
                    ScholarYearID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Schedule_User_FormerID",
                        column: x => x.FormerID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedule_ScholarYear_ScholarYearID",
                        column: x => x.ScholarYearID,
                        principalTable: "ScholarYear",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentEdit",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    studentID = table.Column<string>(nullable: true),
                    nationality = table.Column<string>(maxLength: 50, nullable: true),
                    firstName = table.Column<string>(nullable: true),
                    lastName = table.Column<string>(nullable: true),
                    groupID = table.Column<int>(nullable: false),
                    email = table.Column<string>(nullable: true),
                    phone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentEdit", x => x.id);
                    table.ForeignKey(
                        name: "FK_StudentEdit_Group_groupID",
                        column: x => x.groupID,
                        principalTable: "Group",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentEdit_User_studentID",
                        column: x => x.studentID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserSession",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 15, nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    LoginDate = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSession", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserSession_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    is_catching = table.Column<bool>(nullable: false),
                    FromHour = table.Column<int>(nullable: false),
                    FromMinute = table.Column<int>(nullable: false),
                    ToHour = table.Column<int>(nullable: false),
                    ToMinute = table.Column<int>(nullable: false),
                    TimePart = table.Column<int>(nullable: false),
                    ScheduleID = table.Column<int>(nullable: false),
                    DayID = table.Column<int>(nullable: false),
                    GroupID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Session_Day_DayID",
                        column: x => x.DayID,
                        principalTable: "Day",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Session_Group_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Group",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Session_Schedule_ScheduleID",
                        column: x => x.ScheduleID,
                        principalTable: "Schedule",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActionStudent",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    SecretaryID = table.Column<string>(nullable: false),
                    StudentEditID = table.Column<int>(nullable: false),
                    StudentEditedid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionStudent", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ActionStudent_User_SecretaryID",
                        column: x => x.SecretaryID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActionStudent_StudentEdit_StudentEditedid",
                        column: x => x.StudentEditedid,
                        principalTable: "StudentEdit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Absence",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SessionID = table.Column<int>(nullable: false),
                    StudentID = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Absence", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Absence_Session_SessionID",
                        column: x => x.SessionID,
                        principalTable: "Session",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Absence_User_StudentID",
                        column: x => x.StudentID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Delay",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Duration = table.Column<int>(nullable: false),
                    StudentID = table.Column<string>(nullable: true),
                    SessionID = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delay", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Delay_Session_SessionID",
                        column: x => x.SessionID,
                        principalTable: "Session",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Delay_User_StudentID",
                        column: x => x.StudentID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SessionContext",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ContextID = table.Column<int>(nullable: false),
                    SessionID = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionContext", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SessionContext_Context_ContextID",
                        column: x => x.ContextID,
                        principalTable: "Context",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionContext_Session_SessionID",
                        column: x => x.SessionID,
                        principalTable: "Session",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Absence_SessionID",
                table: "Absence",
                column: "SessionID");

            migrationBuilder.CreateIndex(
                name: "IX_Absence_StudentID",
                table: "Absence",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_ActionGroup_GroupEditedID",
                table: "ActionGroup",
                column: "GroupEditedID");

            migrationBuilder.CreateIndex(
                name: "IX_ActionGroup_SecretaryID",
                table: "ActionGroup",
                column: "SecretaryID");

            migrationBuilder.CreateIndex(
                name: "IX_ActionStudent_SecretaryID",
                table: "ActionStudent",
                column: "SecretaryID");

            migrationBuilder.CreateIndex(
                name: "IX_ActionStudent_StudentEditedid",
                table: "ActionStudent",
                column: "StudentEditedid");

            migrationBuilder.CreateIndex(
                name: "IX_Context_PrecisionID",
                table: "Context",
                column: "PrecisionID");

            migrationBuilder.CreateIndex(
                name: "IX_Delay_SessionID",
                table: "Delay",
                column: "SessionID");

            migrationBuilder.CreateIndex(
                name: "IX_Delay_StudentID",
                table: "Delay",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Formation_DepartementID",
                table: "Formation",
                column: "DepartementID");

            migrationBuilder.CreateIndex(
                name: "IX_Group_ScholarYearID",
                table: "Group",
                column: "ScholarYearID");

            migrationBuilder.CreateIndex(
                name: "IX_Group_SectorID",
                table: "Group",
                column: "SectorID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupEdit_GroupID",
                table: "GroupEdit",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleAffectation_FormerID",
                table: "ModuleAffectation",
                column: "FormerID");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleAffectation_GroupID",
                table: "ModuleAffectation",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleAffectation_ModuleID",
                table: "ModuleAffectation",
                column: "ModuleID");

            migrationBuilder.CreateIndex(
                name: "IX_Precision_ModuleID",
                table: "Precision",
                column: "ModuleID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_FormerID",
                table: "Schedule",
                column: "FormerID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_ScholarYearID",
                table: "Schedule",
                column: "ScholarYearID");

            migrationBuilder.CreateIndex(
                name: "IX_Sector_FormationID",
                table: "Sector",
                column: "FormationID");

            migrationBuilder.CreateIndex(
                name: "IX_Sector_GradeID",
                table: "Sector",
                column: "GradeID");

            migrationBuilder.CreateIndex(
                name: "IX_Session_DayID",
                table: "Session",
                column: "DayID");

            migrationBuilder.CreateIndex(
                name: "IX_Session_GroupID",
                table: "Session",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Session_ScheduleID",
                table: "Session",
                column: "ScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_SessionContext_ContextID",
                table: "SessionContext",
                column: "ContextID");

            migrationBuilder.CreateIndex(
                name: "IX_SessionContext_SessionID",
                table: "SessionContext",
                column: "SessionID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEdit_groupID",
                table: "StudentEdit",
                column: "groupID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEdit_studentID",
                table: "StudentEdit",
                column: "studentID");

            migrationBuilder.CreateIndex(
                name: "IX_User_GroupID",
                table: "User",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_User_AddressID",
                table: "User",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_User_GenderID",
                table: "User",
                column: "GenderID");

            migrationBuilder.CreateIndex(
                name: "IX_UserSession_UserID",
                table: "UserSession",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Absence");

            migrationBuilder.DropTable(
                name: "ActionGroup");

            migrationBuilder.DropTable(
                name: "ActionStudent");

            migrationBuilder.DropTable(
                name: "Delay");

            migrationBuilder.DropTable(
                name: "ModuleAffectation");

            migrationBuilder.DropTable(
                name: "SessionContext");

            migrationBuilder.DropTable(
                name: "UserSession");

            migrationBuilder.DropTable(
                name: "GroupEdit");

            migrationBuilder.DropTable(
                name: "StudentEdit");

            migrationBuilder.DropTable(
                name: "Context");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "Precision");

            migrationBuilder.DropTable(
                name: "Day");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "Module");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "ScholarYear");

            migrationBuilder.DropTable(
                name: "Sector");

            migrationBuilder.DropTable(
                name: "Formation");

            migrationBuilder.DropTable(
                name: "Grade");

            migrationBuilder.DropTable(
                name: "Departement");
        }
    }
}
