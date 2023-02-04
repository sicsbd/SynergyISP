
using System.Linq.Expressions;
using System.Reflection;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Filters.Expressions;
using HotChocolate.Language;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using SynergyISP.Application.Common.Helpers;
using SynergyISP.Domain.Helpers;

namespace SynergyISP.Presentation.APIs.GraphQL.Handlers;
/// <summary>
/// The SoundsLinkHandler already has an implemenation of CanHandle
/// It checks if the field is declared in a string operation type and also checks if
/// the operation of this field uses the `Operation` specified in the override property further
/// below.
/// </summary>
public class SoundsLikeHandler : QueryableStringOperationHandler
{
    // For creating a expression tree we need the `MethodInfo` of the `ToLower` method of string
    private static readonly MethodInfo _soundex = typeof(SoundsLikeHelper)
        .GetMethods(BindingFlags.Public | BindingFlags.Static)
        .Single(x
            => x.Name.EqualsOrdinalIgnoreCase(nameof(SoundsLikeHelper.Soundex))
            && x.GetParameters().Length == 1);

    // For creating a expression tree we need the `MethodInfo` of the `ToLower` method of string
    private static readonly MethodInfo _soundsLike = typeof(SoundsLikeHelper)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Single(x
                => x.Name.EqualsOrdinalIgnoreCase(nameof(SoundsLikeHelper.SoundLike))
                && x.GetParameters().Length <= 2);

    /// <summary>
    /// Initializes a new instance of the <see cref="SoundsLikeHandler"/> class.
    /// </summary>
    /// <param name="inputParser">The input parser.</param>
    public SoundsLikeHandler(InputParser inputParser)
        : base(inputParser)
    {
    }

    /// <summary>
    /// Gets the operation.
    /// </summary>
    protected override int Operation => SoundsLikeHelper.Operation;

    /// <inheritdoc/>
    public override Expression HandleOperation(
        QueryableFilterContext context,
        IFilterOperationField field,
        IValueNode value,
        object? parsedValue)
    {
        // We get the instance of the context. This is the expression path to the property
        // e.g. ~> y.Street
        Expression property = context.GetInstance();

        // the parsed value is what was specified in the query
        // e.g. ~> eq: "221B Baker Street"
        if (parsedValue is string str)
        {
            // Creates and returnes the operation
            // e.g. ~> y.Street.Soundex() == "221b baker street".Soundex()
            return Expression.Equal(
                SqlExpression.Call(_soundex, arguments: property),
                SqlExpression.Call(_soundex, Expression.Constant(str)));
        }

        // Something went wrong 😱
        throw new InvalidOperationException();
    }
}
