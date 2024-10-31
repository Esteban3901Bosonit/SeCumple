using System.ComponentModel.DataAnnotations.Schema;
using SeCumple.CrossCutting.Entities;

namespace SeCumple.Domain.Entities;

[Table("MaePersona")]
public class Person:Base
{
    [Column("iMaePersona")]
    public int Id { get; set; }
    [Column("cNombre")]
    public string Name { get; set; }
    [Column("cInstitucion")]
    public string Institution { get; set; }
    [Column("cOficina")]
    public string Office { get; set; }
    [Column("cCargo")]
    public string Role { get; set; }
    [Column("cCelular")]
    public string PhoneNumber { get; set; }
    [Column("cEmail")]
    public string Email { get; set; }
}