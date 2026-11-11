using EventoApp.Models;

namespace EventoApp.Views;

public partial class CadastroEventoPage : ContentPage
{
    private Evento _evento = new();

    public CadastroEventoPage()
    {
        InitializeComponent();
        BindingContext = _evento;
    }

    private async void OnConcluirClicked(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_evento.Nome))
        {
            await DisplayAlert("Atenção", "Informe o nome do evento.", "OK");
            return;
        }
        if (string.IsNullOrWhiteSpace(_evento.Local))
        {
            await DisplayAlert("Atenção", "Informe o local do evento.", "OK");
            return;
        }
        if (_evento.NumeroParticipantes <= 0)
        {
            await DisplayAlert("Atenção", "O número de participantes deve ser maior que zero.", "OK");
            return;
        }
        if (_evento.CustoPorParticipante < 0)
        {
            await DisplayAlert("Atenção", "O custo por participante não pode ser negativo.", "OK");
            return;
        }
        if (_evento.DataTermino < _evento.DataInicio)
        {
            await DisplayAlert("Atenção", "O término deve ser igual ou posterior ao início.", "OK");
            return;
        }

        await Navigation.PushAsync(new ResumoEventoPage(_evento));

        _evento = new Evento();   // prepara novo cadastro
        BindingContext = _evento;
    }
}