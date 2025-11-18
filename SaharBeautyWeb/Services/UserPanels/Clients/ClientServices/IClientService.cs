using SaharBeautyWeb.Configurations.Interfaces;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Clients.Dtos;

namespace SaharBeautyWeb.Services.UserPanels.Clients.ClientServices;

public interface IClientService : IService
{
    Task<ApiResultDto<string>> AddNewClient(AddNewClientDto dto);

    Task<ApiResultDto<List<GetAllClientsForAddAppointmentDto>>>
        GetAllForAppointment(string? search = null);
}
