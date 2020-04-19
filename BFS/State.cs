using System;
using System.Collections.Generic;
using System.Text;

namespace BFS
{
    public class State
    {
        public int CanibalesIzquierda { get; set; }
        public int MisionerosIzquierda { get; set; }
        public int CanibalesDerecha { get; set; }
        public int MisionerosDerecha { get; set; }
        public bool Bote { get; set; } // TRUE - DERECHA | FALSE - IZQUIERDA
        public bool Explorado { get; set; }
        public State Padre { get; set; }
        public List<State> Hijos { get; set; }

        public State(int canibalesIzquierda, int misionerosIzquierda, int canibalesDerecha, int misionerosDerecha, bool bote)
        {
            CanibalesIzquierda = canibalesIzquierda;
            MisionerosIzquierda = misionerosIzquierda;
            CanibalesDerecha = canibalesDerecha;
            MisionerosDerecha = misionerosDerecha;
            Bote = bote;
            Explorado = false;
            Padre = null;
            Hijos = new List<State>();
        }

        public void GenerarHijos()
        {
            State hijo;
            if(Bote) // BOTE ESTÁ EN LA DERECHA
            {
                hijo = new State(CanibalesIzquierda, MisionerosIzquierda + 1, CanibalesDerecha, MisionerosDerecha - 1, false); // 1 MISIONARIO CRUZA A LA IZQUIERDA
                if (hijo.EsValido())
                    Hijos.Add(hijo);

                hijo = new State(CanibalesIzquierda + 1, MisionerosIzquierda, CanibalesDerecha - 1, MisionerosDerecha, false); // 1 CANIBAL CRUZA A LA IZQUIERDA
                if (hijo.EsValido())
                    Hijos.Add(hijo);

                hijo = new State(CanibalesIzquierda + 1, MisionerosIzquierda + 1, CanibalesDerecha - 1, MisionerosDerecha - 1, false); // 1 CANIBAL Y 1 MISIONERO CRUZAN A LA IZQUIERDA
                if (hijo.EsValido())
                    Hijos.Add(hijo);

                hijo = new State(CanibalesIzquierda + 2, MisionerosIzquierda, CanibalesDerecha - 2, MisionerosDerecha, false); // 2 CANIBALES CRUZAN A LA IZQUIERDA
                if (hijo.EsValido())
                    Hijos.Add(hijo);

                hijo = new State(CanibalesIzquierda, MisionerosIzquierda + 2, CanibalesDerecha, MisionerosDerecha - 2, false); // 2 MISIONEROS CRUZAN A LA IZQUIERDA
                if (hijo.EsValido())
                    Hijos.Add(hijo);
            }
            else // BOTE ESTÁ EN LA IZQUIERDA
            {
                hijo = new State(CanibalesIzquierda, MisionerosIzquierda - 1, CanibalesDerecha, MisionerosDerecha + 1, true); // 1 MISIONARIO CRUZA A LA DERECHA
                if (hijo.EsValido())
                    Hijos.Add(hijo);

                hijo = new State(CanibalesIzquierda - 1, MisionerosIzquierda, CanibalesDerecha + 1, MisionerosDerecha, true); // 1 CANIBAL CRUZA A LA DERECHA
                if (hijo.EsValido())
                    Hijos.Add(hijo);

                hijo = new State(CanibalesIzquierda - 1, MisionerosIzquierda - 1, CanibalesDerecha + 1, MisionerosDerecha + 1, true); // 1 CANIBAL Y 1 MISIONERO CRUZAN A LA DERECHA
                if (hijo.EsValido())
                    Hijos.Add(hijo);

                hijo = new State(CanibalesIzquierda - 2, MisionerosIzquierda, CanibalesDerecha + 2, MisionerosDerecha, false); // 2 CANIBALES CRUZAN A LA DERECHA
                if (hijo.EsValido())
                    Hijos.Add(hijo);

                hijo = new State(CanibalesIzquierda, MisionerosIzquierda - 2, CanibalesDerecha, MisionerosDerecha + 2, false); // 2 MISIONEROS CRUZAN A LA DERECHA
                if (hijo.EsValido())
                    Hijos.Add(hijo);
            }
        }

        public bool EsValido() // VERIFICAR VÁLIDEZ 
            => (MisionerosIzquierda >= 0 && MisionerosDerecha >= 0 && CanibalesIzquierda >= 0 && CanibalesDerecha >= 0) // VALIDAR NÚMEROS
                && (MisionerosIzquierda == 0 || MisionerosIzquierda >= CanibalesIzquierda) // VALIDAR EQUIDAD EN LA IZQUIERDA
                && (MisionerosDerecha == 0 || MisionerosDerecha >= CanibalesDerecha);

        public bool EsFinal() => CanibalesDerecha == 0 && MisionerosDerecha == 0;// VERIFICAR SI SE LLEGÓ AL ESTADO FINAL

        public string ObtenerRecorrido() // OBTENER EL RECORRIDO HASTA EL PADRE
        {
            State next = this;
            List<State> reporte = new List<State>();
            string texto = "";

            while (next.Padre != null) // OBTENER CAMINO
            {
                reporte.Add(next);
                next = next.Padre;
            }

            for(int i = reporte.Count-1, j = 1; i >= 0; i--, j++)
            {
                texto += "Paso N°: " + j + "\n"
                       + "Canibales izquierda: " + reporte[i].CanibalesIzquierda + "\n"
                       + "Misioneros izquierda: " + reporte[i].MisionerosIzquierda + "\n"
                       + "------------------------" + "\n"
                       + "Canibales derecha: " + reporte[i].CanibalesDerecha + "\n"
                       + "Misioneros derecha: " + reporte[i].MisionerosDerecha + "\n";

                if (reporte[i].Bote)
                    texto += "Bote: " + "DERECHA";
                else
                    texto += "Bote: " + "IZQUIERDA";

                texto += "\n\n";
            }

            return texto;
        }
    }
}
        