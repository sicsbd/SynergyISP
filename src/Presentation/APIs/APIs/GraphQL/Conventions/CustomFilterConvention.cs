using HotChocolate.Data.Filters;
using HotChocolate.Data.Filters.Expressions;
using SynergyISP.Application.Common.Helpers;
using SynergyISP.Presentation.APIs.GraphQL.Handlers;

namespace SynergyISP.Presentation.APIs.GraphQL.Conventions;
/// <summary>
/// The custom filtering convention.
/// </summary>
public class CustomFilteringConvention : FilterConvention
{
    /// <inheritdoc/>
    protected override void Configure(IFilterConventionDescriptor descriptor)
    {
        descriptor.AddDefaults();
        descriptor.Configure<StringOperationFilterInputType>(
            x => x.Operation(SoundsLikeHelper.Operation));
        descriptor.Provider(
            new QueryableFilterProvider(
                x => x
                    .AddDefaultFieldHandlers()
                    .AddFieldHandler<SoundsLikeHandler>()));
    }
}
