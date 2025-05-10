using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassMedia.Application.Models
{
    public class ArticleRequest
    {
        public string Head { get; set; } = null!;

        public string Body { get; set; } = null!;

        public double? Latitude { get; set; } = null!;

        public double? Longitude { get; set; } = null!;

        public string? ImageUrl { get; set; } = null!;

        public Guid PublishPlaceId { get; set; }
    }
}
