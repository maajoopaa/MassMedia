using FluentValidation;
using MassMedia.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassMedia.Application.Validators
{
    public class ArticleRequestValidator : AbstractValidator<ArticleRequest>
    {
        public ArticleRequestValidator()
        {
            RuleFor(x => x.Head).NotEmpty().MinimumLength(6).WithMessage("Название не может быть меньше 6 символов");
            RuleFor(x => x.Body).NotEmpty().MinimumLength(10).WithMessage("Описание не может быть меньше 10 символов");
            RuleFor(x => x.PublishPlaceId).NotEmpty().WithMessage("СМИ не может быть пустым");
        }
    }
}
