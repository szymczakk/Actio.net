using System;

namespace Actio.Common.Events
{
  public class ActivityCreated : IAuthenticatedEvent
  {
    protected ActivityCreated()
    {

    }
    public Guid Id { get; }
    public Guid UserId { get; }
    public string Category { get; }
    public string Name { get; }
    public string Description { get; }
    public DateTime CreatedAt { get; }

    public ActivityCreated(Guid id, Guid userId, string category, string name, string description, DateTime createdAt)
    {
      this.CreatedAt = createdAt;
      this.Description = description;
      this.UserId = userId;
      this.Name = name;
      this.Id = id;
      this.Category = category;
    }

  }
}