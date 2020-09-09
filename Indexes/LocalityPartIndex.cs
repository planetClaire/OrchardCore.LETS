using LETS.Models;
using OrchardCore.ContentManagement;
using YesSql.Indexes;

namespace LETS.Indexes
{
    public class LocalityPartIndex : MapIndex
    {
        public string ContentItemId { get; set; }
        public string Postcode { get; set; }
    }
    public class LocalityPartIndexProvider : IndexProvider<ContentItem>
    {
        public override void Describe(DescribeContext<ContentItem> context) =>
            context.For<LocalityPartIndex>()
                .Map(contentItem =>
                {
                    var LocalityPart = contentItem.As<LocalityPart>();
                    
                    return LocalityPart == null
                        ? null
                        : new LocalityPartIndex
                        {
                            ContentItemId = contentItem.ContentItemId,
                            Postcode = LocalityPart.Postcode
                        };
                });
    }
}
