using System.ComponentModel.DataAnnotations;

namespace BooksmartAPI.Repositories.Base
{
    public abstract class BaseEntity<TId>
    {
        [Key]
        public virtual TId Id { get; set; }
    }
}
