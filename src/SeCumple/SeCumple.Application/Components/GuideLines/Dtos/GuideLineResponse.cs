namespace SeCumple.Application.Components.GuideLines.Dtos;

public class GuideLineResponse
{
    public int iMaeLineamiento { get; set; }
    public int iMaeEje { get; set; }
    public string cNum { get; set; }
    public string cNumEje { get; set; }
    public string cDescripcion { get; set; }
    public string cEstado { get; set; }
    public string cTipoRegistro { get; set; }
    public char? cValidado { get; set; }
}