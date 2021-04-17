using FluentMigrator;

namespace FoodCounter.DbMigrations.V1
{
    /// <summary>
    /// Migration aliments class
    /// </summary>
    [Migration(20210419114801)]
    public class AddAliments_20210419114801 : Migration
    {
        /// <summary>
        /// Down migration
        /// </summary>
        public override void Down()
        {
            Delete.Table("Aliments");
        }

        /// <summary>
        /// Up migration
        /// </summary>
        public override void Up()
        {
            Create.Table("Aliments")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Name").AsString(60).NotNullable()
                .WithColumn("Barecode").AsFixedLengthString(13).Nullable()
                .WithColumn("Calories").AsInt32().WithDefaultValue(0)
                ;
        }
    }
}
