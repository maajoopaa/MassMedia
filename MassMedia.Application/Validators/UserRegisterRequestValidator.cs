using FluentValidation;
using MassMedia.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassMedia.Application.Validators
{
    public class UserRegisterRequestValidator : AbstractValidator<UserRegisterRequest>
    {
        public UserRegisterRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Не действительная электронную почта!");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Пароль должен быть от 6 символов!");
        }
    }
}
