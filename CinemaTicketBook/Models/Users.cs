public class Users
{
    [Key]
    public string _id { get; set; }
    public required string name { get; set; }
    public required string password { get; set; }
    public required string email { get; set; }
    public string? city { get; set; }
    public DateTime? createdAt { get; set; }
    public DateTime? updatedAt { get; set; }
}