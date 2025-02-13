using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// Lab 2: Calculator Application
/// Include here the authors: Jason Peacock and Olivia Grace
/// Include here date/revisions: January 21, 2025
/// </summary>
namespace COMP3951_Lab4_Calculator
{
    /// <summary>
    /// The Calculator class represents a calculator that can perform binary and unary operations and store memory. It contains attributes to represent two operands, an 
    /// operation, and memory, as well as methods to access and update these attributes.
    /// </summary>
    internal class Calculator
    {

        /// <summary>
        /// Stores the first operand of a calculation
        /// </summary>
        private double? operand1;

        /// <summary>
        /// Stores the second operand of a calculation
        /// </summary>
        private double? operand2;

        /// <summary>
        /// Stores a binary operation for calculation. The binary operation can be /, *, -, +, or %.
        /// </summary>
        private string operation;

        /// <summary>
        /// Stores the value saved to memory.
        /// </summary>
        private double memory;

        /// <summary>
        /// A flag to tell if the calculator has just entered a calculation.
        /// </summary>
        bool justEnteredOperation = false;

        /// <summary>
        /// Memory property. Includes a get and set function for accessing and updating memory.
        /// </summary>
        public double Memory
        {
            get { return memory; }

            set { memory = value; }
        }

        /// <summary>
        /// Operation property.
        /// </summary>
        public string Operation
        {
            get { return operation; }

            set { operation = value; }
        }

        /// <summary>
        /// justEnteredOperation property.
        /// </summary>
        public bool JustEnteredOperation
        {
            get { return justEnteredOperation; }

            set { justEnteredOperation = value; }
        }

        /// <summary>
        /// Constructor. Initializes a new Calculator object.
        /// </summary>
        public Calculator()
        {
            operand1 = null;
            operand2 = null;
            operation = "";
            memory = 0;
        }

        /// <summary>
        /// If newOperation is a unary operation (x^2, 1/x, or sqrt) then newOperation acts on operand1 or operand2, depending on which is set, stores its result
        /// in that operand, and then returns the result.
        /// 
        /// If newOperation is a binary operation (+, - , *, /, or %) then performCalculation is called for the existing operand1, operand2, and operation. If a non-null 
        /// result is calculated then that result is returned and stored in operand1, newOperation is stored in oepration, and operand2 is set to null. If a null result is 
        /// calculated, then operand1 is set to 0, operation is set to newOperation, and operand2 is set to null.
        /// 
        /// </summary>
        /// <param name="newOperation">a string representing an operation</param>
        /// <returns>a double if a calculation was performed, null otherwise</returns>
        public double? setOperation(string newOperation)
        {
            double? result = null;

            // Check if we are working with a unary operation or a binary operation
            if (newOperation == "sqrt" || newOperation == "1/x" || newOperation == "x^2")
            {

                if (operand2.HasValue)
                {
                    result = performUnaryCalculation(newOperation, operand2 ?? 0);
                    operand2 = result;
                }
                else
                {
                    result = performUnaryCalculation(newOperation, operand1 ?? 0);
                    operand1 = result;
                }

            } else 
            {
                // If there is an existing calculation, perform it and store the result in operand1. Return operand1.
                if (operand1.HasValue && operation != "" && operand2.HasValue)
                {
                    result = performCalculation();
                    operand1 = result;
                    operation = newOperation;
                    operand2 = null;
                } else
                {
                    operation = newOperation;
                    operand2 = null;
                }

            }


            return result;
        }


        /// <summary>
        /// Performs a unary calculation and returns the result. The operation parameter specifies the operator and the operand parameter specifies the operand for the 
        /// unary calculation. The operation parameter can be 1/x, x^2, or sqrt.
        /// </summary>
        /// <param name="operation">a string representing a unary operation</param>
        /// <param name="operand">an operand</param>
        /// <returns>the result of the calculation of the unary operation on the operand</returns>
        private double performUnaryCalculation(string operation, double operand)
        {
            double result = 0;
            if (operation == "1/x")
            {
                if (operand == 0)
                {
                    result = Double.PositiveInfinity;
                }
                else
                {
                    result = 1 / operand;
                }
            } else if (operation == "x^2")
            {
                result = operand * operand;
            } else if (operation == "sqrt")
            {
                result = Math.Sqrt(operand);
            }

            return result;
        }

        /// <summary>
        /// Sets either operand1 or operand2 to be newOperand depending on the current values of operand1 and operand2.
        /// 
        /// If operand1 is null, then operand1 is set to newOperand.
        /// If operand1 is not null and operand 2 is null then operand2 is set to newOperand.
        /// If operand1 and operand2 are both not null, then operand1 is set to newOperand and operation and operand2 are set to null.
        /// </summary>
        /// <param name="newOperand">a double</param>
        public void setOperand(double newOperand)
        {
            if (operand1 == null)
            {
                operand1 = newOperand;
            } else if (operand2 == null)
            {
                operand2 = newOperand;
            } else if (operand1.HasValue && operand2.HasValue)
            {
                operand1 = newOperand;
                operand2 = null;
                operation = "";
            }
        }

        /// <summary>
        /// Performs a binary calculation based on what is stored in operand1, operation, or operand2 and returns the result. If operation is empty, then operand1 is returned.
        /// If operand1 and operation have non-empty and non-null values and operand2 is null, copy the value of operand1 into operand2 and perform the caluclation that way.
        /// </summary>
        /// <returns>the result of the calculation or operand1 if a calculation can not be performed</returns>
        public double? performCalculation()
        {
            double? result = null;

            if (operand2 == null && operand1.HasValue && operation != "")
            {
                operand2 = operand1;
            }

            if (operand1.HasValue && operand2.HasValue && operation != "")
            {
                if (operation == "/")
                {
                    if (operand2 == 0)
                    {
                        result = Double.PositiveInfinity;
                    }
                    else
                    {
                        result = operand1 / operand2;
                    }
                } else if (operation == "*"){
                    result = operand1 * operand2;
                } else if (operation == "-")
                {
                    result = operand1 - operand2;
                } else if (operation == "+")
                {
                    result = operand1 + operand2;
                } else if (operation == "%")
                {
                    result = operand1 * 0.01 * operand2;
                }
            } else
            {
                result = operand1;
            }

            return result;
        }

        /// <summary>
        /// Clears the current operand
        /// </summary>
        public void clearCurrent()
        {
            if(operand1 != null && operation.Equals(""))
            {
                operand1 = null;
            }
        }
        
        /// <summary>
        /// Clears operand1 and operand2 by setting them both to null. Clears operation by setting it to an empty string.
        /// </summary>
        public void clearAll()
        {
            operand1 = null;
            operand2 = null;
            operation = "";
        }
    }
}
