using DAL;
using DAL.Mappers;
using DTO;

namespace Services;

public class FormService(FormRepository repository)
{
    private readonly SectorMapper _sectorMapper = new();
    private readonly FormMapper _formMapper = new();

    public async Task<IEnumerable<Sector?>> GetSectorsAsync()
    {
        var sectors = await repository.GetAllSectorsAsync();
        
        sectors = BuildSectorTree(sectors);
        SortSectors(sectors);
        
        return sectors.Select(s => _sectorMapper.Map(s));
    }

    private List<Domain.Sector> BuildSectorTree(List<Domain.Sector> sectors)
    {
        var sectorDict = sectors.ToDictionary(s => s.Id);
        var roots = new List<Domain.Sector>();

        foreach (var sector in sectors)
        {
            if (sector.ParentSectorId == null)
            {
                roots.Add(sector);
            }
            else if (sectorDict.TryGetValue(sector.ParentSectorId.Value, out var parent))
            {
                parent.ChildSectors ??= new List<Domain.Sector>();
                parent.ChildSectors.Add(sector);
            }
        }
        return roots;
    }

    private void SortSectors(List<Domain.Sector> sectors)
    {
        sectors.Sort((s1, s2) => string.Compare(s1.Name, s2.Name, StringComparison.InvariantCultureIgnoreCase));
        foreach (var sector in sectors)
        {
            if (sector.ChildSectors == null) continue;
            
            var sortedChildren = sector.ChildSectors.OrderBy(s => s.Name, StringComparer.InvariantCultureIgnoreCase).ToList();
            sector.ChildSectors = sortedChildren;
            
            SortSectors(sortedChildren);
        }
    }
    
    public async Task<Form?> PostFormAsync(FormCreate? entity)
    {
        var form = _formMapper.Map(entity);
        if (form == null) return null;
        
        await repository.AddFormAsync(form);
        
        return _formMapper.Map(form);
    }
    
    public async Task PostSectorInFormsAsync(Form form, List<int> sectors)
    {
        var mapped = _formMapper.Map(form);
        await repository.AddSectorsToFormAsync(mapped!.Id, sectors);
    }
    
    public async Task UpdateFormWithSectorsAsync(Form form, List<int> sectors)
    {
        var mapped = _formMapper.Map(form);
        await repository.UpdateFormWithSectorsAsync(mapped!, sectors);
    }

    public async Task<FormWithSectors?> GetFormWithSectorsAsync(int formId)
    {
        var form = await repository.GetFormByIdAsync(formId);
        if (form == null)
        {
            return null;
        }

        var sectorIds = await repository.GetSectorIdsForFormAsync(formId);

        return new FormWithSectors
        {
            Form = _formMapper.Map(form)!,
            SectorIds = sectorIds
        };
    }
}