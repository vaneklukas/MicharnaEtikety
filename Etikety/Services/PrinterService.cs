using Etikety.Interfaces;
using Etikety.Models;

namespace Etikety.Services;

public class PrinterService:IPrinterService
{
    public async Task<bool> PrintLabels(ApiLabelResponse apiData, int dataNoOfLabels)
    {
        Random rng = new Random();
        bool randomBool = rng.Next(0, 2) > 0;
        return randomBool;
    }
}