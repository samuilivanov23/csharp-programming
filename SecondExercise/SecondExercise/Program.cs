using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml;

class Program
{
    public struct Coordinates
    {
        public double lat;
        public double lng;
    }

    public struct Contact
    {
        public string Name;
        public string ID;
        public string PhoneNumber;
    }

    static void Main(string[] args)
    {
        //Task 1
        string directory = AppDomain.CurrentDomain.BaseDirectory;
        string sourceDirectory = Path.GetFullPath(Path.Combine(directory, @"..\..\..\"));

        // Full path to input and output files
        string inputFile = Path.Combine(sourceDirectory, "input1.txt");
        string outputFile = Path.Combine(sourceDirectory, "first_task_output.json");

        List<Coordinates> coordinatesList = new List<Coordinates>();

        // Read from input file
        string input = File.ReadAllText(inputFile);

        // Split the input data by semicolon to get individual coordinate pairs
        string[] pairs = input.Split(';');

        foreach (string pair in pairs)
        {
            // Split each pair by comma to separate latitude and longitude
            string[] coords = pair.Split(',');

            if (coords.Length == 2)
            {
                Coordinates coord;
                if (double.TryParse(coords[0], out coord.lat) && double.TryParse(coords[1], out coord.lng))
                {
                    coordinatesList.Add(coord);
                }
            }
        }

        string jsonString = "[";

        for (int i = 0; i < coordinatesList.Count; i++)
        {
            jsonString += "{\"lat\":" + coordinatesList[i].lat + ",\"lng\":" + coordinatesList[i].lng + "}";
            if (i < coordinatesList.Count - 1)
            {
                jsonString += ",";
            }
        }

        jsonString += "]";
        File.WriteAllText(outputFile, jsonString);

        Console.WriteLine("Coordinates converted and saved as output.json.");


        //Task 2
        Console.OutputEncoding = Encoding.UTF8;
        string inputFile2 = Path.Combine(sourceDirectory, "input2.txt");
        string outputFile2 = Path.Combine(sourceDirectory, "second_task_output.xml");

        string inputData = File.ReadAllText(inputFile2);
        string[] tokens = inputData.Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        List<Contact> contacts = new List<Contact>();
        int offset = 6;

        for (int i = 0; i < tokens.Length; i += offset) 
        {
            string name = "";
            string id = "";
            string phoneNumber = "";

            for (int j = i; j < i + offset; j++) 
            {
                tokens[j] = tokens[j].Trim();
                if (ContainsCyrillic(tokens[j]))
                {
                    name = tokens[j];
                }
                else if (IsValidID(tokens[j]))
                {
                    id = tokens[j];
                }
                else 
                {
                    phoneNumber = tokens[j] + tokens[j+1] + tokens[j+2] + tokens[j+3];
                    j += 3;
                }
            }

            contacts.Add(new Contact { Name = name.Trim(), ID = id.Trim(), PhoneNumber = phoneNumber.Trim() });
        }

        WriteToXmlFile(outputFile2, contacts);
    }

    static void WriteToXmlFile(string filePath, List<Contact> contacts)
    {
        XmlDocument doc = new XmlDocument();
        XmlElement root = doc.CreateElement("Contacts");
        doc.AppendChild(root);

        foreach (var contact in contacts)
        {
            XmlElement contactElement = doc.CreateElement("Contact");

            XmlElement nameElement = doc.CreateElement("Name");
            nameElement.InnerText = contact.Name;
            contactElement.AppendChild(nameElement);

            XmlElement idElement = doc.CreateElement("ID");
            idElement.InnerText = contact.ID;
            contactElement.AppendChild(idElement);

            XmlElement phoneNumberElement = doc.CreateElement("PhoneNumber");
            phoneNumberElement.InnerText = contact.PhoneNumber;
            contactElement.AppendChild(phoneNumberElement);

            root.AppendChild(contactElement);
        }

        doc.Save(filePath);
    }

    static bool ContainsCyrillic(string input)
    {
        return Regex.IsMatch(input, @"\p{IsCyrillic}");
    }

    static bool IsValidID(string input)
    {
        return input.Length == 6;
    }
}