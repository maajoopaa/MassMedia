using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassMedia.DataAccess.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public Role Role { get; set; }

        public virtual ICollection<ArticleEntity> Articles { get; set; } = [];

        public Guid? WorkPlaceId { get; set; } = null!;

        public virtual MassMediaEntity? WorkPlace { get; set; }
    }
}
