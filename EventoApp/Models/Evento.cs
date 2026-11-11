using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace EventoApp.Models;

public class Evento : INotifyPropertyChanged
{
    private static readonly CultureInfo PtBr = new("pt-BR");

    private string _nome = string.Empty;
    private DateTime _dataInicio = DateTime.Today;
    private DateTime _dataTermino = DateTime.Today;
    private int _numeroParticipantes;
    private string _local = string.Empty;
    private decimal _custoPorParticipante;

    public string Nome
    {
        get => _nome;
        set => SetProperty(ref _nome, value);
    }

    public DateTime DataInicio
    {
        get => _dataInicio;
        set
        {
            if (!SetProperty(ref _dataInicio, value)) return;
            if (DataTermino < value) DataTermino = value;
            NotificarDuracao();
        }
    }

    public DateTime DataTermino
    {
        get => _dataTermino;
        set { if (SetProperty(ref _dataTermino, value)) NotificarDuracao(); }
    }

    public int NumeroParticipantes
    {
        get => _numeroParticipantes;
        set { if (SetProperty(ref _numeroParticipantes, value)) NotificarCusto(); }
    }

    public string Local
    {
        get => _local;
        set => SetProperty(ref _local, value);
    }

    public decimal CustoPorParticipante
    {
        get => _custoPorParticipante;
        set { if (SetProperty(ref _custoPorParticipante, value)) NotificarCusto(); }
    }

    // ----- Cálculos -----
    public TimeSpan Duracao => DataTermino.Date - DataInicio.Date;
    public int DuracaoEmDias => Duracao.Days + 1;   // início e término contam
    public decimal CustoTotal => NumeroParticipantes * CustoPorParticipante;

    // ----- Textos formatados -----
    public string DataInicioFormatada => DataInicio.ToString("dd/MM/yyyy (dddd)", PtBr);
    public string DataTerminoFormatada => DataTermino.ToString("dd/MM/yyyy (dddd)", PtBr);
    public string DuracaoTimeSpanTexto => Duracao.ToString("c");
    public string DuracaoEmDiasTexto => $"{DuracaoEmDias} dia(s)";
    public string CustoPorParticipanteFormatado => CustoPorParticipante.ToString("C", PtBr);
    public string CustoTotalFormatado => CustoTotal.ToString("C", PtBr);
    public string ParticipantesTexto => NumeroParticipantes.ToString("N0", PtBr);

    // ----- INotifyPropertyChanged -----
    public event PropertyChangedEventHandler? PropertyChanged;

    private bool SetProperty<T>(ref T campo, T valor, [CallerMemberName] string? nome = null)
    {
        if (EqualityComparer<T>.Default.Equals(campo, valor)) return false;
        campo = valor;
        OnPropertyChanged(nome);
        return true;
    }

    private void OnPropertyChanged(string? nome) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));

    private void NotificarDuracao()
    {
        OnPropertyChanged(nameof(Duracao));
        OnPropertyChanged(nameof(DuracaoEmDias));
        OnPropertyChanged(nameof(DuracaoTimeSpanTexto));
        OnPropertyChanged(nameof(DuracaoEmDiasTexto));
        OnPropertyChanged(nameof(DataInicioFormatada));
        OnPropertyChanged(nameof(DataTerminoFormatada));
    }

    private void NotificarCusto()
    {
        OnPropertyChanged(nameof(CustoTotal));
        OnPropertyChanged(nameof(CustoTotalFormatado));
        OnPropertyChanged(nameof(CustoPorParticipanteFormatado));
        OnPropertyChanged(nameof(ParticipantesTexto));
    }
}