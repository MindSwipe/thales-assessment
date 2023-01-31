using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace ThalesAssessment.Client.Popovers;

/// <summary>
/// Interaction logic for SelectPopover.xaml
/// </summary>
public partial class SelectPopover : Window
{
    public List<SelectItem> Items { get; set; }

    public string SelectLabelText { get; set; }

    public SelectItem? SelectedItem { get; set; }

    public AsyncRelayCommand SelectCommand { get; set; }

    private readonly Func<SelectItem?, Task>? _callback;

    public SelectPopover(List<SelectItem> items, string selectLabelText, Func<SelectItem?, Task>? callback = null)
    {
        Items = items;
        SelectLabelText = selectLabelText;
        SelectCommand = new AsyncRelayCommand(Select);
        _callback = callback;

        Closed += OnClosed;
        
        InitializeComponent();
    }

    private async Task Select()
    {
        if (_callback!= null)
            await _callback.Invoke(SelectedItem);

        Close();
    }

    private void OnClosed(object? sender, EventArgs e)
    {
        _callback?.Invoke(null);
    }
}
