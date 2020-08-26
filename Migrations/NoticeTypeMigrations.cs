using LETS.Indexes;
using LETS.Models;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data.Migration;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Title.Models;
using YesSql.Sql;
using OrchardCore.Autoroute.Models;

namespace LETS.Migrations
{
    public class NoticeTypeMigrations : DataMigration
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public NoticeTypeMigrations(IContentDefinitionManager contentDefinitionManager) => _contentDefinitionManager = contentDefinitionManager;
        public int Create()
        {
            _contentDefinitionManager.AlterPartDefinition(nameof(NoticeTypePart), part => part.Attachable());

            SchemaBuilder.CreateMapIndexTable<NoticeTypePartIndex>(table => table
                .Column<string>(nameof(NoticeTypePartIndex.SortOrder))
                .Column<string>(nameof(NoticeTypePartIndex.ContentItemId), column => column.WithLength(26))
            );

            _contentDefinitionManager.AlterTypeDefinition(ContentTypes.NoticeType, type => type
                .Creatable()
                .Listable()
                .WithPart(nameof(TitlePart))
                .WithPart(nameof(NoticeTypePart))
                .WithPart("AutoroutePart", part => part
                    .WithPosition("3")
                    .WithSettings(new AutoroutePartSettings
                    {
                        Pattern = "{{ Model.ContentItem | display_text | slugify }}",
                        AllowRouteContainedItems = true
                    }))
            );

            return 4;
        }

    }
}
