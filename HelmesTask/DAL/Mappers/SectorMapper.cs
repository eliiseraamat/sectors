using Domain;

namespace DAL.Mappers;

public class SectorMapper : IMapper<Sector, DTO.Sector>
{
    public Sector? Map(DTO.Sector? entity)
    {
        if (entity == null) return null;
        return new Sector()
        {
            Id = entity.Id,
            Name = entity.Name,
            ParentSectorId = entity.ParentSectorId,
            ChildSectors = entity.ChildSectors?.Select(c => Map(c)!).ToList(),
        };
    }

    public DTO.Sector? Map(Sector? entity)
    {
        if (entity == null) return null;
        return new DTO.Sector()
        {
            Id = entity.Id,
            Name = entity.Name,
            ParentSectorId = entity.ParentSectorId,
            ChildSectors = entity.ChildSectors?.Select(c => Map(c)!).ToList()
        };
    }
}