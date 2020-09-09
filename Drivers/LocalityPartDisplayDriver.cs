using LETS.Models;
using LETS.ViewModels;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using System.Threading.Tasks;

namespace LETS.Drivers
{
    public class LocalityPartDisplayDriver : ContentPartDisplayDriver<LocalityPart>
    {
        public override IDisplayResult Display(LocalityPart part, BuildPartDisplayContext context) =>
            Initialize<LocalityPartViewModel>(GetDisplayShapeType(context), viewModel => PopulateViewModel(part, viewModel))
                .Location("SummaryAdmin", "Content:1");

        public override IDisplayResult Edit(LocalityPart part, BuildPartEditorContext context) =>
            Initialize<LocalityPartViewModel>(GetEditorShapeType(context), viewModel => PopulateViewModel(part, viewModel));

        public override async Task<IDisplayResult> UpdateAsync(LocalityPart part, IUpdateModel updater, UpdatePartEditorContext context)
        {
            var viewModel = new LocalityPartViewModel();
            await updater.TryUpdateModelAsync(viewModel, Prefix);
            part.Postcode = viewModel.Postcode;

            return Edit(part, context);
        }

        private static void PopulateViewModel(LocalityPart part, LocalityPartViewModel viewModel)

        {
            viewModel.LocalityPart = part;

            viewModel.Postcode = part.Postcode;
        }
    }
}
