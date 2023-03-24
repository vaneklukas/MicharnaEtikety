using Etikety.Models;

namespace Etikety.Interfaces;

public interface IApiService
{
    Task<ApiLabelResponse> GetLabel(UserData data);
}