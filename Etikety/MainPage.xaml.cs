using Etikety.Interfaces;
using Etikety.Models;

namespace Etikety;

public partial class MainPage : ContentPage
{
	private readonly IApiService _api;
	private readonly IPrinterService _printer;
	
	public MainPage(IApiService apiService,IPrinterService printer)
	{
		_api = apiService;
		_printer = printer;
		InitializeComponent();
	}

	private async void OnCounterClicked(object sender, EventArgs e)
	{
		if (!OrderId.Text.StartsWith("T"))
		{
			await DisplayAlert("Špatně zadáno", 
				"Zakázka musí začínat písmenem T", "OK");
			OrderId.Text = "T";
			return;
		}

		if (Convert.ToDouble(Weight.Text)==0)
		{
			await DisplayAlert("Špatně zadáno",
				"Váha musí být větší než 0", "OK");
			Weight.Text = "0";
			return;
		}

		if (Convert.ToInt32(TicketNo.Text)>15)
		{
			await DisplayAlert("Špatně zadáno",
				"Maximální počet etiket je 15", "OK");
			TicketNo.Text = "1";
			return;
		}

		UserData data = new UserData
		{
			OrderId = OrderId.Text,
			CalculationNumber = Convert.ToByte(CalculationNo.Text),
			NoOfLabels = Convert.ToInt32(TicketNo.Text),
			RequiredWeight = Convert.ToDouble(Weight.Text)
		};
		var apiData=await _api.GetLabel(data);
		switch (apiData.OrderId)
		{
			case "NoConnection":
				await DisplayAlert("Chyba síťě", 
					$"Nepodařilo se spojit se servrem",
					"OK");
				break;
			case "InvalidWeight":
				await DisplayAlert("Špatně zadáno", 
					$"Požadovaná váha {data.RequiredWeight} \n" +
							$" je větší než váha v kalkulaci ({apiData.RequestedWeight})",
					"OK");
				break;
			case "InvalidCalculationNumber":
				await DisplayAlert("Špatně zadáno",
					$"Číslo kalkulace {data.CalculationNumber} neexistuje", 
					"OK");
				break;
		}
		var printerStatus = await _printer.PrintLabels(apiData, data.NoOfLabels);
		switch (printerStatus)
		{
			case true:
				await DisplayAlert("Hotovo",
					$"{data.NoOfLabels} etikety pro:\n " +
					$"{data.OrderId}-{apiData.OperationName} \n odeslány do tiskárny :-)",
					"OK");
				OrderId.Text = "T";
				CalculationNo.Text = "0";
				TicketNo.Text = "1";
				Weight.Text = "0";
				break;
			
			case false:
				await DisplayAlert("Chyba",
					$"Nepodařilo se vytisknout etikety!",
					"OK");
				break;
		}
		
		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}


