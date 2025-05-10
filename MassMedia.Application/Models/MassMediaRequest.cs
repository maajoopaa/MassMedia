using MassMedia.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassMedia.Application.Models
{
    public class MassMediaRequest
    {
        public string Title { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public List<UserEntity> Workers { get; set; } = [];
    }
}
