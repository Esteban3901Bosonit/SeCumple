using System.ComponentModel.DataAnnotations.Schema;
using SeCumple.CrossCutting.Entities;

namespace SeCumple.Domain.Entities;

[Table("MaeArchivo")]
public class FileUploaded: Base
{
    [Column("iMaeArchivo")]
    public int Id { get; set; }
    [Column("cNombreArchivo")]
    public string Name { get; set; }
    [Column("cExtension")]
    public string FileExtension { get; set; }
    [Column("cUbicacion")]
    public string Location { get; set; }
    [Column("cTamanoArchivo")]
    public string Size { get; set; }
    [Column("cFirmaDigitalArchivo")]
    public string FileSignature { get; set; }

}