using LETS.Indexes;
using OrchardCore.Data.Migration;
using YesSql.Sql;

namespace LETS.Migrations
{
    public class NoticeTypeMigrations : DataMigration
    {
        public int Create()
        {

            SchemaBuilder.CreateMapIndexTable<NoticeTypePartIndex>(table => table
                .Column<string>(nameof(NoticeTypePartIndex.SortOrder))
                .Column<string>(nameof(NoticeTypePartIndex.ContentItemId))
            );

            return 1;
        }

    }
}
