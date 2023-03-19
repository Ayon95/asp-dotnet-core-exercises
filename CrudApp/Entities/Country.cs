namespace Entities
{
    /// <summary>
    /// Domain model for storing country details
    /// </summary>
    public class Country
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }
}