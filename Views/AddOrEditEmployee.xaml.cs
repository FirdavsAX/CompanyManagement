using CompanyManagement.Data;
using CompanyManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CompanyManagement.Views
{
    /// <summary>
    /// Interaction logic for AddOrEditEmployee.xaml
    /// </summary>
    public partial class AddOrEditEmployee : Window
    {
        public ObservableCollection<object> _children;
        
        public ObservableCollection<object> Children
          { get { return _children; } }
        public AddOrEditEmployee(emp editEmployee)
        {
            InitializeComponent();
            _children = new ObservableCollection<object>();
            //_children.Add(new Tab1ViewModel());
            //_children.Add(new Tab2ViewModel());

            DataContext = new AddOrEditViewModel(editEmployee);
        }
    }
}
