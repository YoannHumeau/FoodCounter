using FluentMigrator;

namespace FoodCounter.DbMigrations.V1
{
    /// <summary>
    /// Migration aliment consumed class
    /// </summary>
    [Migration(20210508221101)]
    public class _20210508221101_AddAlimentConsume : Migration
    {
        /// <summary>
        /// Down migration
        /// </summary>
        public override void Down()
        {
            Delete.Table("Alimentconsumes");
        }

        /// <summary>
        /// Up migration
        /// </summary>
        public override void Up()
        {
            Create.Table("Alimentconsumes")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("UserId").AsInt64().NotNullable()
                .ForeignKey("Users", "Id")
                .WithColumn("AlimentId").AsInt64().NotNullable()
                .ForeignKey("Aliments", "Id")
                .WithColumn("ConsumeDate").AsDateTime().NotNullable()
                .WithColumn("Weight").AsInt32().WithDefaultValue(0)
                ;
        }
    }
}
