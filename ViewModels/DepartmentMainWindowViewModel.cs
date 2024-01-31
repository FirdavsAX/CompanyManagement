using CompanyManagement.Data;
using CompanyManagement.Data.DataStores;
using MvvmHelpers;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace CompanyManagement.ViewModels
{
    public class DepartmentMainWindowViewModel : BaseViewModel
    {
        #region DataStores 
        private readonly DepartmentDataStore departmentDataStore;
        #endregion
        private string _tekshir = "TEkshirildi";
        public string Tekshir { 
            get => _tekshir;
            set => SetProperty(ref _tekshir, value);
        } 

        #region ICommmands
        public ICommand AddDepartment { get; private set; }
        public ICommand EditDepartment { get; private set; }
        public ICommand DeleteDepartment { get; private set; }
        public ICommand SearchDepartmentText { get; private set; }

        #endregion

        #region UI elements
        public ObservableCollection<dept> Departments { get; private set; }

        private dept _selectedDepartment;
        public dept SelectedDepartment
        {
            get => _selectedDepartment;
            set => SetProperty(ref _selectedDepartment, value);
        }
        private string _locations;
        public string Location
        {
            get => _locations;
            set
            {
                SetProperty(ref _locations, value);
                
                FilterEmployeeByDeptno(value);
            }
        }
        private string _seaerchText;
        public string SearchText
        {
            get => _seaerchText;
            set => SetProperty(ref _seaerchText, value);
        }
        #endregion

        #region others
        #endregion
        public DepartmentMainWindowViewModel()
        {
            departmentDataStore = new DepartmentDataStore();

            CreateAllElements();
            CreateAllCommands();
            LoadData();

        }
        public void LoadData()
        {
            var departments = departmentDataStore.GetDepartment();

            Departments.AddRange(departments);
        }
        public void FilterEmployeeByDeptno(string locationDepartment)
        {
            if(locationDepartment is null)
            {
                return;
            }
            
            Departments.Clear();
            Departments = (ObservableCollection<dept>)Departments
                .Where(x => x.location == locationDepartment);
        }
        public void CreateAllElements()
        {
            Departments = new ObservableCollection<dept>();
        }
        public void CreateAllCommands()
        {
            DeleteDepartment = new DelegateCommand(OnDeleteDepartment);
            SearchDepartmentText = new DelegateCommand(OnSearchDepartment);
            AddDepartment= new DelegateCommand(OnAddDepartment);
            EditDepartment= new DelegateCommand(OnEditDepartment);
        }
        public void OnAddDepartment()
        {
            try
            {/////////////
                //var addEmployeeModal = new AddOrEditDepartment(null);
                //addEmployeeModal.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void OnEditDepartment()
        {////////////
            //var addEmployeeModal = new AddOrEditDepartment(SelectedDepartment);
            //addEmployeeModal.ShowDialog();
        }
        public void OnSearchDepartment()
        {
            try
            {
                Departments.Clear();
                var list = departmentDataStore.GetDepartment(SearchText);
                Departments.AddRange(list);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void OnDeleteDepartment()
        {
            try
            {
                if (SelectedDepartment is null)
                {
                    MessageBox.Show(
                        "Please, select department to delete.",
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);

                    return;
                }

                var result = MessageBox.Show($"You want to delete {SelectedDepartment.dname}?", "Confirm action", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    departmentDataStore.DeleteDepartment(SelectedDepartment);
                    Departments.Remove(SelectedDepartment);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
