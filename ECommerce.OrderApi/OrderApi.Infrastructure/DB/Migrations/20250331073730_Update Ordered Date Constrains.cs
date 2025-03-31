using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderApi.Infrastructure.DB.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderedDateConstrains : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Order_OrderedDate",
                table: "Orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_Order_OrderedDate",
                table: "Orders",
                sql: "DATE(OrderedDate) = DATE('now', 'utc')");
        }
    }
}
