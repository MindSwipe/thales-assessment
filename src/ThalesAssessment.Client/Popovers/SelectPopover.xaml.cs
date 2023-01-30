using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ThalesAssessment.Client.Popovers
{
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
            
            InitializeComponent();
        }

        private async Task Select()
        {
            if (_callback!= null)
                await _callback.Invoke(SelectedItem);

            Close();
        }
    }
}
