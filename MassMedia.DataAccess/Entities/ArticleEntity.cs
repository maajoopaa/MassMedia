namespace MassMedia.DataAccess.Entities
{
    public class ArticleEntity
    {
        public Guid Id { get; set; }

        public string Head { get; set; } = null!;

        public string Body { get; set; } = null!;

        public double? Latitude { get; set; } = null!;

        public double? Longitude { get; set; } = null!;

        public string? ImageUrl { get; set; } = null!;

        public bool IsAccepted { get; set; } = false;

        public Guid CreatedById { get; set; }

        public virtual UserEntity CreatedBy { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public Guid PublishPlaceId { get; set; }

        public virtual MassMediaEntity PublishPlace { get; set; } = null!;
    }
}
