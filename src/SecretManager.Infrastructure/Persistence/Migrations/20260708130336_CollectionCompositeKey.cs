using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecretManager.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CollectionCompositeKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Member_Organizations_OrganizationId",
                table: "Member");

            migrationBuilder.DropForeignKey(
                name: "FK_Member_Users_UserId",
                table: "Member");

            migrationBuilder.DropForeignKey(
                name: "FK_Secrets_Collections_CollectionId",
                table: "Secrets");

            migrationBuilder.DropIndex(
                name: "IX_Secrets_CollectionId",
                table: "Secrets");

            migrationBuilder.DropIndex(
                name: "IX_Secrets_VaultId",
                table: "Secrets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collections",
                table: "Collections");

            migrationBuilder.DropIndex(
                name: "IX_Collections_VaultId",
                table: "Collections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Member",
                table: "Member");

            migrationBuilder.RenameTable(
                name: "Member",
                newName: "Members");

            migrationBuilder.RenameIndex(
                name: "IX_Member_UserId",
                table: "Members",
                newName: "IX_Members_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Member_OrganizationId_UserId",
                table: "Members",
                newName: "IX_Members_OrganizationId_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collections",
                table: "Collections",
                columns: new[] { "VaultId", "Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Members",
                table: "Members",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Secrets_VaultId_Id",
                table: "Secrets",
                columns: new[] { "VaultId", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Organizations_OrganizationId",
                table: "Members",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Users_UserId",
                table: "Members",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Secrets_Collections_VaultId_Id",
                table: "Secrets",
                columns: new[] { "VaultId", "Id" },
                principalTable: "Collections",
                principalColumns: new[] { "VaultId", "Id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Organizations_OrganizationId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_Users_UserId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Secrets_Collections_VaultId_Id",
                table: "Secrets");

            migrationBuilder.DropIndex(
                name: "IX_Secrets_VaultId_Id",
                table: "Secrets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collections",
                table: "Collections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Members",
                table: "Members");

            migrationBuilder.RenameTable(
                name: "Members",
                newName: "Member");

            migrationBuilder.RenameIndex(
                name: "IX_Members_UserId",
                table: "Member",
                newName: "IX_Member_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Members_OrganizationId_UserId",
                table: "Member",
                newName: "IX_Member_OrganizationId_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collections",
                table: "Collections",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Member",
                table: "Member",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Secrets_CollectionId",
                table: "Secrets",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Secrets_VaultId",
                table: "Secrets",
                column: "VaultId");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_VaultId",
                table: "Collections",
                column: "VaultId");

            migrationBuilder.AddForeignKey(
                name: "FK_Member_Organizations_OrganizationId",
                table: "Member",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Member_Users_UserId",
                table: "Member",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Secrets_Collections_CollectionId",
                table: "Secrets",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id");
        }
    }
}
