namespace DTO;

public class FormWithSectors
{
    public Form Form { get; set; } = default!;
    public List<int> SectorIds { get; set; } = new();
}