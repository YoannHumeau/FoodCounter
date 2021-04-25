using FluentMigrator;

namespace FoodCounter.DbMigrations.V1
{
    /// <summary>
    /// Migration user class
    /// </summary>
    [Migration(20210423140101)]
    public class _20210423140101_AddUser : Migration
    {
        private static string _tableName = "Users";

        public override void Down()
        {
            Delete.Table(_tableName);
        }

        public override void Up()
        {
            Create.Table(_tableName)
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Username").AsString(60).NotNullable()
                .WithColumn("Password").AsString(256).NotNullable()
                .WithColumn("Role").AsString(4).NotNullable();
        }
    }
}
