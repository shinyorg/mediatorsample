namespace OwnerModule.Contracts;


/// <summary>
/// 
/// </summary>
/// <param name="PersonId"></param>
/// <param name="VehicleId"></param>
/// <param name="Link">If false, we remove the ownership link</param>
public record LinkRequest(int PersonId, int VehicleId, bool Link) : IRequest;