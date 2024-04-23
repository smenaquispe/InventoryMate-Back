using InventoryMate.Models;

namespace InventoryMate.Dto;

public class LoginResponseDto
{
    public User? user { set; get; }
    public string? token { set; get; }
}