using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
[Table("cliente")]

public class Users
{
    [Key]
    public int PkCliente { get; set; }
    public string Nombre { get; set; }
    public string Apellido1 { get; set; }
    public string Apellido2 { get; set; }
    public string Ciudad { get; set; }
    public int fk_categoria { get; set; }
}