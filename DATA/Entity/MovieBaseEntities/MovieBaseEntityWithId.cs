namespace DATA.Entity.MovieBaseEntities
{
    public abstract class MovieBaseEntity
    {
        
    }
    public abstract class MovieBaseEntityWithId : MovieBaseEntity
    {
        public int EntityId { get; set;}
    }
}