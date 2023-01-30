using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using ThalesAssessment.Client.Popovers;
using ThalesAssessment.Client.Services;
using ThalesAssessment.Entities;
using ThalesAssessment.Client.Views;

namespace ThalesAssessment.Client.ViewModels
{
    public class PersonRoleViewModel : BaseViewModel
    {
        private Person? _selectedPerson;

        private ObservableCollection<Person> _persons;

        private bool _isEnabled;

        private readonly ApiService _apiService;

        public PersonRoleViewModel()
        {
            _apiService = App.Instance.ServiceProvider.GetRequiredService<ApiService>();

            Persons = new ObservableCollection<Person>();
            IsEnabled = true;
            LoadPersonsCommand = new AsyncRelayCommand(LoadPersons);
            AssignNewRoleCommand = new AsyncRelayCommand(AssignNewRole);
            CreateNewRoleCommand = new RelayCommand(CreateNewRole);
            DeleteRoleCommand = new AsyncRelayCommand(DeleteRole);
        }

        public AsyncRelayCommand LoadPersonsCommand { get; set; }

        public AsyncRelayCommand AssignNewRoleCommand { get; set; }

        public RelayCommand CreateNewRoleCommand { get; set; }

        public AsyncRelayCommand DeleteRoleCommand { get; set; }

        public ObservableCollection<Person> Persons
        {
            get => _persons;
            set
            {
                if (value != _persons)
                {
                    _persons = value;
                    OnPropertyChanged();
                }
            }
        }

        public Person? SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                if (_selectedPerson != value)
                {
                    _selectedPerson = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(HasPersonSelected));
                }
            }
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool HasPersonSelected => SelectedPerson != null;

        private async Task LoadPersons()
        {
            var selectedPersonId = SelectedPerson?.Id;

            var persons = await _apiService.GetAllPersons();
            if (persons == null)
                return;

            Persons = new ObservableCollection<Person>(persons);

            if (selectedPersonId != null)
                SelectedPerson = Persons.FirstOrDefault(x => x.Id == selectedPersonId);
        }

        private async Task AssignNewRole()
        {
            if (!HasPersonSelected)
                return;

            var roles = await _apiService.GetAllRoles();

            if (roles == null)
                return;

            var selectItems = roles.Select(x => SelectItem.FromObject(x, y => y.Name)).ToList();

            IsEnabled = false;

            new SelectPopover(
                selectItems,
                "Select role to assign",
                async selectedItem =>
                {
                    if (selectedItem == null || !(selectedItem.Item is Role role))
                        return;

                    // Null Forgiving: Since 'HasPersonSelected' is true, 'SelectedPerson' is not null
                    await _apiService.AssignRoleToPerson(SelectedPerson!, role);
                    await LoadPersons();
                    IsEnabled = true;
                }
            )
            .Show();
        }

        private void CreateNewRole()
        {
            IsEnabled = false;
            var newRoleWindow = new NewRoleView();
            newRoleWindow.Closed += async (sender, args) =>
            {
                await LoadPersons();
                IsEnabled = true;
            };

            newRoleWindow.Show();
        }

        private async Task DeleteRole()
        {

        }
    }
}
