namespace DTO;

public class Sector
{
    public int Id { get; set; }
    
    public string Name { get; set; } = default!;
    
    public int? ParentSectorId { get; set; }
    
    public ICollection<Sector>? ChildSectors { get; set; }
}