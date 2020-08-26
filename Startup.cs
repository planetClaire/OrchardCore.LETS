using System;
using Fluid;
using LETS.Drivers;
using LETS.Indexes;
using LETS.Migrations;
using LETS.Models;
using LETS.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using YesSql.Indexes;

namespace LETS
{
    public class Startup : StartupBase
    {
        static Startup()
        {
            TemplateContext.GlobalMemberAccessStrategy.Register<NoticeTypePartViewModel>();
        }
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddContentPart<NoticeTypePart>()
                .UseDisplayDriver<NoticeTypePartDisplayDriver>();
            services.AddScoped<IDataMigration, NoticeTypeMigrations>();
            services.AddSingleton<IIndexProvider, NoticeTypePartIndexProvider>();
        }

        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
        }
    }
}