using System;
using System.IO;
using System.Collections.Generic;



namespace aoc7
{
    class Program
    {
        static void Main(string[] args)
        {
            string strippedline;
            var validbags = new List<string>(){"shiny gold"} ;  // hier zoek je op. deze lijst wordt steeds uitgebreid met nieuwe validbags.
            var morevalidbags = new List<string>();    //tempwaarde
            string mainbag;                             // staat aan begin van de rule voor contain
            var bags = new Dictionary<string, int>();  // deze zitten in de mainbag
            int validbagscountbeforesearch;            // bijf zoeken totdat de validbagslijst niet meer groeit.
            do
            {
                validbagscountbeforesearch = validbags.Count;
               
                Console.WriteLine(Environment.NewLine);
                foreach (string line in File.ReadLines(@"C:\repos\aoc7\input\real.txt"))
                {
                    //Console.WriteLine(line);
                    strippedline = line.Replace(" bags", null).Replace(" bag", null).Replace(".", null); //haal bag, bags en de . uit de regel
                    //Console.WriteLine(strippedline);
                    mainbag = strippedline.Split(" contain ")[0];  //voor contain
                    //Console.WriteLine(mainbag);
                    foreach (string numberandbag in strippedline.Split(" contain ")[1].Split(", ")) //bijv 5 muted plum, 8 striped bronze, 1 raw silver
                    {
                        //Console.WriteLine(numberandbag);
                        if (numberandbag.Equals("no other"))
                        {
                            //no other is speciaal geval
                            bags.Add("no other", 0);
                        }
                        else
                        {
                            bags.Add(numberandbag.Split(" ")[1] + " " + numberandbag.Split(" ")[2], int.Parse(numberandbag.Split(" ")[0])); //bijv muted plum,5
                        }
                    }

                    foreach (string validbag in validbags) //als de validbag in het lijstje achter de contains staat, voeg dan de mainbag toe aan het tijdelijke lijstje met validbags
                    {
                        if (bags.ContainsKey(validbag))
                        {
                            morevalidbags.Add(mainbag);
                        }
                    }
                    foreach (string validbag in morevalidbags)
                    {
                        if (!validbags.Contains(validbag)) { validbags.Add(validbag); }   //voeg de nieuw gevonde valid bags toe aan het valdigbag lijstje
                    }

                    // maak de lijstjes weer leeg voor volgende regel uit het bestand
                    morevalidbags.Clear();
                    bags.Clear();

                }
                Console.WriteLine($"current validbags, there may be more: { string.Join(",", validbags)}");


            } while (validbags.Count > validbagscountbeforesearch); //blijf naar bags zoeken, zolang er nog steeds meer worden gevonden.

            Console.WriteLine($"validbags including the first bag you want to carry in search: { string.Join(",", validbags)}");
            Console.WriteLine($"count of bag colors: {validbags.Count - 1}");

        }
    }
}