/*
 * 
 * Realized by
 * Simone Salvo
 * 
 * */

using System;
using System.Collections;


namespace aprior14
{
    class Nodo
    {

        private ArrayList rows = new ArrayList();
        private ArrayList fields = new ArrayList();
        private int frequenza = 0;
        private ArrayList son = new ArrayList();

        public  Nodo(object s)
        {
            fields.Add(s.ToString());
        }

        public Nodo()
        {
        }


        public void addRow(object i)
        {
            rows.Add(i);
            frequenza++;
        }

        public ArrayList getRows()
        {
            return rows;
        }

        public void addFigli(Nodo newNodo)
        {
            son.Add(newNodo);
        }

        public string [] getFields()
        {
            String[] str = this.fields.ToArray(typeof(string)) as String[];
            return str;
;
        }

        public int getFrequency()
        {
            return this.frequenza;
        }

        public string getFieldsLikeString()
        {
            String[] str = this.fields.ToArray(typeof(string)) as String[];

            return string.Join(";", str);
        }

        public void addField(string campo)
        {
            if (!fields.Contains(campo))
                fields.Add(campo);
        }
    }
}
