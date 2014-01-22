using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CarrierGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder mappingBuilder = new StringBuilder();
            StringBuilder enumBuilder = new StringBuilder();

            // File must be named "CarrierInfo.csv" and be in the same directory as the program
            string[] lines = File.ReadAllLines("Carriers.csv");
            foreach (string line in lines)
            {
                // Read all values out of the line
                string[] values = line.Split(',');
                string enumVal = values[0];
                string nameVal = values[1];
                string templateVal = values[2];

                Regex rgx = new Regex("[^a-zA-Z0-9]");
                enumVal = rgx.Replace(nameVal, "");

                enumBuilder.Append(enumVal);
                enumBuilder.AppendLine(",");

                // Create a declaration
                string declaration = string.Format(" {{ Carrier.{0} , new CarrierInfo(Carrier.{0}, \"{1}\", \"{2}\") }},", enumVal, nameVal, templateVal);
                // Add it to the values%
                mappingBuilder.AppendLine(declaration);
            }

            // Dump the file
            File.WriteAllText("CarrierInfo.Loads.cs", mappingBuilder.ToString());
            File.WriteAllText("CarrierEnum.cs", enumBuilder.ToString());
        }
    }
}
