namespace AuthenAuthor.Models.RequestModels;

public sealed record UserRequestModel
{
    public string UserName { get; set; }
    public string Password { get; set; }

}
