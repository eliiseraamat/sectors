namespace Domain;

public class SectorInForm
{
    public int Id { get; set; }
    
    public int SectorId { get; set; }
    public Sector? Sector { get; set; }
    
    public int FormId { get; set; }
    public Form? Form { get; set; }
}