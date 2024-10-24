using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookstoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCustomerIdToString2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Convert old string values to integer values (corresponding to the enum)
            migrationBuilder.Sql(
                "UPDATE Orders SET Status = 0 WHERE Status = 'Pending'; " +
                "UPDATE Orders SET Status = 1 WHERE Status = 'Shipped'; " +
                "UPDATE Orders SET Status = 2 WHERE Status = 'Delivered'; " +
                "UPDATE Orders SET Status = 3 WHERE Status = 'Canceled';");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Reverse the conversion from integers back to strings (if you ever need to rollback)
            migrationBuilder.Sql(
                "UPDATE Orders SET Status = 'Pending' WHERE Status = 0; " +
                "UPDATE Orders SET Status = 'Shipped' WHERE Status = 1; " +
                "UPDATE Orders SET Status = 'Delivered' WHERE Status = 2; " +
                "UPDATE Orders SET Status = 'Canceled' WHERE Status = 3;");
            
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
