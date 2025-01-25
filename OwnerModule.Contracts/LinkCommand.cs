using System.ComponentModel.DataAnnotations;

namespace OwnerModule.Contracts;


/// <summary>
/// 
/// </summary>
/// <param name="PersonId"></param>
/// <param name="VehicleId"></param>
/// <param name="Link">If false, we remove the ownership link</param>
[Validate]
public class LinkCommand : ICommand
{
    [Required] public int? PersonId { get; set; }
    [Required] public int? VehicleId { get; set; }
    public bool Link { get; set; }
}