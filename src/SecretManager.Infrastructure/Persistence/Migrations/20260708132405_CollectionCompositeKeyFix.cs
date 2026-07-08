using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecretManager.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CollectionCompositeKeyFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Secrets_Collections_VaultId_Id",
                table: "Secrets");

            migrationBuilder.DropIndex(
                name: "IX_Secrets_VaultId_Id",
                table: "Secrets");

            migrationBuilder.CreateIndex(
                name: "IX_Secrets_VaultId_CollectionId",
                table: "Secrets",
                columns: new[] { "VaultId", "CollectionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Secrets_Collections_VaultId_CollectionId",
                table: "Secrets",
                columns: new[] { "VaultId", "CollectionId" },
                principalTable: "Collections",
                principalColumns: new[] { "VaultId", "Id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Secrets_Collections_VaultId_CollectionId",
                table: "Secrets");

            migrationBuilder.DropIndex(
                name: "IX_Secrets_VaultId_CollectionId",
                table: "Secrets");

            migrationBuilder.CreateIndex(
                name: "IX_Secrets_VaultId_Id",
                table: "Secrets",
                columns: new[] { "VaultId", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_Secrets_Collections_VaultId_Id",
                table: "Secrets",
                columns: new[] { "VaultId", "Id" },
                principalTable: "Collections",
                principalColumns: new[] { "VaultId", "Id" });
        }
    }
}
