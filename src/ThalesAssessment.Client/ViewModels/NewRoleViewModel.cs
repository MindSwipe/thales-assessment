using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Windows;
using ThalesAssessment.Client.Services;

namespace ThalesAssessment.Client.ViewModels
{
    public class NewRoleViewModel : BaseViewModel
    {
        private readonly ApiService _apiService;

        public string Name { get; set; } = string.Empty;

        public AsyncRelayCommand<Window> CreateNewRoleCommand { get; set; }

        public NewRoleViewModel()
        {
            _apiService = App.Instance.ServiceProvider.GetRequiredService<ApiService>();

            CreateNewRoleCommand = new AsyncRelayCommand<Window>(CreateNewRole);
        }

        private async Task CreateNewRole(Window window)
        {
            await _apiService.CreateNewRole(Name);
            window.Close();
        }
    }
}
