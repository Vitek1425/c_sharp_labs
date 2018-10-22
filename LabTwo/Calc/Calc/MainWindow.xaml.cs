using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace FirstWpfApp
{
    public partial class MainWindow : Window
    {
        private bool _lastLetterIsNumber = false;
        private string _expression = "";
        private List<string> _evaluatedExpressions = new List<string>();

        public MainWindow()
        {
            InitializeComponent();

            foreach (UIElement c in LayoutRoot.Children)
            {
                if (c is Button)
                {
                    ((Button)c).Click += OnButtonClick;
                }
            }

            this.KeyUp += new System.Windows.Input.KeyEventHandler(OnKeyUp);
        }
        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            string buttonText = (string)((Button)e.OriginalSource).Content;

            if (_lastLetterIsNumber && buttonText == "=")
            {
                OnEvaluate();
            }
            else if (IsCommand(buttonText))
            {
                if (_lastLetterIsNumber)
                {
                    _expression += buttonText;
                    _lastLetterIsNumber = false;
                }
            }
            else
            {
                if (Int32.TryParse(buttonText, out int number))
                {
                    _expression += buttonText;
                    _lastLetterIsNumber = true;
                }
            }
            UpdateTextbox();
        }
        private void OnKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Back)
            {
                if (_expression.Length == 0)
                    return;
                _expression = _expression.Remove(_expression.Length - 1);
                if (_expression.Length != 0)
                {
                    if (IsCommand(_expression.Substring(_expression.Length - 1)))
                        _lastLetterIsNumber = false;
                    else
                        _lastLetterIsNumber = true;
                }
                else
                {
                    _lastLetterIsNumber = false;
                }

                UpdateTextbox();
                return;
            }

            Button button = GetButtonByInputKey(e.Key);
            if (button != null)
                button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        private bool IsCommand(string text)
        {
            switch (text)
            {
                case "+":
                case "-":
                case "/":
                case "*":
                case "=":
                    return true;
            }
            return false;
        }

        Button GetButtonByInputKey(System.Windows.Input.Key key)
        {
            switch (key)
            {
                case System.Windows.Input.Key.D0:
                    return ButtonZero;
                case System.Windows.Input.Key.D1:
                    return ButtonOne;
                case System.Windows.Input.Key.D2:
                    return ButtonTwo;
                case System.Windows.Input.Key.D3:
                    return ButtonThree;
                case System.Windows.Input.Key.D4:
                    return ButtonFour;
                case System.Windows.Input.Key.D5:
                    return ButtonFive;
                case System.Windows.Input.Key.D6:
                    return ButtonSix;
                case System.Windows.Input.Key.D7:
                    return ButtonSeven;
                case System.Windows.Input.Key.D8:
                    return ButtonEigth;
                case System.Windows.Input.Key.D9:
                    return ButtonNine;
                case System.Windows.Input.Key.OemPlus:
                case System.Windows.Input.Key.Add:
                    return ButtonAdd;
                case System.Windows.Input.Key.OemMinus:
                case System.Windows.Input.Key.Subtract:
                    return ButtonSub;
                case System.Windows.Input.Key.Multiply:
                    return ButtonMult;
                case System.Windows.Input.Key.Divide:
                    return ButtonDiv;
                case System.Windows.Input.Key.Enter:
                    return ButtonEvaluate;
            }
            return null;
        }

        private void OnEvaluate()
        {
            string result = Calc.MathEvaluator.Evaluate(_expression);
            if (result?.Length > 0)
            {
                _expression = "";
                _evaluatedExpressions.Add(result);
            }
            UpdateTextbox();
        }

        private void UpdateTextbox()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string evalutedExpression in _evaluatedExpressions)
            {
                sb.Append(evalutedExpression);
                sb.Append("\n");
            }
            sb.Append(_expression);

            textBlock.Text = sb.ToString();
        }
    }
}