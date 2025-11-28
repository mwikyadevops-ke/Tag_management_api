using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TagManagement.Migrations
{
    /// <inheritdoc />
    public partial class RemoveWeightEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_WeightEvents_WeighEventId",
                table: "Tags");

            migrationBuilder.DropTable(
                name: "WeightEvents");

            migrationBuilder.DropIndex(
                name: "IX_Tags_WeighEventId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "WeighEventId",
                table: "Tags");

            migrationBuilder.AddColumn<string>(
                name: "CloseReason",
                table: "Tags",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloseReason",
                table: "Tags");

            migrationBuilder.AddColumn<int>(
                name: "WeighEventId",
                table: "Tags",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WeightEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AxleWeights = table.Column<decimal[]>(type: "numeric[]", nullable: false),
                    GrossWeight = table.Column<decimal>(type: "numeric", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    Processed = table.Column<bool>(type: "boolean", nullable: false),
                    Source = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    VehicleReg = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightEvents", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_WeighEventId",
                table: "Tags",
                column: "WeighEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_WeightEvents_WeighEventId",
                table: "Tags",
                column: "WeighEventId",
                principalTable: "WeightEvents",
                principalColumn: "Id");
        }
    }
}
