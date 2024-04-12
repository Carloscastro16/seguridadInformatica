using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("logs")]

public class Logs
{
    [Key]
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public string NombreFuncion { get; set; }
    public string Ip { get; set; }
    public string Datos { get; set; }
    public string Response { get; set; }
}