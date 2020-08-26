using LETS.Models;
using OrchardCore.ContentManagement;
using YesSql.Indexes;

namespace LETS.Indexes
{
    public class NoticeTypePartIndex : MapIndex
    {
        public string ContentItemId { get; set; }
        public int SortOrder { get; set; }
    }
    public class NoticeTypePartIndexProvider : IndexProvider<ContentItem>
    {
        public override void Describe(DescribeContext<ContentItem> context) =>
            context.For<NoticeTypePartIndex>()
                .Map(contentItem =>
                {
                    var noticeTypePart = contentItem.As<NoticeTypePart>();
                    
                    return noticeTypePart == null
                        ? null
                        : new NoticeTypePartIndex
                        {
                            ContentItemId = contentItem.ContentItemId,
                            SortOrder = noticeTypePart.SortOrder
                        };
                });
    }
}
