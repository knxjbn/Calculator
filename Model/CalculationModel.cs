using System;

namespace Calculator.Model
{
    public class CalculationModel
    {
        private string result;

        public CalculationModel()
        {

        }

        public string FirstNumber { get; set; }
        public string SecondNumber { get; set; }
        public string Operation { get; set; }
        public string Result { get { return result; } }

        public void CalculateResult()
        {
            try
            {
                switch (Operation)
                {
                    case "+":
                        result = (Convert.ToDouble(FirstNumber) + Convert.ToDouble(SecondNumber)).ToString();
                        break;
                    case "-":
                        result = (Convert.ToDouble(FirstNumber) - Convert.ToDouble(SecondNumber)).ToString();
                        break;
                    case "*":
                        result = (Convert.ToDouble(FirstNumber) * Convert.ToDouble(SecondNumber)).ToString();
                        break;
                    case "/":
                        result = (Convert.ToDouble(FirstNumber) / Convert.ToDouble(SecondNumber)).ToString();
                        break;
                }
            }
            catch (Exception)
            {
                result = "Fail";
            }
        }
    }
}
