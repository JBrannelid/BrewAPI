namespace BrewAPI.Models

// Helper interface for mapping entities with an Id property
// Keep our repository generic and reusable without dealing with specific PK-Key properties

{
    public interface IEntity
    {
        int Id { get; }
    }
}