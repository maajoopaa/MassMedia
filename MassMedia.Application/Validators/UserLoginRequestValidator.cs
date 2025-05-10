using FluentValidation;
using MassMedia.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassMedia.Application.Validators
{
    public class UserLoginRequestValidator : AbstractValidator<UserLoginRequest>
    {
        public UserLoginRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Не действительная электронную почта!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Пароль не может быть пустым!");
        }
    }
}
