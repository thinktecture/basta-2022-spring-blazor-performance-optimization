using Microsoft.AspNetCore.Mvc;
using BlazorPerformance.Api.Services;
using BlazorPerformance.Shared.Services;
using BlazorPerformance.Shared.Models;

namespace BlazorPerformance.Api.Controllers;

[Route("[controller]")]
public class ContributionController : CollectionControllerBase<Contribution>
{
    public ContributionController(ContributionsService contributionsService)
        : base(contributionsService)
    { }
}
