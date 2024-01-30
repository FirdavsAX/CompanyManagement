using CompanyManagement.Data;
using CompanyManagement.Data.DataStores;
using CompanyManagement.Views;
using MvvmHelpers;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CompanyManagement.ViewModels
{
    public  class MainWindowViewModel:BaseViewModel
    {
        #region DataStores 
        private readonly EmployeeDataStore employeeDataStore ;
        private readonly DepartmentDataStore departmentDataStore;
        #endregion


        #region ICommmands
        public ICommand AddEmployee { get; private set; }
        public ICommand EditEmployee { get; private set; }
        public ICommand DeleteEmployee { get; private set; }
        public ICommand SearchEmployeeText { get; private set; }

        #endregion

        #region UI elements
        public ObservableCollection<emp> Employees { get; private set; }
        public ObservableCollection<dept> Departments{ get; private set; }

        private emp _selectedEmployeeDelete;
        public emp SelectedEmployee
        {
            get => _selectedEmployeeDelete;
            set => SetProperty(ref _selectedEmployeeDelete, value);
        }
        private int _departmentsDeptno=0;
        public int DepartmentsDeptno
        {
            get => _departmentsDeptno;
            set
            {
                SetProperty(ref _departmentsDeptno, value);
                var selectedDepartment = Departments[value];
                FilterEmployeeByDeptno(selectedDepartment.deptno);
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
        public MainWindowViewModel()
        {
            employeeDataStore = new EmployeeDataStore();
            departmentDataStore = new DepartmentDataStore();

            CreateAllElements();
            CreateAllCommands();
            LoadData();

        }
        public void LoadData()
        {
            var employeesLoad = employeeDataStore.GetEmployees();
            var departments = departmentDataStore.GetDepartment();
           
            Employees.AddRange(employeesLoad);
            Departments.AddRange(departments);
        }
        public void FilterEmployeeByDeptno(int id)
        {
            var employees = id == 0 ?
                employeeDataStore.GetEmployees()
                :employeeDataStore.GetEmployees(id);

            Employees.Clear();
            Employees.AddRange(employees);
        }
        public void CreateAllElements()
        {
            Departments = new ObservableCollection<dept>();
            Employees = new ObservableCollection<emp>();
        }
        public void CreateAllCommands()
        {
            DeleteEmployee = new DelegateCommand(OnDeleteEmployee);
            SearchEmployeeText = new DelegateCommand(OnSearchEmployee);
            AddEmployee = new DelegateCommand(OnAddEmployee);
            EditEmployee = new DelegateCommand(OnEditEmployee);
        }
        public void OnAddEmployee()
        {
            try
            {
                var addEmployeeModal = new AddOrEditEmployee(null);
                addEmployeeModal.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void OnEditEmployee()
        {
            var addEmployeeModal = new AddOrEditEmployee(SelectedEmployee);
            addEmployeeModal.ShowDialog();
        }
        public void OnSearchEmployee()
        {
            Employees.Clear();
            var newEmployees = employeeDataStore.GetEmployees(SearchText);
            Employees.AddRange(newEmployees);
        }

        public void OnDeleteEmployee()
        {
            try
            {
                if (SelectedEmployee is null)
                {
                    MessageBox.Show(
                        "Please, select employee to delete.",
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);

                    return;
                }

                var result = MessageBox.Show($"You want to delete {SelectedEmployee.ename}?","Confirm action",MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    employeeDataStore.DeleteEmployee(SelectedEmployee);
                    Employees.Remove(SelectedEmployee);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        
    }
}
