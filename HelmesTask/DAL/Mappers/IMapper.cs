namespace DAL.Mappers;

public interface IMapper<TDomain, TDto>
{
    public TDomain? Map(TDto? entity);
    
    public TDto? Map(TDomain? entity);
}