using System;
using System.Linq;
using System.Windows.Input;

namespace CogenerationPlantSim.ViewModels;

/// <summary>
///     View model for all that concerns card numbers.
/// </summary>
internal interface ICardNumberViewModel : IViewModelBase {
    /// <summary>
    ///     Gets the
    ///     <see cref = "ICommand" />
    ///     for generating card numbers.
    /// </summary>
    internal ICommand GenerateCardNumberCommand { get; }

    /// <summary>
    ///     Gets or sets the current card number.
    /// </summary>
    internal string CardNumber { get; set; }
}

/// <inheritdoc cref = "ICardNumberViewModel" />
internal class CardNumberViewModel : ICardNumberViewModel {
    private static readonly Random       Random = new((int)DateTime.Now.Ticks);
    private readonly        RelayCommand _generateCardNumberCommand;


    /// <summary>
    /// Initialises a new instance of the <see cref="CardNumberViewModel"/> class.
    /// </summary>
    internal CardNumberViewModel () {
        this._generateCardNumberCommand = new RelayCommand(param => this.OnGenerateCardNumber(), _ => true);
    }

    /// <inheritdoc />
    public ICommand GenerateCardNumberCommand => this._generateCardNumberCommand;

    /// <inheritdoc />
    public string   CardNumber                { get; set; } = "";

    /// <summary>
    /// Generates a random number as a string.
    /// </summary>
    /// <returns>A randomised number as a valid string.</returns>
    private string RandomiseNumber () {
        const string pool = "0123456789";

        var chars = Enumerable.Range(0, 5).Select(x => pool[Random.Next(0, pool.Length)]);
        return new string(chars.ToArray());
    }

    /// <summary>
    /// The endpoint for the command <see cref="GenerateCardNumberCommand"/>
    /// Generates a card number.
    /// </summary>
    private void OnGenerateCardNumber () { this.CardNumber = this.RandomiseNumber(); }
}
