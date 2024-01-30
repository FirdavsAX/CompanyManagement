using CompanyManagement.Data;
using CompanyManagement.Data.DataStores;
using MvvmHelpers;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CompanyManagement.ViewModels
{
    public class AddOrEditViewModel:BaseViewModel
    {
        #region other
        public readonly EmployeeDataStore employeeDataStore;
        public readonly DepartmentDataStore departmentDataStore;


        private bool _addOrEdit;
        public bool AddOrEdit
        {
            get => _addOrEdit;
            set
            {
                SetProperty(ref _addOrEdit, value);
            }
        }

        private emp _employee;
        public emp Employee
        {
            get => _employee;
            set
            {
                SetProperty(ref _employee, value);
            }
        }
        public List<emp> Employees { get; set; }
        public List<dept> Departments { get; set; }

        public List<string> Jobs { get; private set; }
        #endregion


        #region UI elements
        private string _empnoAdd;
        public string EmpnoAdd
        {
            get => _empnoAdd;
            set
            {
                SetProperty(ref _empnoAdd, value);
            }
        }
        private string _ename;
        public string Ename
        {
            get => _ename;
            set
            {
                SetProperty(ref _ename, value);
            } 
        }
        private DateTime _hiredate;
        public DateTime HireDate 
        { 
            get=>_hiredate;
            set
            {
                SetProperty(ref _hiredate, value);
            } 
        }
        private string _salary;
        public string Salary
        {
            get => _salary;
            set
            {
                SetProperty(ref _salary, value);
            }
        }
        private string _comisson;
        public string Comission 
        {
            get => _comisson;
            set
            {
                SetProperty(ref _comisson, value);
            }
        }
        
        private string _selectedJob;
        public string SelectedJob
        {
            get => _selectedJob;
            set
            {
                SetProperty(ref _selectedJob, value);
            }
        }
        private int _selectedManager;
        public int SelectedManager
        {
            get => _selectedManager;
            set
            {
                SetProperty(ref _selectedManager, value);
            }
        }
        private int _selectedDepartment;
        public int SelectedDepartment
        {
            get => _selectedDepartment;
            set
            {
                SetProperty(ref _selectedDepartment, value);
            }
        }

        #endregion

        #region Commands
        public ICommand AddOrEditEmployeeCommand{ get; set; }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="editEmployee"></param>
        public AddOrEditViewModel(emp editEmployee)
        {
            AddOrEdit = editEmployee is null;
            Employee = editEmployee ?? new emp();
            

            employeeDataStore = new EmployeeDataStore();
            departmentDataStore = new DepartmentDataStore();

            LoadData();

            AddOrEditEmployeeCommand = new DelegateCommand(OnAddOrEdit);
            
        }
        public void LoadData()
        {
            Employees = employeeDataStore
                .GetEmployees();
            Departments = departmentDataStore
                .GetDepartment();
            Jobs = Employees
                .Select(x => x.job)
                .Distinct()
                .ToList();

            if (AddOrEdit)
            {
                return;
            }
            EmpnoAdd = Employee.empno.ToString();
            Ename = Employee.ename;
            SelectedJob = Employee.job;
            SelectedManager =  (int)Employee.mgr;
            HireDate = (DateTime)Employee.hiredate;
            Salary = Employee.sal.ToString();
            Comission = Employee.comm.ToString();
            SelectedDepartment = Employee.deptno;

        }
        public void OnAdd()
        {
            try
            {
                Employee.empno = int.Parse(EmpnoAdd);
                Employee.ename = Ename;
                Employee.job = SelectedJob;
                Employee.hiredate = HireDate;
                Employee.mgr = SelectedManager;
                Employee.sal = decimal.Parse(Salary);
                Employee.comm = decimal.Parse(Comission);
                Employee.deptno = SelectedDepartment;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void OnEdit()
        {
            try
            {
                Employee.ename = Ename;
                Employee.job = SelectedJob;
                Employee.hiredate = HireDate;
                Employee.mgr = SelectedManager;
                Employee.sal = decimal.Parse(Salary);
                Employee.comm = decimal.Parse(Comission);
                Employee.deptno = SelectedDepartment;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void OnAddOrEdit()
        {
            try
            {

                if (AddOrEdit)
                {
                    OnAdd();
                    employeeDataStore.AddOrEdit(Employee);
                    MessageBox.Show($"Employee {Employee.ename} is succesfull created");
                }
                else
                {
                    OnEdit();
                    employeeDataStore.AddOrEdit(Employee);
                    MessageBox.Show($"Employee {Employee.ename} is succesfully edited");
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }  

    }
}
