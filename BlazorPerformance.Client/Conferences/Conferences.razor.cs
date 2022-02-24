namespace BlazorPerformance.Client.Conferences
{
    public partial class Conferences
    {
        protected override void UpdateCollection(int id, int count)
        {
            Collection = Collection.Select(x =>
            {
                if (x.Id == id)
                {
                    x.VisitorsCount = count;
                }
                return x;
            }).ToList();
            StateHasChanged();
        }
    }
}