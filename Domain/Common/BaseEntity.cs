namespace Domain.Common;
public abstract class BaseEntity<T>
{
    public required T Id { get; init; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModifiedAt { get; set; }
    public string? LastModifiedBy { get; set; }
}
