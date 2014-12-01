/*
 * 
 * Realized by
 * Simone Salvo
 * 
 * */

using System;
using System.Collections;
using System.IO;

namespace aprior14
{
    class Program
    {

        static void Main(string[] args)
        {
            string line;

            ArrayList  fields = new ArrayList();
            ArrayList newSource = new ArrayList();

            Nodo newNodo = null;
            int[] values = null;
            Nodo[] source = null;
            ArrayList rowsA = null;
            ArrayList rowsB = null;
            Nodo[] sourcePlus = null;
            int percent = Int32.Parse(args[0]);

            int common = 0;
            int row = 0;
            int figli = 0;
            int m = 0;
            int j = 0;
            int w = 0;
            int k = 0;

            System.IO.StreamReader fileInput = new System.IO.StreamReader(args[1]);
            string fileOutput = args[1]+"RES.txt";

            if (!File.Exists(fileOutput))
                File.Create(fileOutput);

            // LETTURA DEI CAMPI, PRIMA RIGA DEL FILE
            line = fileInput.ReadLine();
            if (line != null)
                foreach (string field in line.Split(';'))
                    if (!string.IsNullOrEmpty(field))
                        fields.Add(field);
            //////////////////////////////////

            int fieldsN = fields.Count;

            source = new Nodo[fieldsN];
            values = new int[fieldsN];
            for (int i = 0; i < fieldsN; i++)
            {
                source[i] = null;
                values[i] = 0;
            }

            // LETTURA DEL FILE, DALLA SECONDA RIGA ALLA FINE
            while ((line = fileInput.ReadLine()) != null)
            { 
                if (!string.IsNullOrEmpty(line))
                {
                    foreach (string field in line.Split(';'))
                        values[k++] = Int32.Parse(field);

                    for (int i = 0; i < fieldsN; i++)
                    {
                        if (values[i] == 1)
                        {
                              if (source[i] == null)
                            {
                                source[i] = new Nodo(fields[i]);
                                figli++;
                            }
                            source[i].addRow(row);
                        }
                    }
                    row++;
                    k = 0;
                }
            }
            ///////////////////////////
           
            TextWriter tw = new StreamWriter(fileOutput, false);
            //RIMOZIONE NODI BASE LA QUALE FREQUENZA E' MINORE DI PERCENT
            // E STAMPA SUL FILE NEL RELATIVO SINGLETON CON FREQUENZA
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i].getFrequency() < percent)
                    source[i] = null;
                else
                    tw.WriteLine(source[i].getFieldsLikeString() + ";" + source[i].getFrequency());
            }
            ////////////////////////////

            sourcePlus = source;
  
            while (figli > 1)
            {
                figli = 0;
                fieldsN = sourcePlus.Length - 1;
                for (int i = 0; i < fieldsN ; i++)
                {
                    if (sourcePlus[i]!=null)
                    {
                        j = fields.Count - 1;
                        w = sourcePlus[i].getFields().Length - 1;
                     
                        while(sourcePlus[i].getFields()[w].CompareTo(fields[j]) != 0)
                            j--;
                        

                        for (j=j+1; j < fields.Count; j++)
                        {
                            rowsA = sourcePlus[i].getRows();
                            newNodo = null;
                            common = 0;
                            if (source[j]!=null)
                            {
                                rowsB = source[j].getRows();

                                for (int h = 0; h < rowsA.Count; h++)
                                {
                                    while ((m < rowsB.Count) && ((int)rowsA[h]>=(int)rowsB[m]))
                                    {
                                        if ((int)rowsA[h] == (int)rowsB[m])/////////////LOOK
                                        {
                                            if (newNodo == null)
                                                newNodo = new Nodo();
                                            newNodo.addRow(rowsA[h]);//////////////LOOK

                                            common++;
                                        }
                                        m++;
                                    } 
                                    m = 0;
                                }

                                if (( common >= 1) && (newNodo.getFrequency() > (percent)))
                                {
                                    figli++;
                                    foreach (string campo in sourcePlus[i].getFields())
                                        if (!string.IsNullOrEmpty(campo))
                                            newNodo.addField(campo);
                            
                                    newNodo.addField(fields[j].ToString());

                                    sourcePlus[i].addFigli(newNodo);
                                    source[j].addFigli(newNodo);

                                    newSource.Add(newNodo);

                                    tw.WriteLine(newNodo.getFieldsLikeString() + ";" + newNodo.getFrequency());

                                }
                            }
                        }
                    }
                }
                sourcePlus = newSource.ToArray(typeof(Nodo)) as Nodo[];
                newSource.Clear();
            }
            tw.Close();
        }
    }
}
