using OrchardCore.ContentManagement;

namespace LETS.Models
{
    public class NoticeTypePart : ContentPart
    {
        public int RequiredCount { get; set; }
        public int SortOrder { get; set; }
    }
}
