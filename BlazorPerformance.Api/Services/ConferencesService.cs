using Microsoft.EntityFrameworkCore;
using BlazorPerformance.Shared.Models;
using BlazorPerformance.Shared.Services;
using BlazorPerformance.Api.Data;

namespace BlazorPerformance.Api.Services;

public class ConferencesService : IConferenceService
{
    private readonly SampleDatabaseContext _context;

    public ConferencesService(SampleDatabaseContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public Task<List<Conference>> GetCollectionAsync(CollectionRequest request, CancellationToken cancellationToken)
    {
        var result = String.IsNullOrWhiteSpace(request.SearchTerm)
            ? _context.Conferences
            : _context.Conferences.Where(c => c.Title.Contains(request.SearchTerm));
        return result.Skip(request.Skip).Take(request.Take).ToListAsync(cancellationToken);
    }

    public Task<Conference?> GetItemAsync(int id, CancellationToken cancellationToken)
    {
        return _context.Conferences.FirstOrDefaultAsync(conf => conf.Id == id, cancellationToken);
    }

    public async Task<Conference?> GetRandomConference(CancellationToken cancellationToken)
    {
        var random = new Random();
        var conferenceCount = _context.Conferences.Count();
        if (conferenceCount > 0)
        {
            var randomIndex = random.Next(1, conferenceCount > 25 ? 25 : conferenceCount);
            return await _context.Conferences.FirstOrDefaultAsync(c => c.Id == randomIndex, cancellationToken);
        }
        return null;
    }
}
