using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ristysoft.CashFlow.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FundTransfers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FromFundId = table.Column<int>(type: "INTEGER", nullable: true),
                    ToFundId = table.Column<int>(type: "INTEGER", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundTransfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FundTransfers_Funds_FromFundId",
                        column: x => x.FromFundId,
                        principalTable: "Funds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FundTransfers_Funds_ToFundId",
                        column: x => x.ToFundId,
                        principalTable: "Funds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FundTransfers_FromFundId",
                table: "FundTransfers",
                column: "FromFundId");

            migrationBuilder.CreateIndex(
                name: "IX_FundTransfers_ToFundId",
                table: "FundTransfers",
                column: "ToFundId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FundTransfers");
        }
    }
}
