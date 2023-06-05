using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Windows;

namespace CogenerationPlantSim; 

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    private readonly TcpClient _tcpClient;
    private          string    _cardNumber;

    private string _connectButtonContent;
    private int    _networkPort;

    public MainWindow () {
        this._cardNumber           = "";
        this._networkPort          = 16500;
        this._connectButtonContent = "Connect";
        this._tcpClient            = new TcpClient("localhost", this._networkPort);
        this.InitializeComponent();
        this.DataContext = this;
    }

    public string CardNumber {
        get => this._cardNumber;

        set {
            if (this._cardNumber == value)
                return;

            this._cardNumber = value;
            this.OnPropertyChanged();
        }
    }

    public int NetworkPort {
        get => this._networkPort;

        set {
            if (this._networkPort == value)
                return;

            this._networkPort = value;
            this.OnPropertyChanged();
        }
    }

    public string ConnectButtonContent {
        get => this._connectButtonContent;

        set {
            if (this._connectButtonContent == value)
                return;
            this._connectButtonContent = value;
            this.OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged ([CallerMemberName] string? propertyName = null) {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T> (ref T field, T value, [CallerMemberName] string? propertyName = null) {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;
        field = value;
        this.OnPropertyChanged(propertyName);
        return true;
    }

    private void OnGenerateNumberButtonClicked (object sender, RoutedEventArgs routedEventArgs) {
        this.CardNumber = this.RandomiseNumber();
    }

    private void OnSendNumberButtonClicked (object sender, RoutedEventArgs routedEventArgs) {
        if (this._tcpClient.Connected) { } else {
            _ = MessageBox.Show("Client is disconnected.", "disconnected", MessageBoxButton.OK, MessageBoxImage.Warning
                              , MessageBoxResult.OK);
        }
    }

    private async void OnConnectButtonClicked (object sender, RoutedEventArgs routedEventArgs) {
        if (this._tcpClient.Connected) {
            await this._tcpClient.Client.DisconnectAsync(true);
            this.ConnectButtonContent = "Connect";
        } else {
            await this._tcpClient.Client.ConnectAsync("localhost", this.NetworkPort);
            this.ConnectButtonContent = "Disconnect";
        }
    }
}
