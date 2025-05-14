namespace Domain;

public class Sector
{
    public int Id { get; set; }
    
    public string Name { get; set; } = default!;
    
    public int? ParentSectorId { get; set; }

    public Sector? ParentSector { get; set; }
    
    public ICollection<SectorInForm>? SectorInForms { get; set; }
    public ICollection<Sector>? ChildSectors { get; set; }
}