namespace SeCumple.Application.Components.Interventions.Dtos;

public class RegionResponse
{
    public int iMaeRegion { get; set; }
    public string cNombre { get; set; }
    public string cUbigeo { get; set; }
    public IReadOnlyList<ProvinceResponse>? Provinces { get; set; }
}