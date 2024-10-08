using System.ComponentModel.DataAnnotations.Schema;

namespace SeCumple.CrossCutting.Entities;

public class Base
{
    [Column("iCodUsuarioRegistro")]
    public int CreatedBy { get; set; }        
    [Column("dFechaRegistro")]
    public DateTime CreationDate { get; set; }        
    [Column("cIpRegistro")]
    public string? CreationIp { get; set; }
    [Column("iCodUsuarioModificacion")]
    public int ModifiedBy { get; set; }
    [Column("dFecModificacion")]
    public DateTime ModificationDate { get; set; }       
    [Column("cIpModificacion")]
    public string? ModificationIp { get; set; }
    [Column("cEstado")] 
    public char Status { get; set; }
}