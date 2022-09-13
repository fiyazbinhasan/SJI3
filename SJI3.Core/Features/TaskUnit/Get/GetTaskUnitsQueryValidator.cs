using FluentValidation;
using SJI3.Core.Common.Domain;

namespace SJI3.Core.Features.TaskUnit.Get;

public class GetTaskUnitsQueryValidator : AbstractValidator<IGetTaskUnitsQuery>
{
    public GetTaskUnitsQueryValidator(ITypeHelperService typeHelperService)
    {
        RuleFor(i => i.ResourceParameters)
            .SetValidator(new ResourceParametersValidator(typeHelperService));
    }
}

public class ResourceParametersValidator : AbstractValidator<ResourceParameters>
{
    public ResourceParametersValidator(ITypeHelperService typeHelperService)
    {
        RuleFor(i => i.Fields)
            .Custom((fields, context) =>
            {
                if (!typeHelperService.TypeHasProperties<TaskUnitModel>(fields))
                {
                    context.AddFailure($"[{fields}] contains one or more invalid entry for data shaping");
                }
            });
    }
}