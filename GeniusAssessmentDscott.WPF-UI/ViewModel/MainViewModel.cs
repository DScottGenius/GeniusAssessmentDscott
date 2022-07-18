using GeniusAssessmentDscott.Data.Entities;
using GeniusAssessmentDscott.WPF_UI.Data;
using GeniusAssessmentDscott.WPF_UI.WPF_Commands;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GeniusAssessmentDscott.WPF_UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<User> Users { get; private set; }
        public ObservableCollection<Payment> Payments { get; private set; }
        private IDataService DataService;
        private bool userChecked;
        public bool UserChecked
        {
            get { return userChecked; }
            set
            {
                userChecked = value;
                OnPropertyChanged(nameof(PaymentChecked));
            }
        }

        private bool paymentChecked;

        public bool PaymentChecked
        {
            get { return paymentChecked; }
            set
            {
                paymentChecked = value;
                OnPropertyChanged(nameof(UserChecked));
            }
        }

        private string user;
        public string UsersText
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged();
            }
        }

        private string filePath;
        public string FilePath
        {
            get { return filePath; }
            set
            {
                filePath = value;
                OnPropertyChanged();
            }
        }

        private string output;

        public string Output
        {
            get { return output; }
            set
            {
                output = value;
                OnPropertyChanged();
            }
        }


        //Buttons
        public ICommand SubmitButtonClicked { get; set; }
        public ICommand BrowseButtonClicked { get; set; }

        //Radio Buttons
        public ICommand UserCheckedCommand { get; set; }
        public ICommand PaymentCheckedCommand { get; set; }

        public MainViewModel(IDataService dataServiceIn)
        {
            DataService = dataServiceIn;
            Users = new ObservableCollection<User>();
            Payments = new ObservableCollection<Payment>();
            FilePath = "";

            SubmitButtonClicked = new RelayCommand(ProcessData, canSubmit);
            BrowseButtonClicked = new RelayCommand(BrowseFile, canBrowse);
            
            UserCheckedCommand = new RelayCommand(userBoxChecked, canCheck);
            PaymentCheckedCommand = new RelayCommand(paymentBoxChecked, canCheck);

            userChecked = true;
            paymentChecked = false;
        }

        public void Load()
        {

        }

        public void BrowseFile(object value)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
            }
        }

        public void ProcessData(object value)
        {
            if (userChecked)
            {
                ProcessUser();
            }
            else if (paymentChecked)
            {
                ProcessPayment();
            }
        }

        private void ProcessPayment()
        {
            var payments = DataService.ProcessPayment(FilePath);

            Payments.Clear();
            Output = "";

            if (payments == null)
            {
                Output = "No payments found in the file you chose. Please check the file you are trying to use.";
                return;
            }

            foreach (var p in payments)
            {
                Payments.Add(p);
            }

            Output = "File read succesfully!\n\nPayments present in the database\n\n";
            foreach (Payment p in Payments)
            {
                Output += $"{p.ToString()}\n";
            }
            Users.Clear();
            var userPayments = DataService.ProcessUserPayments();

            Output += "\nUsers and their associated payments (if any)\n";
            foreach (User u in userPayments)
            {
                Output += $"\nAccount: {u.AccountNumber} / {u.AccountName}";
                if (u.PaymentsMade.Count > 0)
                {
                    foreach (Payment p in u.PaymentsMade)
                    {
                        Output += $"\n{p.ToString()}";
                    }
                }
                else
                {
                    Output += "\nNo associated payments found.";
                }
                Output += "\n-------------------";
            }
        }

        private void ProcessUser()
        {
            var users = DataService.ProcessUser(FilePath);

            Users.Clear();
            Output = "";

            if (users == null)
            {
                Output = "No users found in the file you chose. Please check the file you are trying to use.";
                return;
            }

            foreach (var u in users)
            {
                Users.Add(u);
            }

            Output = "File read succesfully!\n\nUsers present in the database\n\n";
            foreach (User u in Users)
            {
                Output += $"{u.ToString()}\n";
            }
        }

        private bool canSubmit(object value)
        {
            if (filePath == null || filePath == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool canBrowse(object value)
        {
            return true;
        }

        private bool canCheck(object value)
        {
            return true;
        }

        private void userBoxChecked(object value)
        {
            PaymentChecked = false;
            UserChecked = true;
        }
        private void paymentBoxChecked(object value)
        {
            UserChecked = false;
            PaymentChecked = true;
        }
    }
}
