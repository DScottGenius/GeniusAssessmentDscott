using Microsoft.VisualBasic.FileIO;
using System;
using System.Globalization;

namespace GeniusAssessmentDscott.CSV_Reader
{
    public abstract class ReadCSV
    {
        protected string Filepath
        {
            get;
        }
        protected ReadCSV(string filePath)
        {
            Filepath = filePath;
        }

        protected void StartParse(string filepath)
        {
            using (TextFieldParser textFieldParser = new TextFieldParser(filepath))
            {
                textFieldParser.TextFieldType = FieldType.Delimited;

                textFieldParser.SetDelimiters(",");
                parseCSV(textFieldParser);
            }
        }

        protected abstract void parseCSV(TextFieldParser parser);

        protected string CheckDate(string dateString)
        {
            DateTime date;
            bool validDate = DateTime.TryParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

            if (validDate)
            {
                return $"{date.Year}/{date.Month}/{date.Day}";
            }
            else
            {
                throw new FormatException();
            }
        }

    }
}
