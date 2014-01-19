using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CarrierGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder builder = new StringBuilder();

            // File must be named "CarrierInfo.csv" and be in the same directory as the program
            string[] lines = File.ReadAllLines("Carriers.csv");
            foreach (string line in lines)
            {
                // Read all values out of the line
                string[] values = line.Split(',');
                string enumVal = values[0];
                string nameVal = values[1];
                string templateVal = values[2];

                // Create a declaration
                string declaration = string.Format(" {{ Carrier.{0} , new CarrierInfo(Carrier.{0}, \"{1}\", \"{2}\") }},", enumVal, nameVal, templateVal);
                // Add it to the values%
                builder.AppendLine(declaration);
            }

            // Dump the file
            File.WriteAllText("CarrierInfo.Loads.cs", builder.ToString());
        }
    }
}
