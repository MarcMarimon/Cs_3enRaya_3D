using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }
        static void Menu()
        {
            while (true)
            {
                Console.WriteLine("Seleccione una opción:");
                Console.WriteLine("1. Jugar al 3 en raya");
                Console.WriteLine("2. Jugar al N en raya");
                Console.WriteLine("3. Jugar al 3 en raya 3D");
                Console.WriteLine("4. Jugar al N en raya 3D");
                Console.WriteLine("5. Enum dia de la semana");
                Console.WriteLine("0. Salir");

                int opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        JugarEnRaya(3);
                        break;
                    case 2:
                        JugarNEnRaya();
                        break;
                    case 3:
                        JugarEnRaya3D(3);
                        break;
                    case 4:
                        JugarNEnRaya3D();
                        break;
                    case 5:
                        IntroducirDiaDeLaSemana();
                        break;
                    case 0:
                        Console.WriteLine("Saliendo...");
                        return;
                    default:
                        Console.WriteLine("Opción no válida. Inténtelo de nuevo.");
                        break;
                }
            }
        }
        static void JugarEnRaya(int size)
        {
            string[,] tablero = new string[size, size];
            InicializarTablero(tablero);

            char jugadorActual = 'X';
            int movimientos = 0;
            int totalMovimientos = size * size;

            while (true)
            {
                Console.Clear();
                DibujarTablero(tablero);

                Console.WriteLine($"Turno del jugador {jugadorActual}. Ponga su ficha en el tablero (1-{totalMovimientos}). Si se desea terminar la partida pulse la tecla Q");
                string input = Console.ReadLine();

                if (input.ToUpper() == "Q")
                {
                    Console.WriteLine("Saliendo de la partida...");
                    return;
                }

                if (int.TryParse(input, out int posicion) && posicion >= 1 && posicion <= totalMovimientos)
                {
                    int fila = (posicion - 1) / size;
                    int columna = (posicion - 1) % size;

                    if (tablero[fila, columna] != "X" && tablero[fila, columna] != "O")
                    {
                        tablero[fila, columna] = jugadorActual.ToString();
                        movimientos++;

                        if (VerificarVictoria(tablero, jugadorActual.ToString()))
                        {
                            Console.Clear();
                            DibujarTablero(tablero);
                            Console.WriteLine($"El ganador es {jugadorActual} !!");
                            break;
                        }
                        if (movimientos == totalMovimientos)
                        {
                            Console.Clear();
                            DibujarTablero(tablero);
                            Console.WriteLine("Empate!");
                            break;
                        }

                        jugadorActual = (jugadorActual == 'X') ? 'O' : 'X';

                    }
                    else
                    {
                        Console.WriteLine("Posición ocupada, intentelo de nuevo");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("Porfavor introducza una posición valida");
                    Console.ReadKey();
                }
            }
        }

        static void JugarNEnRaya()
        {
            int size = 0;
            while (true)
            {
                Console.WriteLine("Introduzca el tamaño del tablero:");

                if (!int.TryParse(Console.ReadLine(), out size) || size > 4)
                {
                    Console.WriteLine("Introduzca un numero valido para el tamaño del tablero (no mayor a 4).");
                }
                else
                {
                    break;
                }
            }
            JugarEnRaya(size);
        }
        static void InicializarTablero(string[,] tablero)
        {
            int contador = 1;
            for (int i = 0; i < tablero.GetLength(0); i++)
            {
                for (int j = 0; j < tablero.GetLength(1); j++)
                {
                    tablero[i, j] = contador.ToString();
                    contador++;
                }
            }
        }
        static void DibujarTablero(string[,] tablero)
        {
            int size = tablero.GetLength(0);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write($" {tablero[i, j].PadLeft(2)} ");
                    if (j < size - 1) Console.Write("|");
                }
                Console.WriteLine();
                if (i < size - 1) Console.WriteLine(new string('-', size * 5 - 1));
            }
        }
        static bool VerificarLinea(string[,] tablero, string jugador, int startX, int startY, int stepX, int stepY, int size)
        {
            int x = startX;
            int y = startY;

            while (x < size && y < size && x >= 0 && y >= 0)
            {
                if (tablero[x, y] != jugador)
                {
                    return false;
                }
                x += stepX;
                y += stepY;
            }

            return true;
        }
        static bool VerificarVictoria(string[,] tablero, string jugador)
        {
            int size = tablero.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                if (VerificarLinea(tablero, jugador, i, 0, 0, 1, size) ||
                    VerificarLinea(tablero, jugador, 0, i, 1, 0, size))
                {
                    return true;
                }
            }

            if (VerificarLinea(tablero, jugador, 0, 0, 1, 1, size) ||
                VerificarLinea(tablero, jugador, 0, size - 1, 1, -1, size))
            {
                return true;
            }

            return false;
        }
        static void JugarNEnRaya3D()
        {
            int size = 0;
            while (true)
            {
                Console.WriteLine("Introduzca el tamaño del tablero:");

                if (!int.TryParse(Console.ReadLine(), out size) || size > 4)
                {
                    Console.WriteLine("Introduzca un numero valido para el tamaño del tablero (no mayor a 4).");
                }
                else
                {
                    break;
                }
            }
            JugarEnRaya3D(size);
        }
        static void JugarEnRaya3D(int size)
        {
            string[,,] tablero = new string[size,size,size];
            InicializarTablero3D(tablero);

            char jugadorActual = 'X';
            int movimientos = 0;
            int totalMovimientos = size * size * size;

            while (true)
            {
                Console.Clear();
                DibujarTablero3D(tablero);

                Console.WriteLine($"Turno del jugador {jugadorActual}. Ponga su ficha en el tablero (1-{totalMovimientos}). Si se desea terminar la partida pulse la tecla Q");
                string input = Console.ReadLine();

                if (input.ToUpper() == "Q")
                {
                    Console.WriteLine("Saliendo de la partida...");
                    return;
                }
                
                if(int.TryParse(input, out int posicion) && posicion >= 1 && posicion <= totalMovimientos)
                {
                    int x = (posicion - 1) / (size * size);
                    int y = (posicion - 1) % (size * size) / size;
                    int z = (posicion - 1) % (size * size) % size;

                    if (tablero[x, y, z] != "X" && tablero[x, y, z] != "O")
                    {
                        tablero[x, y, z] = jugadorActual.ToString();
                        movimientos++;

                        if (VerificarVictoria3D(tablero, jugadorActual.ToString(), size))
                        {
                            Console.Clear();
                            DibujarTablero3D(tablero);
                            Console.WriteLine($"El ganador es {jugadorActual} !!");
                            break;
                        }
                        if (movimientos == totalMovimientos)
                        {
                            Console.Clear();
                            DibujarTablero3D(tablero);
                            Console.WriteLine("Empate!");
                            break;
                        }

                        jugadorActual = (jugadorActual == 'X') ? 'O' : 'X';
                    }
                    else
                    {
                        Console.WriteLine("Posición ocupada, inténtelo de nuevo");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("Por favor, introduzca una posición válida");
                    Console.ReadKey();
                }
            }
        }
        static void InicializarTablero3D(string[,,] tablero)
        {
            int contador = 1;
            for (int i = 0; i < tablero.GetLength(0); i++)
            {
                for (int j = 0; j < tablero.GetLength(1); j++)
                {
                    for (int k = 0; k < tablero.GetLength(2); k++)
                    {
                        tablero[i, j, k] = contador.ToString();
                        contador++;
                    }
                }
            }
        }

        static void DibujarTablero3D(string[,,] tablero)
        {
            int size = tablero.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                Console.WriteLine($"Fila {i + 1}:");
                for (int z = 0; z < size; z++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        Console.Write($" {tablero[i, j, z].PadLeft(2)} ");
                        if (j < size - 1) Console.Write("|");
                    }
                    Console.WriteLine();
                    if (z < size - 1) Console.WriteLine(new string('-', size * 5 - 1));
                }
                Console.WriteLine();
            }
        }

        static bool VerificarLinea3D(string[,,] tablero, string jugador, int startX, int startY, int startZ, int stepX, int stepY, int stepZ, int size)
        {
            int count = 0;
            int x = startX;
            int y = startY;
            int z = startZ;

            while (x < size && y < size && z < size && x >= 0 && y >= 0 && z >= 0)
            {
                if (tablero[x, y, z] == jugador)
                {
                    count++;
                    if (count == size) return true;
                }
                else
                {
                    count = 0;
                }
                x += stepX;
                y += stepY;
                z += stepZ;
            }

            return false;
        }
        static bool VerificarVictoria3D(string[,,] tablero, string jugador, int size)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    // Verificar en el plano XY
                    if (VerificarLinea3D(tablero, jugador, i, j, 0, 0, 0, 1, size) || VerificarLinea3D(tablero, jugador, i, 0, j, 0, 1, 0, size))
                        return true;
                }

                // Verificar en el plano XZ
                if (VerificarLinea3D(tablero, jugador, i, 0, 0, 0, 1, 0, size) || VerificarLinea3D(tablero, jugador, i, 0, size - 1, 0, 1, 0, size))
                    return true;

                // Verificar en el plano YZ
                if (VerificarLinea3D(tablero, jugador, 0, i, 0, 1, 0, 0, size) || VerificarLinea3D(tablero, jugador, 0, i, size - 1, 1, 0, 0, size))
                    return true;
            }

            // Verificar diagonales en el plano XY
            if (VerificarLinea3D(tablero, jugador, 0, 0, 0, 1, 1, 0, size) || VerificarLinea3D(tablero, jugador, 0, size - 1, 0, 1, -1, 0, size))
                return true;

            // Verificar diagonales en el plano XZ
            if (VerificarLinea3D(tablero, jugador, 0, 0, 0, 1, 0, 1, size) || VerificarLinea3D(tablero, jugador, 0, size - 1, 0, 1, 0, -1, size))
                return true;

            // Verificar diagonales en el plano XYZ
            if (VerificarLinea3D(tablero, jugador, 0, 0, 0, 1, 1, 1, size) || VerificarLinea3D(tablero, jugador, size - 1, 0, 0, -1, 1, 1, size) ||
                VerificarLinea3D(tablero, jugador, 0, size - 1, 0, 1, -1, 1, size) || VerificarLinea3D(tablero, jugador, 0, 0, size - 1, 1, 1, -1, size) ||
                VerificarLinea3D(tablero, jugador, size - 1, size - 1, 0, -1, -1, 1, size) || VerificarLinea3D(tablero, jugador, size - 1, 0, size - 1, -1, 1, -1, size) ||
                VerificarLinea3D(tablero, jugador, 0, size - 1, size - 1, 1, -1, -1, size) || VerificarLinea3D(tablero, jugador, size - 1, size - 1, size - 1, -1, -1, -1, size))
                return true;

            return false;
        }


        enum DiasDeLaSemana
        {
            Lunes,
            Martes,
            Miercoles,
            Jueves,
            Viernes,
            Sabado,
            Domingo
        }
        static void IntroducirDiaDeLaSemana()
        {
            Console.WriteLine("Introduzca un día de la semana (Lunes, Martes, Miercoles, Jueves, Viernes, Sabado, Domingo):");
            string input = Console.ReadLine();

                if (Enum.TryParse<DiasDeLaSemana>(input, true, out DiasDeLaSemana dia))

            {
                int numeroDia = (int)dia +1;
                Console.WriteLine($"El día introducido es: {input} y su número correspondiente es: {numeroDia}");
            }
            else
            {
                Console.WriteLine("Día no válido. Por favor, introduzca un día de la semana correcto.");
            }
        }

    }
}
