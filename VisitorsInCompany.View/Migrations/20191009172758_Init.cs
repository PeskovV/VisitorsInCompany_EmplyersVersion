using Microsoft.EntityFrameworkCore.Migrations;

namespace VisitorsInCompany.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Visitors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false).Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Patronymic = table.Column<string>(nullable: true),
                    Organization = table.Column<string>(nullable: false),
                    VisitGoal = table.Column<string>(nullable: false),
                    Attendant = table.Column<string>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    EntryTime = table.Column<string>(nullable: false),
                    ExitTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitors", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Visitors");
        }
    }
}
