using DTO;
using Form = Domain.Form;

namespace DAL.Mappers;

public class FormMapper : IMapper<Form, DTO.Form>
{
    public Form? Map(DTO.Form? entity)
    {
        if (entity is null) return null;
        return new Form()
        {
            Id = entity.Id,
            Name = entity.Name,
            AgreedToTerms = entity.AgreedToTerms,
        };
    }

    public DTO.Form? Map(Form? entity)
    {
        if (entity is null) return null;
        return new DTO.Form()
        {
            Id = entity.Id,
            Name = entity.Name,
            AgreedToTerms = entity.AgreedToTerms,
        };
    }
    
    public Form? Map(FormCreate? entity)
    {
        if (entity is null) return null;
        return new Form()
        {
            Name = entity.Name,
            AgreedToTerms = entity.AgreedToTerms,
        };
    }
}