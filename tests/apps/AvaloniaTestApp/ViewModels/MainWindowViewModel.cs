using System;
using System.Reactive.Linq;
using System.Windows.Input;
using ReactiveUI;
using Avalonia.Controls;

namespace AvaloniaTestApp.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private string _greeting = "Welcome to Avalonia!";
    public string Greeting
    {
        get => _greeting;
        set
        {
            if (value.Contains("hi"))
                throw new ArgumentException("No hi allowed");
            this.RaiseAndSetIfChanged(ref _greeting, value);
        }
    }

    private bool _isDynamicButtonVisible = false;
    public bool IsDynamicButtonVisible
    {
        get => _isDynamicButtonVisible;
        set 
        { 
            this.RaiseAndSetIfChanged(ref _isDynamicButtonVisible, value);
            this.RaisePropertyChanged(nameof(DynamicRemoveButton));
        }
    }

    public Button? DynamicRemoveButton
    {
        get
        {
            if (!_isDynamicButtonVisible)
                return null;

            return new Button
            {
                Content = "Remove Me!",
                Command = RemoveButtonCommand,
                Margin = new Avalonia.Thickness(5, 0),
                [Avalonia.Automation.AutomationProperties.AutomationIdProperty] = "DynamicRemoveButton"
            };
        }
    }

    readonly string[] greetings = ["Hello", "Hi", "What's up?", "How are you?"];
    readonly Random random = new();

    public ICommand AddButtonCommand { get; }
    public ICommand RemoveButtonCommand { get; }

    public MainWindowViewModel()
    {
        AddButtonCommand = ReactiveCommand.Create(OnAddButton, 
            this.WhenAnyValue(x => x.IsDynamicButtonVisible).Select(visible => !visible));
        RemoveButtonCommand = ReactiveCommand.Create(OnRemoveButton);
    }

    public void ClickCommand()
    {        
        Greeting = greetings[random.Next(greetings.Length)];
    }

    public void OnAddButton()
    {
        IsDynamicButtonVisible = true;
    }

    public void OnRemoveButton()
    {
        IsDynamicButtonVisible = false;
    }
}
