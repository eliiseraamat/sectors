using Domain;

namespace DAL.DataSeeding;

public class AppDataInit
{
    public static void SeedAppData(AppDbContext context)
    {
        if (context.Sectors.Any()) return;
        
        var sectors = new Dictionary<int, Sector>();

        foreach (var sector in InitialData.Sectors)
        {
            var newSector = new Sector()
            {
                Id = sector.Id,
                Name = sector.Name,
                ParentSectorId = sector.ParentSectorId,
                ChildSectors = new List<Sector>() 
            };
            sectors.Add(sector.Id, newSector);
            context.Sectors.Add(newSector);
        }

        foreach (var sector in sectors)
        {
            if (sector.Value.ParentSectorId == null) continue;
            var parentSector = sectors[sector.Value.ParentSectorId.Value];

            parentSector.ChildSectors?.Add(sector.Value);

            sector.Value.ParentSector = parentSector;
        }

        context.SaveChanges();
    }
}