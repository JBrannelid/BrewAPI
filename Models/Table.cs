namespace BrewAPI.Models
{
    public class Table : IEntity
    {
        public int PK_TableId { get; set; }
        public int Id => PK_TableId; 
        public int TableNumber { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }
        public virtual List<Booking> Bookings { get; set; }
    }
}

// Table entity: stores information about a restaurant table
// Navigation property links to related bookings
// Data validation and constraints are handled via Fluent API in DbContext