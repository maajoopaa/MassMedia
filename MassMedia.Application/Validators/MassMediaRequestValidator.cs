using FluentValidation;
using MassMedia.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassMedia.Application.Validators
{
    public class MassMediaRequestValidator : AbstractValidator<MassMediaRequest>
    {
        public MassMediaRequestValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MinimumLength(3).WithMessage("Название не должно быть меньше 3 символов!");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Изображение не может быть пустым!");
        }
    }
}
