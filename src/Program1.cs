using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DeadLine24.FileHelper;

namespace DeadLine24
{
 

  class Program
  {
 static  string uzup(int inp )
    {
        if (inp < 10)
            return "0" + inp.ToString();
        return inp.ToString();
    }

    static void Main(string[] args)
    {
        for (int fi = 0; fi < 11; fi++)
        {

            FileReader fIn = new FileReader();
            fIn.openFile("trans"+uzup(fi)+".in");
            List<String> words;
            words = new List<string>();
            int amount = (int)fIn.readDouble();
            for (int i = 0; i < amount; i++)
                words.Add(fIn.readLine().Trim());

            /*
             * Zawieranie
             */
            for (int i = 0; i < words.Count; i++)
                for (int j = i + 1; j < words.Count; j++)
                {
                    if (words[i].Contains(words[j]))
                    {
                        words.RemoveAt(j);
                        j--;
                    }
                }
            //--

            String output = words[0];
            words.RemoveAt(0);
            //for (int i = 0; i < words.Count; i++)
            while (words.Count > 0)
            {
                int i = 0;
                // szukamy max zawierania                
                int max = 0;
                int index = i;
                for (int j = i; j < words.Count; j++)
                {
                    for (int k = words[j].Length; k > 0; k--)
                        if (output.Length - k > 0)
                        {
                           // Console.WriteLine(output.Substring(output.Length - k) + " : " + words[j].Substring(0, k));
                            if (output.Substring(output.Length - k).Contains(words[j].Substring(0, k)))
                            {
                               // Console.WriteLine(output.Substring(output.Length - k) + " : " + words[j].Substring(0, k));
                                if (k > max)
                                {
                                    max = k;
                                    index = j;
                                    break;
                                }
                            }
                        }
                }
                //Console.WriteLine("Dodaje do " + output + " słowo " + words[index] + " Obcięte do " +words[index].Substring(max));
                output += words[index].Substring(max);
                words.RemoveAt(index);
                if (index == i)
                    i--;

            }

            for (int i = 0; i < words.Count; i++)
                Console.WriteLine(fi+": " +words[i]);
          //  Console.WriteLine("---");
           // Console.WriteLine(output);
            fIn.closeFile();
            Console.WriteLine("Plik " + fi + " zakonczono: "+output);
            FileWriter fw = new FileWriter();
            fw.openFile("trans" + uzup(fi) + ".ans");
            fw.writeString(output.Length.ToString());
            fw.writeEnter();
            fw.writeString(output);
            fw.closeFile();
        }
      System.Console.ReadKey();     

    }
  }
}
