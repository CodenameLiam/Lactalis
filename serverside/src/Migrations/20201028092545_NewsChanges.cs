using Lactalis.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lactalis.Migrations
{
    public partial class NewsChanges : Migration
    {
        ed override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "PromotedArticles");

            // migrationBuilder.AlterDatabase()
            //     .Annotation("Npgsql:Enum:price_type", "amount,negotiable,free,swaptrade")
            //     .Annotation("Npgsql:Enum:state", "qld,nsw,vic,wa,sa,tas,nt")
            //     .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,")
            //     .OldAnnotation("Npgsql:Enum:price_type", "amount,negotiable,free,swaptrade")
            //     .OldAnnotation("Npgsql:Enum:state", "qld,nsw,vic,tas,wa,sa,nt")
            //     .OldAnnotation("Npgsql:PostgresExtension:uuid-ossp", ",,");



            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PromotedArticles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "NewsArticle",
                nullable: true);
        }

        ed override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "PromotedArticles");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "NewsArticle");

            // migrationBuilder.AlterDatabase()
            //     .Annotation("Npgsql:Enum:price_type", "amount,negotiable,free,swaptrade")
            //     .Annotation("Npgsql:Enum:state", "qld,nsw,vic,tas,wa,sa,nt")
            //     .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,")
            //     .OldAnnotation("Npgsql:Enum:price_type", "amount,negotiable,free,swaptrade")
            //     .OldAnnotation("Npgsql:Enum:state", "qld,nsw,vic,wa,sa,tas,nt")
            //     .OldAnnotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.AddColumn<State>(
                name: "State",
                table: "PromotedArticles",
                type: "state",
                nullable: false,
                defaultValue: State.QLD);
        }
    }
}
