namespace BooksmartAPI.Repositories.Base
{
    public class EntityNotFoundException<TEntity, TId> : Exception where TEntity : BaseEntity<TId>
    {
        public EntityNotFoundException(string entityName, TEntity? entity) : base($"{entityName} was not found.")
        {
            Entity = entity;
        }

        public TEntity? Entity { get; set; }
    }
}
