namespace Etikety.Models;

public class ApiLabelResponse
{
    public string ProductName { get; set; }
    public string OrderId { get; set; }
    public string Color { get; set; }
    public string OperationName { get; set; }
    public double RequestedWeight { get; set; }
    public List<Chemistry> BaseChemistry { get; set; }
    public List<Chemistry> ColorChemistry { get; set; }
}

public class Chemistry
{
    public string ChemistryId { get; set; }
    public string ChemistryName { get; set; }
    public double Quantity { get; set; }
    public string Unit { get; set; }
}