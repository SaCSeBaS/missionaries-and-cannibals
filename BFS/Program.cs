using System;

namespace BFS
{
    class Program
    {
        static void Main(string[] args)
        {
            State estado = new State(0, 0, 3, 3, true); // CREAR ESTADO INICIAL
            BFS BreadthFirstSearch = new BFS(); // CREAR LA BÚSQUEDA POR ANCHURA
            State estadoFinal = BreadthFirstSearch.BreadthFirstSearch(estado); // OBTENER LA SOLUCIÓN (NODO OBJETIVO)
            
            Console.WriteLine("Estado Inicial:" + "\n"
            + "Canibales izquierda: " + estado.CanibalesIzquierda + "\n"
            + "Misioneros izquierda: " + estado.MisionerosIzquierda + "\n"
            + "------------------------" + "\n"
            + "Canibales derecha: " + estado.CanibalesDerecha + "\n"
            + "Misioneros derecha: " + estado.MisionerosDerecha);
            if (estado.Bote)
                Console.WriteLine("Bote: DERECHA");
            else
                Console.WriteLine("Bote: IZQUIERDA");
            Console.Write("\n");
            Console.WriteLine(estadoFinal.ObtenerRecorrido()); // MOSTRAR EL RECORRIDO
        }
    }
}
