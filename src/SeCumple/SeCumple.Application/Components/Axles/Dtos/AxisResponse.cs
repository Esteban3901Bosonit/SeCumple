using SeCumple.Application.Components.GuideLines.Dtos;

namespace SeCumple.Application.Components.Axles.Dtos;

public class AxisResponse
{
    public int iMaeEje { get; set; }
    public string cTitulo { get; set; }
    public string cNum { get; set; }
    public string cEstado { get; set; }
    public string cTipoRegistro { get; set; }
    public char cValidado { get; set; }
    public ICollection<GuideLineResponse>? listLineamiento { get; set; }
}