using FluentMigrator;

namespace FoodCounter.DbMigrations.V1
{
    /// <summary>
    /// Migration aliments class
    /// </summary>
    [Migration(20210419114001)]
    public class AddLogTable : Migration
    {
        /// <summary>
        /// Up migration
        /// </summary>
        public override void Up()
        {
            Create.Table("Log")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Text").AsString();
        }

        /// <summary>
        /// Down migration
        /// </summary>
        public override void Down()
        {
            Delete.Table("Log");
        }
    }
}
