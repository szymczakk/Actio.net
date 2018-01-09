namespace Actio.Common.Events
{
  public class UserAuthenticated : IEvent
  {
    public string Email { get; }

    protected UserAuthenticated()
    {
        
    }

    public UserAuthenticated(string email)
    {
      this.Email = email;
    }
  }
}