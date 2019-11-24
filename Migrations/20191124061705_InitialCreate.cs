using Microsoft.EntityFrameworkCore.Migrations;

namespace Food_Enforcement.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Openfda",
                columns: table => new
                {
                    Openfda_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Openfda", x => x.Openfda_ID);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Results_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    skip = table.Column<int>(nullable: false),
                    limit = table.Column<int>(nullable: false),
                    total = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Results_ID);
                });

            migrationBuilder.CreateTable(
                name: "Meta",
                columns: table => new
                {
                    Meta_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    disclaimer = table.Column<string>(nullable: true),
                    terms = table.Column<string>(nullable: true),
                    license = table.Column<string>(nullable: true),
                    last_updated = table.Column<string>(nullable: true),
                    Results_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meta", x => x.Meta_ID);
                    table.ForeignKey(
                        name: "FK_Meta_Results_Results_ID",
                        column: x => x.Results_ID,
                        principalTable: "Results",
                        principalColumn: "Results_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RootObject",
                columns: table => new
                {
                    RootObject_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Meta_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RootObject", x => x.RootObject_ID);
                    table.ForeignKey(
                        name: "FK_RootObject_Meta_Meta_ID",
                        column: x => x.Meta_ID,
                        principalTable: "Meta",
                        principalColumn: "Meta_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Result",
                columns: table => new
                {
                    Result_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    country = table.Column<string>(nullable: true),
                    city = table.Column<string>(nullable: true),
                    reason_for_recall = table.Column<string>(nullable: true),
                    address_1 = table.Column<string>(nullable: true),
                    address_2 = table.Column<string>(nullable: true),
                    code_info = table.Column<string>(nullable: true),
                    product_quantity = table.Column<string>(nullable: true),
                    center_classification_date = table.Column<string>(nullable: true),
                    distribution_pattern = table.Column<string>(nullable: true),
                    state = table.Column<string>(nullable: true),
                    product_description = table.Column<string>(nullable: true),
                    report_date = table.Column<string>(nullable: true),
                    classification = table.Column<string>(nullable: true),
                    Openfda_ID = table.Column<int>(nullable: true),
                    recall_number = table.Column<string>(nullable: true),
                    recalling_firm = table.Column<string>(nullable: true),
                    initial_firm_notification = table.Column<string>(nullable: true),
                    event_id = table.Column<string>(nullable: true),
                    product_type = table.Column<string>(nullable: true),
                    termination_date = table.Column<string>(nullable: true),
                    more_code_info = table.Column<string>(nullable: true),
                    recall_initiation_date = table.Column<string>(nullable: true),
                    postal_code = table.Column<string>(nullable: true),
                    voluntary_mandated = table.Column<string>(nullable: true),
                    status = table.Column<string>(nullable: true),
                    RootObject_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Result", x => x.Result_ID);
                    table.ForeignKey(
                        name: "FK_Result_Openfda_Openfda_ID",
                        column: x => x.Openfda_ID,
                        principalTable: "Openfda",
                        principalColumn: "Openfda_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Result_RootObject_RootObject_ID",
                        column: x => x.RootObject_ID,
                        principalTable: "RootObject",
                        principalColumn: "RootObject_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meta_Results_ID",
                table: "Meta",
                column: "Results_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Result_Openfda_ID",
                table: "Result",
                column: "Openfda_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Result_RootObject_ID",
                table: "Result",
                column: "RootObject_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RootObject_Meta_ID",
                table: "RootObject",
                column: "Meta_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Result");

            migrationBuilder.DropTable(
                name: "Openfda");

            migrationBuilder.DropTable(
                name: "RootObject");

            migrationBuilder.DropTable(
                name: "Meta");

            migrationBuilder.DropTable(
                name: "Results");
        }
    }
}
