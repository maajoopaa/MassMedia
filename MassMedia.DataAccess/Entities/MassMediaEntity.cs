
namespace MassMedia.DataAccess.Entities
{
    public class MassMediaEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public virtual ICollection<ArticleEntity> Articles { get; set; } = [];

        public virtual ICollection<UserEntity> Workers { get; set; } = [];
    }
}
