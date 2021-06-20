using Microsoft.EntityFrameworkCore.Migrations;

namespace NSE.Pedidos.Infra.Migrations
{
    public partial class Voucher_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoDesconto",
                table: "Vouchers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoDesconto",
                table: "Vouchers");
        }
    }
}
