using Etikety.Models;

namespace Etikety.Interfaces;

public interface IPrinterService
{
    Task<bool> PrintLabels(ApiLabelResponse apiData, int dataNoOfLabels);
}