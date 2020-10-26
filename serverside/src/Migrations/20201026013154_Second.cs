using System;
using Lactalis.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lactalis.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "NewsArticle");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:price_type", "amount,negotiable,free,swaptrade")
                .Annotation("Npgsql:Enum:state", "qld,nsw,vic,tas,wa,sa,nt")
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,")
                .OldAnnotation("Npgsql:Enum:state", "qld,nsw,vic,tas,wa,sa,nt")
                .OldAnnotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.AddColumn<Guid>(
                name: "FeatureImageId",
                table: "NewsArticle",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Headline",
                table: "NewsArticle",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PromotedArticlesId",
                table: "NewsArticle",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Farm",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AgriSupplyDocumentCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Owner = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Modified = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgriSupplyDocumentCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImportantDocumentCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Owner = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Modified = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportantDocumentCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PromotedArticles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Owner = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Modified = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    State = table.Column<State>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotedArticles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QualityDocumentCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Owner = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Modified = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualityDocumentCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SustainabilityPost",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Owner = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Modified = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Title = table.Column<string>(nullable: true),
                    ImageId = table.Column<Guid>(nullable: true),
                    FileId = table.Column<Guid>(nullable: true),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SustainabilityPost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SustainabilityPost___Files_FileId",
                        column: x => x.FileId,
                        principalTable: "__Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SustainabilityPost___Files_ImageId",
                        column: x => x.ImageId,
                        principalTable: "__Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalDocumentCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Owner = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Modified = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalDocumentCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TradingPostCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Owner = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Modified = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradingPostCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TradingPostListing",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Owner = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Modified = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Title = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    AdditionalInfo = table.Column<string>(nullable: true),
                    AddressLine1 = table.Column<string>(nullable: true),
                    AddressLine2 = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    ProductImageId = table.Column<Guid>(nullable: true),
                    Price = table.Column<int>(nullable: true),
                    PriceType = table.Column<PriceType>(nullable: false),
                    FarmerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradingPostListing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TradingPostListing_AspNetUsers_FarmerId",
                        column: x => x.FarmerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TradingPostListing___Files_ProductImageId",
                        column: x => x.ProductImageId,
                        principalTable: "__Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AgriSupplyDocument",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Owner = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Modified = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    FileId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    AgriSupplyDocumentCategoryId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgriSupplyDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgriSupplyDocument_AgriSupplyDocumentCategory_AgriSupplyDoc~",
                        column: x => x.AgriSupplyDocumentCategoryId,
                        principalTable: "AgriSupplyDocumentCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgriSupplyDocument___Files_FileId",
                        column: x => x.FileId,
                        principalTable: "__Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ImportantDocument",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Owner = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Modified = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    FileId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Qld = table.Column<bool>(nullable: true),
                    Nsw = table.Column<bool>(nullable: true),
                    Vic = table.Column<bool>(nullable: true),
                    Tas = table.Column<bool>(nullable: true),
                    Wa = table.Column<bool>(nullable: true),
                    Sa = table.Column<bool>(nullable: true),
                    Nt = table.Column<bool>(nullable: true),
                    DocumentCategoryId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportantDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportantDocument_ImportantDocumentCategory_DocumentCategor~",
                        column: x => x.DocumentCategoryId,
                        principalTable: "ImportantDocumentCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImportantDocument___Files_FileId",
                        column: x => x.FileId,
                        principalTable: "__Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "QualityDocument",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Owner = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Modified = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    FileId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    QualityDocumentCategoryId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualityDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QualityDocument___Files_FileId",
                        column: x => x.FileId,
                        principalTable: "__Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_QualityDocument_QualityDocumentCategory_QualityDocumentCate~",
                        column: x => x.QualityDocumentCategoryId,
                        principalTable: "QualityDocumentCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalDocument",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Owner = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Modified = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    FileId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    TechnicalDocumentCategoryId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicalDocument___Files_FileId",
                        column: x => x.FileId,
                        principalTable: "__Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_TechnicalDocument_TechnicalDocumentCategory_TechnicalDocume~",
                        column: x => x.TechnicalDocumentCategoryId,
                        principalTable: "TechnicalDocumentCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TradingPostListingsTradingPostCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Created = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Modified = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Owner = table.Column<Guid>(nullable: false),
                    TradingPostListingsId = table.Column<Guid>(nullable: false),
                    TradingPostCategoriesId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradingPostListingsTradingPostCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TradingPostListingsTradingPostCategories_TradingPostCategor~",
                        column: x => x.TradingPostCategoriesId,
                        principalTable: "TradingPostCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TradingPostListingsTradingPostCategories_TradingPostListing~",
                        column: x => x.TradingPostListingsId,
                        principalTable: "TradingPostListing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewsArticle_FeatureImageId",
                table: "NewsArticle",
                column: "FeatureImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NewsArticle_PromotedArticlesId",
                table: "NewsArticle",
                column: "PromotedArticlesId");

            migrationBuilder.CreateIndex(
                name: "IX_AgriSupplyDocument_AgriSupplyDocumentCategoryId",
                table: "AgriSupplyDocument",
                column: "AgriSupplyDocumentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AgriSupplyDocument_FileId",
                table: "AgriSupplyDocument",
                column: "FileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImportantDocument_DocumentCategoryId",
                table: "ImportantDocument",
                column: "DocumentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportantDocument_FileId",
                table: "ImportantDocument",
                column: "FileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QualityDocument_FileId",
                table: "QualityDocument",
                column: "FileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QualityDocument_QualityDocumentCategoryId",
                table: "QualityDocument",
                column: "QualityDocumentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SustainabilityPost_FileId",
                table: "SustainabilityPost",
                column: "FileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SustainabilityPost_ImageId",
                table: "SustainabilityPost",
                column: "ImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalDocument_FileId",
                table: "TechnicalDocument",
                column: "FileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalDocument_TechnicalDocumentCategoryId",
                table: "TechnicalDocument",
                column: "TechnicalDocumentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TradingPostListing_FarmerId",
                table: "TradingPostListing",
                column: "FarmerId");

            migrationBuilder.CreateIndex(
                name: "IX_TradingPostListing_ProductImageId",
                table: "TradingPostListing",
                column: "ProductImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TradingPostListingsTradingPostCategories_TradingPostCategor~",
                table: "TradingPostListingsTradingPostCategories",
                column: "TradingPostCategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_TradingPostListingsTradingPostCategories_TradingPostListing~",
                table: "TradingPostListingsTradingPostCategories",
                column: "TradingPostListingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsArticle___Files_FeatureImageId",
                table: "NewsArticle",
                column: "FeatureImageId",
                principalTable: "__Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsArticle_PromotedArticles_PromotedArticlesId",
                table: "NewsArticle",
                column: "PromotedArticlesId",
                principalTable: "PromotedArticles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsArticle___Files_FeatureImageId",
                table: "NewsArticle");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsArticle_PromotedArticles_PromotedArticlesId",
                table: "NewsArticle");

            migrationBuilder.DropTable(
                name: "AgriSupplyDocument");

            migrationBuilder.DropTable(
                name: "ImportantDocument");

            migrationBuilder.DropTable(
                name: "PromotedArticles");

            migrationBuilder.DropTable(
                name: "QualityDocument");

            migrationBuilder.DropTable(
                name: "SustainabilityPost");

            migrationBuilder.DropTable(
                name: "TechnicalDocument");

            migrationBuilder.DropTable(
                name: "TradingPostListingsTradingPostCategories");

            migrationBuilder.DropTable(
                name: "AgriSupplyDocumentCategory");

            migrationBuilder.DropTable(
                name: "ImportantDocumentCategory");

            migrationBuilder.DropTable(
                name: "QualityDocumentCategory");

            migrationBuilder.DropTable(
                name: "TechnicalDocumentCategory");

            migrationBuilder.DropTable(
                name: "TradingPostCategory");

            migrationBuilder.DropTable(
                name: "TradingPostListing");

            migrationBuilder.DropIndex(
                name: "IX_NewsArticle_FeatureImageId",
                table: "NewsArticle");

            migrationBuilder.DropIndex(
                name: "IX_NewsArticle_PromotedArticlesId",
                table: "NewsArticle");

            migrationBuilder.DropColumn(
                name: "FeatureImageId",
                table: "NewsArticle");

            migrationBuilder.DropColumn(
                name: "Headline",
                table: "NewsArticle");

            migrationBuilder.DropColumn(
                name: "PromotedArticlesId",
                table: "NewsArticle");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Farm");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:state", "qld,nsw,vic,tas,wa,sa,nt")
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,")
                .OldAnnotation("Npgsql:Enum:price_type", "amount,negotiable,free,swaptrade")
                .OldAnnotation("Npgsql:Enum:state", "qld,nsw,vic,tas,wa,sa,nt")
                .OldAnnotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "NewsArticle",
                type: "text",
                nullable: true);
        }
    }
}
