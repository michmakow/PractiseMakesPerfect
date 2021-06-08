using System;
using System.Windows;
using System.Windows.Controls;

namespace SimpleCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double lastNumber, result;
        private SelectedOperator selectedOperator;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultLabel.Content.ToString(), out lastNumber))
            {
                ResultLabel.Content = "0";
            }

            selectedOperator = (sender as Button).Content.ToString() switch
            {
                "+" => SelectedOperator.Addition,
                "-" => SelectedOperator.Subtraction,
                "*" => SelectedOperator.Multiplication,
                "/" => SelectedOperator.Division,
                _ => selectedOperator
            };
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedValue = int.Parse((sender as Button).Content.ToString());

            ResultLabel.Content = ResultLabel.Content.ToString().Equals("0")
                ? $"{selectedValue}"
                : $"{ResultLabel.Content}{selectedValue}";
        }

        private void AcButton_OnClick(object sender, RoutedEventArgs e)
        {
            ResultLabel.Content = "0";
            result = 0;
            lastNumber = 0;
        }

        private void PercentageButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultLabel.Content.ToString(), out var tempNumber))
            {
                tempNumber = tempNumber / 100;
                if (lastNumber != 0) tempNumber *= lastNumber;
                
                ResultLabel.Content = tempNumber.ToString();
            }
        }

        private void NegativeButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultLabel.Content.ToString(), out lastNumber))
            {
                lastNumber *= -1;
                ResultLabel.Content = lastNumber.ToString();
            }
        }

        private void EqualButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultLabel.Content.ToString(), out var newNumber))
            {
                switch (selectedOperator)
                {
                    case SelectedOperator.Addition:
                        result = lastNumber + newNumber;
                        break;
                    case SelectedOperator.Subtraction:
                        result = lastNumber - newNumber;
                        break;
                    case SelectedOperator.Multiplication:
                        result = lastNumber * newNumber;
                        break;
                    case SelectedOperator.Division:
                        if (newNumber == 0)
                        {
                            MessageBox.Show("You can not divide by 0",
                                "Wrong operation",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                            result = 0;
                        }
                        else result = lastNumber / newNumber;
                        
                        break;
                }

                ResultLabel.Content = result.ToString();
            }
        }

        private void PointButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (ResultLabel.Content.ToString().Contains(".")) return;

            ResultLabel.Content = $"{ResultLabel.Content}.";
        }
    }

    public enum SelectedOperator
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }
}