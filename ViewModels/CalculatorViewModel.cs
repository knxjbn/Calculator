using Calculator.Commands;
using Calculator.Model;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Calculator.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class CalculatorViewModel : ViewModelBase
    {
        private CalculationModel calculationModel;

        private string resultBox;
        private string lastOperation;
        private bool resetResultBox = false;

        #region Operations
        private DelegateCommand<string> dgOperation;

        public ICommand CommandOperation
        {
            get
            {
                if (dgOperation == null)
                    dgOperation = new DelegateCommand<string>(OperationIsPressed, CanOperationBePressed);
                return dgOperation;
            }
        }

        private static bool CanOperationBePressed(string btn)
        {
            return true;
        }

        public void OperationIsPressed(string operation)
        {
            try
            {
                if (string.IsNullOrEmpty(FirstNumber) || OperationLast == "=")
                {
                    FirstNumber = resultBox;
                    OperationLast = operation;
                }
                else
                {
                    SecondNumber = resultBox;
                    Operation = lastOperation;
                    calculationModel.CalculateResult();
                    OperationLast = operation;
                    ResultBox = Result;
                    FirstNumber = resultBox;
                }
                resetResultBox = true;
            }
            catch (Exception)
            {
                ResultBox = Result == "" ? "Fail" : Result;
            }
        }

        #endregion

        #region Numbers
        private DelegateCommand<string> dgNumber;

        public ICommand CommandNumber
        {
            get
            {
                if (dgNumber == null)
                    dgNumber = new DelegateCommand<string>(NumberIsPressed, CanNumberBePressed);
                return dgNumber;
            }
        }

        private static bool CanNumberBePressed(string btn)
        {
            return true;
        }

        public void NumberIsPressed(string btn)
        {
            if (btn == "C")
            {
                ResultBox = "0";
                FirstNumber = "";
                SecondNumber = "";
                Operation = "";
                OperationLast = "";
            }
            else if (resultBox == "0" || resetResultBox)
                ResultBox = btn;
            else
                ResultBox = resultBox + btn;
            resetResultBox = false;
        }
        #endregion

        #region Publics
        public CalculatorViewModel()
        {
            calculationModel = new CalculationModel();
            resultBox = "0";
            FirstNumber = "";
            SecondNumber = "";
            Operation = "";
            lastOperation = "";
        }

        public string FirstNumber
        {
            get { return calculationModel.FirstNumber; }
            set { calculationModel.FirstNumber = value; }
        }

        public string SecondNumber
        {
            get { return calculationModel.SecondNumber; }
            set { calculationModel.SecondNumber = value; }
        }

        public string Operation
        {
            get { return calculationModel.Operation; }
            set { calculationModel.Operation = value; }
        }

        public string OperationLast
        {
            get { return lastOperation; }
            set { lastOperation = value; }
        }

        public string Result
        {
            get { return calculationModel.Result; }
        }

        public string ResultBox
        {
            get { return resultBox; }
            set
            {
                resultBox = value;
                OnPropertyChanged("ResultBox");
            }
        }
        #endregion
    }
}