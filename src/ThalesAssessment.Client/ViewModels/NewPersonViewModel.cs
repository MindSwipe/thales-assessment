using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Windows;
using ThalesAssessment.Client.Services;

namespace ThalesAssessment.Client.ViewModels;

public class NewPersonViewModel : BaseViewModel
{
    public string Name { get; set; } = string.Empty;

    public AsyncRelayCommand<Window> CreateNewUserCommand { get; set; }

    private readonly ApiService _apiService;

    public NewPersonViewModel()
    {
        _apiService = App.Instance.ServiceProvider.GetRequiredService<ApiService>();

        CreateNewUserCommand = new AsyncRelayCommand<Window>(CreateNewUser);
    }

    private async Task CreateNewUser(Window? window)
    {
        await _apiService.CreateNewUser(Name);
        window?.Close();
    }
}
