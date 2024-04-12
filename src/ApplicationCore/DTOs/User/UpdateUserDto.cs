namespace ApplicationCore.DTOs.User;

public class UpdateUserDto
{
    public string Nombre { get; set; }
    public string Apellido1 { get; set; }
    public string Apellido2 { get; set; }
    public string Ciudad { get; set; }
    public int fk_categoria { get; set; }
}

