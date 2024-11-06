using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public abstract class ActionsBase
{
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    [ConcurrencyCheck]
    public DateTime? UpdatedAt { get; set; } 
}