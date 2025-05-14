namespace Domain;

public class Form
{
    public int Id { get; set; }
    
    public string Name { get; set; } = default!;
    
    public bool AgreedToTerms { get; set; }
    
    public ICollection<SectorInForm>? SectorInForms { get; set; }
}