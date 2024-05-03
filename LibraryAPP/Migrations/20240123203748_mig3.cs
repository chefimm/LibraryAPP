using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAPP.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsbnNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KitapAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YazarAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SayfaSayisi = table.Column<int>(type: "int", nullable: true),
                    YayinlanmaYili = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KutuphanedeMi = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookReport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlanKisiAdSoyad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlanKisiTcNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlanKisiTelNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlimTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TeslimTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ToplamAlimSuresi = table.Column<double>(type: "float", nullable: true),
                    BookId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookReport_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookReport_BookId",
                table: "BookReport",
                column: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookReport");

            migrationBuilder.DropTable(
                name: "Book");
        }
    }
}
