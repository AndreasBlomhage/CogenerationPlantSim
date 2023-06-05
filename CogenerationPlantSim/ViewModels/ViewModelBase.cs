using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CogenerationPlantSim.ViewModels;

/// <summary>
///     Base class for all view models in the simulator.
/// </summary>
internal interface IViewModelBase { }

/// <inheritdoc cref = "IViewModelBase" />
internal class ViewModelBase : IViewModelBase, INotifyPropertyChanged, IDisposable {
    public void Dispose () {
        this.Dispose(true);
        GC.SuppressFinalize(this);
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

    protected virtual void Dispose (bool disposing) {
        if (disposing) { }
    }
}
