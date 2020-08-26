using LETS.Models;
using LETS.ViewModels;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using System.Threading.Tasks;

namespace LETS.Drivers
{
    public class NoticeTypePartDisplayDriver : ContentPartDisplayDriver<NoticeTypePart>
    {
        public override IDisplayResult Display(NoticeTypePart part, BuildPartDisplayContext context) =>
            Initialize<NoticeTypePartViewModel>(GetDisplayShapeType(context), viewModel => PopulateViewModel(part, viewModel))
                .Location("SummaryAdmin", "Content:1");

        public override IDisplayResult Edit(NoticeTypePart part, BuildPartEditorContext context) =>
            Initialize<NoticeTypePartViewModel>(GetEditorShapeType(context), viewModel => PopulateViewModel(part, viewModel));

        public override async Task<IDisplayResult> UpdateAsync(NoticeTypePart part, IUpdateModel updater, UpdatePartEditorContext context)
        {
            var viewModel = new NoticeTypePartViewModel();
            await updater.TryUpdateModelAsync(viewModel, Prefix);
            part.RequiredCount = viewModel.RequiredCount;
            part.SortOrder = viewModel.SortOrder;

            return Edit(part, context);
        }

        private static void PopulateViewModel(NoticeTypePart part, NoticeTypePartViewModel viewModel)
        {
            viewModel.NoticeTypePart = part;

            viewModel.RequiredCount = part.RequiredCount;
            viewModel.SortOrder = part.SortOrder;
        }
    }
}
