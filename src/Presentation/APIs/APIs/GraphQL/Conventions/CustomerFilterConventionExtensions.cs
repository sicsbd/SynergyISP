using HotChocolate.Data.Filters;
using HotChocolate.Data.Filters.Expressions;
using SynergyISP.Application.Common.Helpers;
using SynergyISP.Presentation.APIs.GraphQL.Handlers;

namespace SynergyISP.Presentation.APIs.GraphQL.Conventions;
/// <summary>
/// The customer filter convention extensions.
/// </summary>
public static class CustomerFilterConventionExtensions
{
    public static IFilterConventionDescriptor AddSoundsLike(
        this IFilterConventionDescriptor descriptor)
    {
        descriptor.Operation(SoundsLikeHelper.Operation).Name(SoundsLikeHelper.OperationName);
        descriptor.Configure<StringOperationFilterInputType>(desc
            => desc
                .Operation(SoundsLikeHelper.Operation)
                .Name(SoundsLikeHelper.OperationName)
                .Type<StringType>());
        return descriptor;
    }

    public static IFilterConventionDescriptor UseSoundsLike(
        this IFilterConventionDescriptor descriptor)
    {
        descriptor.AddProviderExtension(
            new QueryableFilterProviderExtension(
                y => y.AddFieldHandler<SoundsLikeHandler>()));
        return descriptor;
    }
}