using System;
using System.Collections.Generic;

namespace RobiCommandMovement
{
    static class Program
    {       
        static void Main(string[] args)
        {
            string command = "LLF9RF4RRF12";
            //X = -8, Y = 9, D = L
            string lastLocation = LastLocation(command);
            Console.WriteLine(lastLocation);
        }

        static (string, string) SplitAt(this string command, int index) => (command.Substring(0, index), command[index..]);

        static string LastLocation(string command)
        {
            var dirMap = new Dictionary<int, char>
            {
                { 0, 'T' },
                { 1, 'L' },
                { 2, 'B' },
                { 3, 'R' }
            };
            int x = 0, y = 0, lastDir = 0, b=0 ;
            var D = 'T';
            for (var i = 0; i < command.Length; i++)
            {
                var com = command[i];
                if (com.Equals('L')) //anti-clockwise
                { 
                    int curDir = ++lastDir % 4;
                    D = dirMap.GetValueOrDefault(curDir);
                }
                else if (com.Equals('R')) //clockwise
                { 
                    if (lastDir == 0)
                        lastDir = 3;
                    else
                        lastDir--;
                    int curDir = lastDir;
                    D = dirMap.GetValueOrDefault(curDir);
                }
                else //movement
                { 
                    var stepConcat = string.Empty;
                    if (com.Equals('F'))
                    {
                        var (_, right) = SplitAt(command, i + 1);
                        for (int j = 0; j < right.Length; j++)                        {
                            char c = right[j];
                            if (!int.TryParse(c.ToString(), out int result)) break;
                            stepConcat += result.ToString();
                        }
                    }
                    int steps = string.IsNullOrWhiteSpace(stepConcat) ? 0 : Convert.ToInt32(stepConcat);
                    if (D.Equals('T'))
                    {
                        y += steps;
                    }
                    else if (D.Equals('L'))
                    {
                        x -= steps;
                    }
                    else if (D.Equals('B'))
                    {
                        y -= steps;
                    }
                    else if (D.Equals('R'))
                    {
                        x += steps;
                    }
                }
            }
            return $"X:{x}; Y:{y}; D:{D}; B:{b}";
        }
    }
}
