using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_DatabaseTriggers_onChampionRestrictions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE Champions ADD HasRestrictions BIT DEFAULT 0;");

            migrationBuilder.Sql(@"
CREATE TRIGGER TR_ChampionRestrictions_Insert
ON ChampionRestrictions
AFTER INSERT
AS
BEGIN
    UPDATE Champions
    SET HasRestrictions = 1
    WHERE Id IN (SELECT ChampionId FROM inserted);
END;");

            migrationBuilder.Sql(@"
CREATE TRIGGER TR_ChampionRestrictions_Delete
ON ChampionRestrictions
AFTER DELETE
AS
BEGIN
    UPDATE Champions
    SET HasRestrictions = CASE
        WHEN EXISTS (SELECT 1 FROM ChampionRestrictions WHERE ChampionId = d.ChampionId)
        THEN 1 ELSE 0 END
    FROM deleted d
    WHERE Champions.Id = d.ChampionId;
END;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS TR_ChampionRestrictions_Insert;");
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS TR_ChampionRestrictions_Delete;");
            migrationBuilder.Sql("ALTER TABLE Champions DROP COLUMN HasRestrictions;");
        }
    }
}
