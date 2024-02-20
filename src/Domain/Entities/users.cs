using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
[Table("cliente")]

public class Users
{
    [Key]
    public int pkCliente { get; set; }
    public string nombre { get; set; }
    public string ciudad { get; set; }
}