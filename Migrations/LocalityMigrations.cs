using LETS.Indexes;
using OrchardCore.Data.Migration;
using YesSql.Sql;

namespace LETS.Migrations
{
    public class LocalityMigrations : DataMigration
    {
        public int Create()
        {
            SchemaBuilder.CreateMapIndexTable<LocalityPartIndex>(table => table
                .Column<string>(nameof(LocalityPartIndex.Postcode))
                .Column<string>(nameof(LocalityPartIndex.ContentItemId))
            );

            return 1;
        }

    }
}
    