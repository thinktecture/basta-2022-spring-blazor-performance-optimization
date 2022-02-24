using BlazorPerformance.Shared.Models;
using System.ServiceModel;

namespace BlazorPerformance.Shared.Services;

[ServiceContract]
public interface IContributionsService : IDataService<Contribution>
{
}
