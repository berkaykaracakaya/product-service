using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Product.Domain.Product.ValueObjects;

namespace Product.Infrastructure.Persistence.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "category_id_seq");

            migrationBuilder.CreateSequence(
                name: "product_id_seq");

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    pid = table.Column<long>(type: "bigint", nullable: false),
                    title = table.Column<string>(type: "text", nullable: true),
                    min_stock_quantity = table.Column<double>(type: "double precision", nullable: false),
                    max_stock_quantity = table.Column<double>(type: "double precision", nullable: false),
                    status = table.Column<bool>(type: "boolean", nullable: false),
                    created_date = table.Column<long>(type: "bigint", nullable: false),
                    last_modified_date = table.Column<long>(type: "bigint", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EventName = table.Column<string>(type: "text", nullable: true),
                    AggregateId = table.Column<long>(type: "bigint", nullable: false),
                    Version = table.Column<long>(type: "bigint", nullable: false),
                    Event = table.Column<string>(type: "text", nullable: true),
                    Timestamp = table.Column<long>(type: "bigint", nullable: false),
                    IsSent = table.Column<bool>(type: "boolean", nullable: false),
                    SentDate = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    title = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    quantity = table.Column<double>(type: "double precision", nullable: false),
                    categories = table.Column<List<ProductCategory>>(type: "jsonb", nullable: true),
                    created_date = table.Column<long>(type: "bigint", nullable: false),
                    last_modified_date = table.Column<long>(type: "bigint", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_categories_pid",
                table: "categories",
                column: "pid");

            migrationBuilder.Sql("CREATE INDEX IX_product_category_id ON products ((\"categories\" -> 'Id'), (\"categories\" -> 'Title'))");
            
            migrationBuilder.Sql(@"INSERT INTO categories(id,pid,title,min_stock_quantity,max_stock_quantity,status,created_date,last_modified_date) 
            VALUES(1,0,'Test Ana Kategori',5,10,true,11111111,11111111),
            (2,1,'Test Alt 1-1 Kategori',5,10,true,11111111,11111111),
            (3,1,'Test Alt 1-2 Kategori',5,10,true,11111111,11111111),
            (4,2,'Test Alt 2-1 Kategori',5,10,true,11111111,11111111),
            (5,3,'Test Alt 2-2 Kategori',5,10,true,11111111,11111111),
            (6,0,'Test Ana 2 Kategori',5,10,true,11111111,11111111)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP INDEX IX_product_category_id");
            
            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "OutboxMessages");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropSequence(
                name: "category_id_seq");

            migrationBuilder.DropSequence(
                name: "product_id_seq");
        }
    }
}
