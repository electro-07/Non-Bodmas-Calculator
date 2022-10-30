using System;
using System.Collections.Generic;

namespace Calculator
{
    public class Calculator
    {
        static string[] opList { get; } = { "+", "-", "*", "/", "^", ",", ";" };
        static string[] independentOpList { get; } =
        {
            "!",
            ":",
            "S",
            "C",
            "T",
            "%",
            "_",
            "e",
            "p"
        };

        public static void Main(string[] args)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Title = "Calculator";

            decimal calculatedVal = 0;
            bool showCommas = true;

            Console.WriteLine("Example: 1+1 = 2");

            while (true)
            {
            end:
                string? input = "";
                string tempVal = "";
                string originalInput = "";
                int numIndex = 1;
                bool useOldAnswer = false;
                List<string> operators = new();
                List<decimal> numbers = new();
                Random random = new();

                Console.SetCursorPosition(0, 3);
                Console.WriteLine("Enter a value to calculate\r\n\r\n");

                Instructions();

                Console.SetCursorPosition(0, 5);
                originalInput = $"{Console.ReadLine()}";
                input = $"{originalInput};";
                input = input.Replace(" ", "");
                input = input.Replace("(", "");
                input = input.Replace(")", "");

                for (int i = 0; i < input!.Length; i++)
                {
                    if (!opList.Any(input.Substring(i, 1).Contains))
                    {
                        switch (input.Substring(i, 1))
                        {
                            case "X":
                            case "x":
                                showCommas = !showCommas;
                                Console.Clear();
                                Console.SetCursorPosition(0, 0);
                                if (showCommas)
                                {
                                    Console.WriteLine("Commas Enabled");
                                }
                                else
                                {
                                    Console.WriteLine("Commas Removed");
                                }
                                goto end;

                            case "!":

                                {
                                    string minRand = "";
                                    string maxRand = "";
                                    i++;
                                    try
                                    {
                                        while (input.Substring(i, 1) != "-")
                                        {
                                            minRand += input.Substring(i, 1);
                                            i++;
                                        }
                                        i++;
                                        while (
                                            !opList.Any(input.Substring(i, 1).Contains)
                                            && !independentOpList.Any(
                                                input.Substring(i, 1).Contains
                                            )
                                        )
                                        {
                                            maxRand += input.Substring(i, 1);
                                            i++;
                                        }
                                        tempVal += (
                                            random.NextInt64(
                                                Convert.ToInt32(minRand),
                                                Convert.ToInt32(maxRand)
                                            )
                                        ).ToString();
                                        i--;
                                    }
                                    catch (System.Exception)
                                    {
                                        Error();
                                        goto end;
                                    }
                                }
                                break;

                            case ":":

                                {
                                    try
                                    {
                                        tempVal = Math.Sqrt(Convert.ToDouble(tempVal)).ToString();
                                    }
                                    catch (System.Exception)
                                    {
                                        Error();
                                        goto end;
                                    }
                                }
                                break;

                            case "S":

                                {
                                    try
                                    {
                                        tempVal = Math.Sin(Convert.ToDouble(tempVal)).ToString();
                                    }
                                    catch (System.Exception)
                                    {
                                        Error();
                                        goto end;
                                    }
                                }
                                break;

                            case "C":

                                {
                                    try
                                    {
                                        tempVal = Math.Cos(Convert.ToDouble(tempVal)).ToString();
                                    }
                                    catch (System.Exception)
                                    {
                                        Error();
                                        goto end;
                                    }
                                }
                                break;

                            case "T":

                                {
                                    try
                                    {
                                        tempVal = Math.Tan(Convert.ToDouble(tempVal)).ToString();
                                    }
                                    catch (System.Exception)
                                    {
                                        Error();
                                        goto end;
                                    }
                                }
                                break;

                            default:
                                if (input.Substring(i, 1) == ".")
                                {
                                    tempVal += ".";
                                }
                                else
                                {
                                    try
                                    {
                                        tempVal += Convert.ToDecimal(input.Substring(i, 1));
                                    }
                                    catch (System.Exception)
                                    {
                                        Error();
                                        goto end;
                                    }
                                }
                                break;

                            case "%":
                                try
                                {
                                    tempVal = (Convert.ToDecimal((tempVal)) / 100).ToString();
                                }
                                catch (System.Exception)
                                {
                                    Error();
                                    goto end;
                                }
                                break;

                            case "_":
                                useOldAnswer = true;
                                tempVal = calculatedVal.ToString();
                                break;

                            case "e":
                                if (tempVal == "")
                                {
                                    tempVal = "1";
                                }
                                tempVal = (Convert.ToDouble(tempVal) * Math.E).ToString();
                                break;

                            case "p":
                                if (tempVal == "")
                                {
                                    tempVal = "1";
                                }
                                tempVal = (Convert.ToDouble(tempVal) * Math.PI).ToString();
                                break;
                        }
                    }
                    else
                    {
                        try
                        {
                            numbers.Add(Convert.ToDecimal(tempVal));
                            tempVal = "";
                            if (input.Substring(i, 1) != ";")
                            {
                                operators.Add(input.Substring(i, 1));
                            }
                        }
                        catch (System.Exception)
                        {
                            Error();
                            goto end;
                        }
                    }
                }

                if (!useOldAnswer)
                {
                    calculatedVal = numbers[0];
                }

                for (int i = 0; i < operators.Count; i++)
                {
                    switch (operators[i])
                    {
                        case "+":
                            try
                            {
                                calculatedVal += numbers[numIndex];
                            }
                            catch
                            {
                                Error();
                                goto end;
                            }
                            break;

                        case "-":
                            try
                            {
                                calculatedVal -= numbers[numIndex];
                            }
                            catch
                            {
                                Error();
                                goto end;
                            }
                            break;

                        case "*":
                            try
                            {
                                calculatedVal *= numbers[numIndex];
                            }
                            catch
                            {
                                Error();
                                goto end;
                            }
                            break;

                        case "/":
                            try
                            {
                                calculatedVal /= numbers[numIndex];
                            }
                            catch
                            {
                                Error();
                                goto end;
                            }
                            break;

                        case "^":

                            {
                                calculatedVal = (decimal)
                                    Math.Pow((double)calculatedVal, (double)numbers[numIndex]);
                            }
                            break;

                        case ",":
                            calculatedVal = Math.Round(
                                calculatedVal,
                                Convert.ToInt32(numbers[numIndex])
                            );
                            break;

                        case ":":
                            calculatedVal = (decimal)Math.Sqrt((double)calculatedVal);
                            break;

                        case "S":
                            calculatedVal = (decimal)
                                Math.Sin((double)calculatedVal * Math.PI / 180);
                            break;

                        case "C":
                            calculatedVal = (decimal)
                                Math.Cos((double)calculatedVal * Math.PI / 180);
                            break;

                        case "T":
                            calculatedVal = (decimal)
                                Math.Tan((double)calculatedVal * Math.PI / 180);
                            break;
                    }
                    numIndex++;
                }

                string calculatedValCommas = AddCommas();

                Console.Clear();
                Console.Write($"{originalInput} = ");
                Console.ForegroundColor = ConsoleColor.Green;
                if (showCommas)
                {
                    Console.WriteLine($"{calculatedValCommas}\r\n");
                }
                else
                {
                    Console.WriteLine($"{calculatedVal}\r\n");
                }
                Console.ForegroundColor = ConsoleColor.White;

                void Instructions()
                {
                    Console.SetCursorPosition(0, 8);

                    Console.WriteLine(
                        @"Instructions:

+ = Addition (10 + 20 gives 30)
- = Subtraction (10 - 20 gives -10)
* = Multiplication(10 * 20 gives 200)
/ = Division (10 / 20 gives 0.5)
^ = Power (10 ^ 2 gives 100)
% = Percent (500% gives 5)
!- = Random Number (!10-20 gives a random number between 10 and 20)
, = Round (10 / 3 ,2 gives 3.33)
: = Square Root (16: gives 4)
S = Sin (90 S gives 1)
C = Cos (90 C gives 0)
T = Tan (45 T gives 1)
e = Euler's Number (e gives 2.71828...)
p = PI (e gives 3.14159...)
_ = Old Answer (3 + 3 gives 6, so _ will then give 6)

Type X to toggle commas

Brackets and spaces can be added anywhere but they do not need to be added"
                    );
                }

                void Error()
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error\r\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                string AddCommas()
                {
                    string calculatedValCommas = calculatedVal.ToString();

                    for (
                        int i =
                            calculatedValCommas.Length
                            - 3
                            - (calculatedValCommas.Length - calculatedValCommas.IndexOf("."));
                        i > 0;
                        i -= 3
                    )
                    {
                        calculatedValCommas = calculatedValCommas.Insert(i, ",");
                    }
                    return calculatedValCommas;
                }
            }
        }
    }
}
