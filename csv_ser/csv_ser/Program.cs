using System;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

class Program
{
    static void Main(string[] args)
    {
        read_and_search test_fadi = new read_and_search();
        test_fadi.spleting_keys(args);
    }
}
class read_and_search
{
    private string filePath;
    private int columnNumber;
    private string searchKey;

    public void spleting_keys(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Invalid number of arguments. Usage: search.exe <file_path> <column_number> <search_key>");
            return;
        }
        int arg_number = 0, t1 = 0;
        string temp = "";
        res:
        while (!args[arg_number].Contains(".csv")) { arg_number++; }
        arg_number++;
        for (; t1 < arg_number; t1++)
        {
            temp += args[t1] + " ";
        }

        temp.Remove(temp.Length);
        if (!File.Exists(@temp))
        {
            temp += "";
            goto res;
        }

        filePath = temp;
        columnNumber = int.Parse(args[arg_number]);
        temp = "";
        for (t1 = (arg_number + 1); t1 < args.Length; t1++)
        {
            temp = temp + args[t1] + " ";
        }
        temp.Remove(temp.Length);
        searchKey = temp;
        save_in_file(serch());
    }
    private string[] serch()
    {
        string[] arr = new string[1];
        try
        {
            var lines = File.ReadAllLines(filePath);
            string t1, t2; ;
            List<string> matchingLines = new List<string>();
            foreach (var line in lines)
            {
                var columns = line.Split(';');
                var colom = columns[0].Split(',');
                t1=colom[columnNumber].ToUpper();
                t2=searchKey.ToUpper();
                if (String.Compare(t1,0,t2,0,t1.Length,false)==0)
                {
                    matchingLines.Add(line);
                }
            }

            if (matchingLines.Any())
            {
                foreach (var line in matchingLines)
                {
                    Console.WriteLine(line);
                }
                arr = matchingLines.ToArray();
            }
            else
            {
                Console.WriteLine("No matching lines found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }

        return arr;
    }
    private void save_in_file(string[] lines)
    {
        string filepath = Path.GetDirectoryName(filePath);
        filepath += "\\res.txt";
        try
        {
            if (!File.Exists("res.txt"))
            {
                Console.WriteLine("File does not exist. Creating a new file.");
                File.Create(filepath).Close();
            }
            else
            {
                File.WriteAllText(filepath, string.Empty);
            }
            if (lines.Length > 0)
            {
                File.WriteAllLines(filepath, lines);
            }
            else
            {
                File.WriteAllText(filepath, "there is no res");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error : " + ex.Message);
        }
    }
}

