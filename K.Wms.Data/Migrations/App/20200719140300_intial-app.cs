using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace K.Wms.Data.Migrations.App
{
    public partial class intialapp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "corporations",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    logo_url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_corporations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "legal_form_types",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    country = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_legal_form_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "legal_form_type_names",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    legal_form_type_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_legal_form_type_names", x => x.id);
                    table.ForeignKey(
                        name: "fk_legal_form_type_names_legal_form_types_legal_form_type_id",
                        column: x => x.legal_form_type_id,
                        principalTable: "legal_form_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "legal_forms",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    city = table.Column<string>(nullable: true),
                    vat_id = table.Column<string>(nullable: true),
                    country_code = table.Column<string>(nullable: true),
                    legal_form_type_id = table.Column<int>(nullable: false),
                    corporation_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_legal_forms", x => x.id);
                    table.ForeignKey(
                        name: "fk_legal_forms_corporations_corporation_id",
                        column: x => x.corporation_id,
                        principalTable: "corporations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_legal_forms_legal_form_types_legal_form_type_id",
                        column: x => x.legal_form_type_id,
                        principalTable: "legal_form_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_legal_form_type_names_legal_form_type_id",
                table: "legal_form_type_names",
                column: "legal_form_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_legal_forms_corporation_id",
                table: "legal_forms",
                column: "corporation_id");

            migrationBuilder.CreateIndex(
                name: "ix_legal_forms_legal_form_type_id",
                table: "legal_forms",
                column: "legal_form_type_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "legal_form_type_names");

            migrationBuilder.DropTable(
                name: "legal_forms");

            migrationBuilder.DropTable(
                name: "corporations");

            migrationBuilder.DropTable(
                name: "legal_form_types");
        }
    }
}
