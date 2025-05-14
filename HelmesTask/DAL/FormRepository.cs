using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class FormRepository(AppDbContext context)
{
    public async Task<List<Sector>> GetAllSectorsAsync()
    {
        return await context.Sectors.ToListAsync();
    }

    public async Task<Form> AddFormAsync(Form entity)
    {
        context.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }
    
    public async Task<Form?> UpdateFormWithSectorsAsync(Form updatedForm, List<int> selectedSectorIds)
    {
        var existingForm = await context.Forms
            .Include(f => f.SectorInForms)
            .FirstOrDefaultAsync(f => f.Id == updatedForm.Id);

        if (existingForm == null) return null;
        
        existingForm.Name = updatedForm.Name;
        existingForm.AgreedToTerms = updatedForm.AgreedToTerms;
        
        var existingSectorIds = existingForm.SectorInForms!.Select(s => s.SectorId).ToList();
        
        var sectorsToRemove = existingForm.SectorInForms!
            .Where(s => !selectedSectorIds.Contains(s.SectorId))
            .ToList();
        
        context.RemoveRange(sectorsToRemove);
        
        var sectorsToAdd = selectedSectorIds
            .Where(id => !existingSectorIds.Contains(id))
            .Select(sectorId => new SectorInForm
            {
                FormId = updatedForm.Id,
                SectorId = sectorId
            });

        await context.SectorInForms.AddRangeAsync(sectorsToAdd);

        await context.SaveChangesAsync();

        return existingForm;
    }
    
    public async Task AddSectorsToFormAsync(int formId, List<int> sectorIds)
    {
        var newSectors = sectorIds
            .Select(sectorId => new SectorInForm
            {
                FormId = formId,
                SectorId = sectorId
            });

        await context.SectorInForms.AddRangeAsync(newSectors);
        await context.SaveChangesAsync();
    }
    
    public async Task<Form?> GetFormByIdAsync(int id)
    {
        return await context.Forms.FindAsync(id);
    }

    public async Task<List<int>> GetSectorIdsForFormAsync(int formId)
    {
        return await context.SectorInForms
            .Where(x => x.FormId == formId)
            .Select(x => x.SectorId)
            .ToListAsync();
    }
}