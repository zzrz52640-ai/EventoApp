using EventoApp.Models;

namespace EventoApp.Views;

public partial class ResumoEventoPage : ContentPage
{
    public ResumoEventoPage(Evento evento)
    {
        InitializeComponent();
        BindingContext = evento;
    }

    private async void OnNovoCadastroClicked(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}