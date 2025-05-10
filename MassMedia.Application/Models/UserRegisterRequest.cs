using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassMedia.Application.Models
{
    public class UserRegisterRequest
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string RepeatPassword { get; set; } = null!;
    }
}
