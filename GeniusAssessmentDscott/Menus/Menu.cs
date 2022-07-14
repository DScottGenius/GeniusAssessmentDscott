using System;
using System.Data.SqlClient;
using System.IO;

namespace GeniusAssessmentDscott.Menus
{
    public abstract class Menu
    {
        protected string filePath;
        protected bool fileExists;

        //The menus (both payments and user) will ask the user to input a filepath to be imported into the database
        public Menu(string fileIn)
        {
            Console.WriteLine("Please input a filepath or type 'q' to return to base menu");
            fileExists = false;
            while (true)
            {
                string input;
                if (fileIn != "")
                {

                    input = fileIn;
                }
                else
                {
                    input = @Console.ReadLine();
                }

                //Allow the user to return to the starting menu.
                if (input.ToLower() == "q")
                {
                    break;
                }
                else
                {
                    filePath = @input;
                }
                try
                {
                    fileExists = File.Exists(filePath);
                    if (fileExists)
                    {
                        break;
                    }
                    else
                    {
                        throw new FileNotFoundException();
                    }

                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                    break;
                }
                catch (FileNotFoundException e)
                {
                    //If the file can't be found, inform the user and ask them to try again.
                    Console.WriteLine($"There was an issue locating the file path {filePath} please try again.");
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error has occurred \n{e.Message}");
                    break;
                }


            }

        }

        //Subclasses will choose what parse to impelement and initiate inserting the data to the database
        public abstract void startParse(string filepath);
    }
}
