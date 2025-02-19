using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using COMP3951_Lab4_Calculator;
using System.Reflection;
using System.Windows.Forms;

namespace UnitTestProjectCalculator
{
    /// <summary>
    /// Lab 4: Unit test class for testing the Calculator functionality.
    /// The Calculator this unit test is testing is developed by Jason Peacock and Olivia Grace.
    /// 
    /// Authors: Jason Peacock, Tanner Parkes, Caleb Janzen, Phyo Thu Kha
    /// Date:    February 19, 2025
    /// Version: 1.0
    /// </summary>
    [TestClass]
    public class UnitTestCalculator
    {
        private Calculator calculator;
        private Form1 form;
        private TextBox textBoxCalculation; 

        /// <summary>
        /// Setup method to initialize necessary objects before each test method.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            calculator = new Calculator();
            form       = new Form1();

            // Reflection to access the private 'textBoxCalculation' field from Form1.
            FieldInfo textBoxField = typeof(Form1).GetField("textBoxCalculation", BindingFlags.NonPublic | BindingFlags.Instance);
            textBoxCalculation     = (TextBox)textBoxField.GetValue(form);
        }

        /// <summary>
        /// Tests the square root unary operation.
        /// </summary>
        [TestMethod]
        public void TestUnaryOperation_SquareRoot()
        {
            // Arrange
            double operand;
            double? result;
            string operation;

            operand   = 9;
            operation = "sqrt";

            // Act
            calculator.setOperand(operand);
            result = calculator.setOperation(operation);

            // Assert
            Assert.AreEqual(3, result); 
        }

        /// <summary>
        /// Tests the addition binary operation.
        /// </summary>
        [TestMethod]
        public void TestBinaryOperation_Addition()
        {
            // Arrange 
            double operand1;
            double operand2;
            double? result;
            string operation;

            operand1  = 235.223;
            operand2  = 152.156;
            operation = "+";

            // Act
            calculator.setOperand(operand1);
            calculator.setOperation(operation);
            calculator.setOperand(operand2);
            result = calculator.performCalculation();

            // Assert 
            Assert.AreEqual(387.379, result);
        }

        /// <summary>
        /// Tests division by zero and the result handling.
        /// </summary>
        [TestMethod]
        public void TestDivisonByZero()
        {
            // Arrange 
            double operand1;
            double operand2;
            double? result;
            string operation;

            operand1  = 12;
            operand2  = 0;
            operation = "/";

            // Act
            calculator.setOperand(operand1);
            calculator.setOperation(operation);
            calculator.setOperand(operand2);
            result = calculator.performCalculation();

            // Assert
            Assert.AreEqual(double.PositiveInfinity, result);
        }

        /// <summary>
        /// Tests the clear all functionality of the calculator.
        /// </summary>
        [TestMethod]
        public void TestClearAll()
        {
            // Arrange
            double operand1;
            double operand2;
            string operation; 

            operand1  = 100;
            operand2  = 200;
            operation = "*";

            // Act
            calculator.setOperand(operand1);
            calculator.setOperation(operation);
            calculator.setOperand(operand2);
            calculator.clearAll();

            // Assert
            Assert.IsNull(calculator.performCalculation());
            Assert.AreEqual("", calculator.Operation);
        }

        /// <summary>
        /// Tests handling of single digit numeric input.
        /// </summary>
        [TestMethod]
        public void TestHandleNumericInput_SingleDigit()
        {
            // Arrange
            // Reflection to access the private method "handleNumericInput" from Form. 
            MethodInfo handleNumericInput = typeof(Form1).GetMethod("handleNumericInput", BindingFlags.NonPublic | BindingFlags.Instance); 

            form.Show(); // Ensure that form is initialzed

            // Act 
            handleNumericInput.Invoke(form, new object[] { "12" }); // Invoke the private method with the digit "12"

            // Assert 
            Assert.AreEqual("12", textBoxCalculation.Text);
        }

        /// <summary>
        /// Test handling multiple digits of numeric input.
        /// </summary>
        [TestMethod]
        public void TestHandleNumericInput_MultipleDigits()
        {
            // Arrange 
            // Reflection to access the private method "handleNumericInput" from Form. 
            MethodInfo handleNumberInput = typeof(Form1).GetMethod("handleNumericInput", BindingFlags.NonPublic | BindingFlags.Instance);

            form.Show();  // Ensure that form is initialized

            //Act
            handleNumberInput.Invoke(form, new object[] { "9" }); // Invoke the private method with the digit "9"
            handleNumberInput.Invoke(form, new object[] { "5" }); // Invoke the private method with the digit "5"
            handleNumberInput.Invoke(form, new object[] { "3" }); // Invoke the private method with the digit "3"

            // Assert 
            Assert.AreEqual("953", textBoxCalculation.Text);
        }

        /// <summary>
        /// Tests the plus-minus functionality, where a positive number becomes negative. 
        /// </summary>
        [TestMethod]
        public void TestPlusMinus_PositiveNumber_BecomesNegative()
        {
            // Arrange
            // Reflection to access the private method "handleNumericInput" from Form. 
            MethodInfo handleNumericInput = typeof(Form1).GetMethod("handleNumericInput", BindingFlags.NonPublic | BindingFlags.Instance);

            // Reflection to access the private method "PlusMinus" from Form. 
            MethodInfo plusMinus = typeof(Form1).GetMethod("PlusMinus", BindingFlags.NonPublic | BindingFlags.Instance);

            form.Show(); // Ensure the form is initialized

            // Act
            handleNumericInput.Invoke(form, new object[] { "99" }); // Invoke the private method with the digit "99"
            plusMinus.Invoke(form, null); // Invoke the private method with null as its parameter

            // Assert
            Assert.AreEqual("-99", textBoxCalculation.Text);
        }

        /// <summary>
        /// Tests the clear all function rests the calculation to zero.
        /// </summary>
        [TestMethod]
        public void TestClearAll_ResetToZero()
        {
            // Arrange
            // Reflection to access the private method "handleNumericInput" from Form. 
            MethodInfo handNumericInput = typeof(Form1).GetMethod("handleNumericInput", BindingFlags.NonPublic | BindingFlags.Instance);

            // Reflection to access the private method "ClearAll" from Form. 
            MethodInfo clearAll = typeof(Form1).GetMethod("ClearAll", BindingFlags.NonPublic | BindingFlags.Instance);

            form.Show(); // Ensure the form is initialized

            // Act
            handNumericInput.Invoke(form, new object[] { "123" }); // Invoke the private method with the digit "123"
            clearAll.Invoke(form, null); // Invoke the private method with null as its parameter
            
            // Assert 
            Assert.AreEqual("0", textBoxCalculation.Text);
        }
    }
}
