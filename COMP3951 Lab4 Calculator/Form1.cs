using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// Lab 2: Calculator Application
/// Include here the authors: Jason Peacock and Olivia Grace
/// Include here date/revisions: January 22, 2025
/// </summary>
namespace COMP3951_Lab4_Calculator
{
    /// <summary>
    /// Form1 represents the interface for the calculator application. It contains a Calculator object and
    /// methods for the user to interact with the Calculator.
    /// </summary>
    public partial class Form1 : Form
    {

        /// <summary>
        /// A Calculator object to handle the logic of the calculator application.
        /// </summary>
        Calculator calculator;
        
        /// <summary>
        /// Initializes the components of Form1.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Handles button click events from the form by sending the button text to an interpreter.
        /// Sets the focus back to the form so the enter key can be intercepted and used as equals operator.
        /// </summary>
        /// <param name="sender">The object that initiated the Click event</param>
        /// <param name="e">A Click event</param>
        private void button_Click(object sender, EventArgs e)
        {
            Button buttonClicked;
            String buttonText;

            buttonClicked = (Button)sender;
            buttonText = buttonClicked.Text;

            inputInterpreter(buttonText);

            this.ActiveControl = null;
        }


        /// <summary>
        /// Handles KeyPress events from the form by sending numbers and valid operators to be interpreted.
        /// KeyPress used over KeyDown for numbers and operators since it allows easier identification of inputs
        /// like * through KeyChar
        /// </summary>
        /// <param name="sender">The object that initiated the KeyPress event</param>
        /// <param name="e">The KeyPress event</param>
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char[] validInputs = { '*', '/', '-', '+', '%', '^', 'i', '=' };

            int i;
            if (int.TryParse(e.KeyChar.ToString(), out i) || validInputs.Contains(e.KeyChar))
            {
                inputInterpreter(e.KeyChar.ToString());
            }
        }

        /// <summary>
        /// Handles KeyDown events. Only used for identifying when enter has been pressed and used for equals.
        /// Implemented this way as the enter key is not easily identfied through the KeyPress event.
        /// Calls inputInterpreter() rather than Equals() directly for more consistent behaviour.
        /// </summary>
        /// <param name="sender">The object that initiated the KeyDown event</param>
        /// <param name="e">The KeyDown event</param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                inputInterpreter("=");
            }
        }

        /// <summary>
        /// Takes incoming strings representing numeric or operational inputs and interprets them.
        /// Calls the appropriate method to deal with the given input.
        /// </summary>
        /// <param name="input">A string representing a single user input</param>
        private void inputInterpreter(string input)
        {
            string[] operationInputs = { "*", "/", "-", "+", "%", "^", "i", "1/x", "x^2", "sqrt" };

            int i;
            if (int.TryParse(input, out i) || input.Equals("."))
            {
                handleNumericInput(input);
            }
            else if (operationInputs.Contains(input))
            {
                switch (input)
                {
                    case "^": input = "x^2"; break;
                    case "i": input = "1/x"; break;
                    case "s": input = "sqrt"; break;
                }

                handleOperationInputs(input);
            }
            else
            {
                switch (input)
                {
                    case "+/-": PlusMinus(); break;
                    case "CE": ClearCurrent(); break;
                    case "C": ClearAll(); break;
                    case "<-": Backspace(); break;
                    case "=": Equals(); break;
                    case "MC": MemoryClear(); break;
                    case "MR": MemoryRecall(); break;
                    case "MS": MemoryAssign(); break;
                    case "M+": MemoryAdd(); break;
                    case "On": togglePower(); break;
                    case "Off": togglePower(); break;
                    default: MessageBox.Show("INVALID INPUT SPECIFIED"); break;
                }
            }
        }

        /// <summary>
        /// Takes numeral string inputs as well as decimal and updates the calculators text box
        /// Logic exists to ensure overwriting a single zero, overwriting existing operation results,
        /// and dealing with decimal input edge cases all operate correctly.
        /// </summary>
        /// <param name="input">A string consisting of one numeral digit or a decimal</param>
        private void handleNumericInput(string input)
        {
            string currentBoxText = textBoxCalculation.Text;

            // set display to nothing in anticipation of a replacement if current is just 0 and incoming isn't a decimal
            if (currentBoxText.Equals("0") && input != ".")
            {
                currentBoxText = "";
            }

            // allows replacing of number on display rather than appending after an operation with two operands
            if (!calculator.Operation.Equals("") && calculator.JustEnteredOperation == true)
            {
                textBoxCalculation.Text = input;
                calculator.JustEnteredOperation = false;
            }
            // ignores duplicate decimal point input
            else if (currentBoxText.Contains(".") && input.Equals("."))
            {
                // do nothing
            }
            // otherwise concatenate new digit
            else
            {
                textBoxCalculation.Text = String.Concat(currentBoxText, input);
            }
        }

        /// <summary>
        /// Set operand1 of the Calculation object to be the value in the textbox
        /// if operand1, operand2, oepration are set then set operand1 to be their result, else set operand1 to be
        /// the value in the textbox. Edge cases for the unary operators 1/x, x^2, and sqrt
        /// </summary>
        /// <param name="input">A string containing a validated operation identifier</param>
        private void handleOperationInputs(string input)
        {
            String currentBoxText = textBoxCalculation.Text;

            double? result;
            double textBoxNumber = Double.Parse(textBoxCalculation.Text);

            calculator.setOperand(textBoxNumber);

            result = calculator.setOperation(input);

            if (result != null)
            {
                textBoxCalculation.Text = result.ToString();
            }
            if (input != "1/x" && input != "x^2" && input != "sqrt")
            {
                calculator.JustEnteredOperation = true;
            }
        }

        /// <summary>
        /// Sets the value in textbox to be opposite parity.
        /// </summary>
        private void PlusMinus()
        {
            String currentBoxText = textBoxCalculation.Text;

            if (currentBoxText[0].Equals('-'))
            {
                textBoxCalculation.Text = currentBoxText.Remove(0, 1);
            }
            else
            {
                textBoxCalculation.Text = String.Concat("-", currentBoxText);
            }
        }

        /// <summary>
        /// Sets the calculator's memory to 0.
        /// </summary>
        private void MemoryClear()
        {
            calculator.Memory = 0;
        }

        /// <summary>
        /// Sets the textbox to be the value in the calculator's memory
        /// </summary>
        private void MemoryRecall()
        {
            textBoxCalculation.Text = calculator.Memory.ToString();
        }

        /// <summary>
        /// Assigns the value in the textbox to the calculator's memory.
        /// </summary>
        private void MemoryAssign()
        {
            double textBoxNumber = Double.Parse(textBoxCalculation.Text);

             calculator.Memory = textBoxNumber;
        }

        /// <summary>
        /// Adds the value in the textbox to the calculator's memory.
        /// </summary>
        private void MemoryAdd()
        {
            double textBoxNumber = Double.Parse(textBoxCalculation.Text);

            calculator.Memory += textBoxNumber;
        }

        /// <summary>
        /// Clears the current operand from the calculator.
        /// </summary>
        private void ClearCurrent()
        {
            calculator.clearCurrent();
            textBoxCalculation.Text = "0";
        }

        /// <summary>
        /// Clears operand1, operand2, and the operator in the calculator object, leaves memory.
        /// </summary>
        private void ClearAll()
        {
            calculator.clearAll();
            textBoxCalculation.Text = "0";
        }

        /// <summary>
        /// Removes last digit from textbox. if textbox text length is one instead replaces with zero.
        /// </summary>
        private void Backspace()
        {
            String currentBoxText = textBoxCalculation.Text;
            String updatedBoxText;

            if(currentBoxText.Length == 1)
            {
                updatedBoxText = "0";
            }
            else
            {
                updatedBoxText = currentBoxText.Remove(currentBoxText.Length - 1, 1);
            }

            textBoxCalculation.Text = updatedBoxText;
        }

        /// <summary>
        /// Calls the calculator's performCalculation function and displays the result in the textbox.
        /// </summary>
        private void Equals()
        {
            double textBoxNumber = Double.Parse(textBoxCalculation.Text);
            double? result;

            calculator.setOperand(textBoxNumber);

            result = calculator.performCalculation();

            if (result != null)
            {
                textBoxCalculation.Text = result.ToString();
                calculator.JustEnteredOperation = true;
            }
        }

        /// <summary>
        /// Turns the calculator on or off. When turning the calculator on, all buttons are enabled and 
        /// a pictures is loaded at the bottom of the screen. When turning the calculator off, all buttons
        /// except the On/Off button are disabled and the picture is removed from the bottom of the screen.
        /// </summary>
        private void togglePower()
        {
            calculator.clearAll();

            buttonOnOff.Text = buttonOnOff.Text.Equals("On") ? "Off" : "On";

            textBoxCalculation.Text = "0";

            textBoxCalculation.Enabled = !textBoxCalculation.Enabled;

            button0.Enabled = !button0.Enabled;
            button1.Enabled = !button1.Enabled;
            button2.Enabled = !button2.Enabled;
            button3.Enabled = !button3.Enabled;
            button4.Enabled = !button4.Enabled;
            button5.Enabled = !button5.Enabled;
            button6.Enabled = !button6.Enabled;
            button7.Enabled = !button7.Enabled;
            button8.Enabled = !button8.Enabled;
            button9.Enabled = !button9.Enabled;
            buttonDecimal.Enabled = !buttonDecimal.Enabled;
            buttonPlusMinus.Enabled = !buttonPlusMinus.Enabled;

            buttonDivide.Enabled = !buttonDivide.Enabled;
            buttonMultiply.Enabled = !buttonMultiply.Enabled;
            buttonSubtract.Enabled = !buttonSubtract.Enabled;
            buttonAdd.Enabled = !buttonAdd.Enabled;
            buttonSquareRoot.Enabled = !buttonSquareRoot.Enabled;
            buttonPercent.Enabled = !buttonPercent.Enabled;
            buttonInverse.Enabled = !buttonInverse.Enabled;
            buttonSquare.Enabled = !buttonSquare.Enabled;

            buttonClearCurrent.Enabled = !buttonClearCurrent.Enabled;
            buttonClearAll.Enabled = !buttonClearAll.Enabled;
            buttonBackspace.Enabled = !buttonBackspace.Enabled;
            buttonEquals.Enabled = !buttonEquals.Enabled;

            buttonMemoryClear.Enabled = !buttonMemoryClear.Enabled;
            buttonMemoryRecall.Enabled = !buttonMemoryRecall.Enabled;
            buttonMemoryAssign.Enabled = !buttonMemoryAssign.Enabled;
            buttonMemoryAdd.Enabled = !buttonMemoryAdd.Enabled;

            pictureBoxImage.Visible = !pictureBoxImage.Visible;
        }

        /// <summary>
        /// Creates a new calculation object.
        /// </summary>
        /// <param name="sender">the form</param>
        /// <param name="e">the load event</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            calculator = new Calculator();
        }
    }
}
