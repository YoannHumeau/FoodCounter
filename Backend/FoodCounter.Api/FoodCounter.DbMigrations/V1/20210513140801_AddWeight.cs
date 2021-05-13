using FluentMigrator;

namespace FoodCounter.DbMigrations.V1
{
    /// <summary>
    /// Migration Weight class
    /// </summary>
    [Migration(20210513140801)]
    public class _20210513140801_AddWeight : Migration
    {
        private readonly string _tableName = "UserWeights";

        /// <summary>
        /// Down migration
        /// </summary>
        public override void Down()
        {
            Delete.Table(_tableName);
        }

        /// <summary>
        /// Up migration
        /// </summary>
        public override void Up()
        {
            Create.Table(_tableName)
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("UserId").AsInt64().NotNullable()
                .WithColumn("WeightDate").AsDate().NotNullable()
                .WithColumn("Weight").AsInt32().NotNullable()
                ;

            Create.UniqueConstraint("CONSTRAINT_Unique_UserId_WeightDate")
                .OnTable(_tableName)
                .Columns("UserId", "WeightDate")
                ;
        }
    }
}
