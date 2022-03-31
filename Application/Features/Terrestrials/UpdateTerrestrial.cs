using Domain;
using FluentValidation;
using MediatR;

namespace Application.Features.Terrestrials
{
    public class UpdateTerrestrial
    {
        public class UpdateTerrestrialCommand : IRequest<Terrestrial>
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Color { get; set; }
            public int LegsNumber { get; set; }
            public bool IsFun { get; set; }
        }
        
        public class UpdateTerrestrialValidator : AbstractValidator<UpdateTerrestrialCommand>
        {
            public UpdateTerrestrialValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Color).NotEmpty();
                RuleFor(x => x.LegsNumber);
                RuleFor(x => x.IsFun);
            }
        }
    }
}