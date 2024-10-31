using SeCumple.Domain.Entities;

namespace SeCumple.Application.Components.Interventions.Dtos;

public class ProvinceResponse
{
    public int iMaeProvincia { get; set; }
    public string cNombre { get; set; }
    public string cUbigeo { get; set; }
    public IReadOnlyList<DistrictResponse>? Districts { get; set; }
}