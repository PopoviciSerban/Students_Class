using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Elevi_Class
{
    class Program
    {
        static bool Comparision(string a, string b)
        {
            for (int i = 0; i < Math.Min(a.Length, b.Length); i++)
                if (a[i] < b[i])
                    return true;
                else if (a[i] > b[i])
                    return false;

            if (a.Length <= b.Length)
                return true;

            return false;
        }

        static void Main(string[] args)
        {
            var input_path = @"C:\Users\Serban\Desktop\input.txt";
            var output_path = @"C:\Users\Serban\Desktop\output.txt";
            
            string[] lines = File.ReadAllLines(input_path);

            List<Elev> Elevi = new List<Elev>();

            foreach (string line in lines)
            {
                string[] data = line.Split(' ');
                double[] note = new double[25];
                int numar_note = Convert.ToInt32(data[2]);

                for (int i = 0; i < numar_note; i++)
                    note[i] = Convert.ToDouble(data[i + 3]);

                Elev e = new Elev(data[0], data[1], numar_note, note);

                Elevi.Add(e);
            }

            for (int i = 0; i < Elevi.Count; i++)
                for (int j = i + 1; j < Elevi.Count; j++)
                    if (Elevi[i].media < Elevi[j].media || (Elevi[i].media == Elevi[j].media && Comparision(Elevi[i].nume, Elevi[j].nume) == false)
                        || (Elevi[i].media == Elevi[j].media && Elevi[i].nume.CompareTo(Elevi[j].nume) == 0 && Comparision(Elevi[i].prenume, Elevi[j].prenume) == false))
                    {
                        Elev Aux = Elevi[i];
                        Elevi[i] = Elevi[j];
                        Elevi[j] = Aux;
                    }

            using (StreamWriter sw = File.CreateText(output_path))
            {
                for (int i = 0; i < Elevi.Count; i++)
                    sw.WriteLine(Elevi[i].nume + " " + Elevi[i].prenume + " " + Math.Round(Elevi[i].media, 2));
            }
        }
    }

    internal class Elev
    {
        public string nume;
        public string prenume;
        public double media;
        private int numar_note;
        private double[] note;

        public Elev(string lastname, string firstname, int marks_number, double[] marks)
        {
            nume = lastname;
            prenume = firstname;
            media = 0;
            numar_note = marks_number;
            note = marks;

            for (int i = 0; i < numar_note; i++)
                media += note[i];

            media /= numar_note;
        }
    }
}
