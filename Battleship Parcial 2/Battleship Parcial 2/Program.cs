//Variables para el tablero.
int barcosRestantes2;
int barcosColocados = 0;
int[,] tablero;
int[,] tablero2;
int[,] tableros;
int intentos;
int fila, columna;
//Variables para los jugadores
List<string> nombres = new List<string>();
int jugadores = 0;
bool continuar = true;
int intentosJuegoActual = 0;
Dictionary<string, List<int>> puntajes = new Dictionary<string, List<int>>();
int opcion;
string ganador = "";
string perdedor = "";
void mensajeinicial()
{
    Console.Clear();
    Console.WriteLine("\t\t\tBIENVENIDO A BATTLESHIP IN C#\n\t\tFavor de ingrese la opción que desea elegir");
    Console.WriteLine("\t1) Jugar.\n\t2)Mostrar Instrucciones.\n\t3)Salir\n");
}
void instrucciones()
{
    Console.WriteLine("\t\t¡Instrucciones del juego!\n");
    Console.WriteLine("\t1)El objetivo del juego es hundir los 5 barcos que se encuentran ocultos en el tablero.");
    Console.WriteLine("\t2)Para jugar, debes ingresar las coordenadas de una casilla del tablero (fila y columna).");
    Console.WriteLine("\t3)Si en esa casilla se encuentra un barco, se considerará un golpe.");
    Console.WriteLine("\t4)Si no hay un barco, se considerará un fallo.");
    Console.WriteLine("\t5)El juego termina cuando se hunden todos los barcos.");
    Console.WriteLine("\t6)Si elegiste el modo de 2 jugadores, el jugador 2 empezará a jugar cuando el \n\tjugador 1 haya hundido todos los barcos");
    Console.WriteLine("\tPresione Enter para continuar");
    Console.ReadKey();
    battleship();
}
void players()
{   Console.Clear();
    Console.WriteLine("\t¿Cuántos jugadores?\n\t1) Un Jugador\n\t2) Dos jugadores");

    while (!int.TryParse(Console.ReadLine(), out jugadores) || (jugadores != 1 && jugadores != 2))
    {
        Console.WriteLine("\tERROR, elige una opción válida");
    }
    for (int i = 0; i < jugadores; i++)
    {
        Console.WriteLine($"\tIngrese el nombre del jugador {i + 1}");
        nombres.Add(Console.ReadLine());
    }
    for (int i = 0; i < jugadores; i++)
    {
        Console.WriteLine($"\t Jugador {i + 1} {nombres[i]}\n");
    }
}
void Paso1_CrearTablero()
{

    int filas, columnas;
    Console.WriteLine("\tElija la dificultad del juego");
    Console.WriteLine("\t1) Fácil (tablero de 5x5 y se generan 5 barcos)");
    Console.WriteLine("\t2) Normal (tablero de 10x10 y se generan 7 barcos)");
    Console.WriteLine("\t3) Difícil (tablero de 15x15 y se generan 9 barcos)");
    int opcion = int.Parse(Console.ReadLine());
    switch (opcion)
    {
        case 1:
            filas = columnas = 5;
            break;
        case 2:
            filas = columnas = 10;
            break;
        case 3:
            filas = columnas = 15;
            break;
        default:
            Console.WriteLine("Opción inválida. Seleccionando tamaño por defecto (5x5)");
            filas = columnas = 5;
            break;
    }
    tablero = new int[filas, columnas];
    tablero2 = new int[filas, columnas];
    tableros = new int[filas, columnas];
    for (int p = 0; p <= jugadores; p++)
    {
        for (int f = 0; f < tablero.GetLength(0); f++)
        {
            for (int c = 0; c < tablero.GetLength(1); c++)
            {
                tablero[f, c] = 0;
            }
        }
    }

    // Inicializar tablero2
    if (jugadores == 2)
    {
        for (int f = 0; f < tablero2.GetLength(0); f++)
        {
            for (int c = 0; c < tablero2.GetLength(1); c++)
            {
                tablero2[f, c] = 0;
            }
        }
    }
}
void Paso2_ColocarBarcos()
{
    Random rand = new Random();

    switch (tableros.GetLength(0))
    {
        case 5: // Fácil (5 barcos)
            while (barcosColocados < 5)
            {
                int fila = rand.Next(0, 5);
                int columna = rand.Next(0, 5);

                if (tablero[fila, columna] == 0)
                {
                    tablero[fila, columna] = 1;
                    barcosColocados++;
                }
            }
            break;
        case 10: // Normal (7 barcos)
            while (barcosColocados < 7)
            {
                int fila = rand.Next(0, 10);
                int columna = rand.Next(0, 10);

                if (tablero[fila, columna] == 0)
                {
                    tablero[fila, columna] = 1;
                    barcosColocados++;
                }
            }
            break;
        case 15: // Difícil (9 barcos)
            while (barcosColocados < 9)
            {
                int fila = rand.Next(0, 15);
                int columna = rand.Next(0, 15);

                if (tablero[fila, columna] == 0)
                {
                    tablero[fila, columna] = 1;
                    barcosColocados++;
                }
            }
            break;
        default:
            Console.WriteLine("Error en la selección del tablero.");
            break;
    }

    // Si jugamos con dos jugadores, colocamos los barcos del jugador 2 en el segundo tablero
    if (jugadores == 2)
    {
        barcosColocados = 0;
        switch (tableros.GetLength(0))
        {
            case 5: // Fácil (5 barcos)
                while (barcosColocados < 5)
                {
                    int fila = rand.Next(0, 5);
                    int columna = rand.Next(0, 5);

                    if (tablero2[fila, columna] == 0)
                    {
                        tablero2[fila, columna] = 1;
                        barcosColocados++;
                    }
                }
                break;
            case 10: // Normal (7 barcos)
                while (barcosColocados < 7)
                {
                    int fila = rand.Next(0, 10);
                    int columna = rand.Next(0, 10);

                    if (tablero2[fila, columna] == 0)
                    {
                        tablero2[fila, columna] = 1;
                        barcosColocados++;
                    }
                }
                break;
            case 15: // Difícil (9 barcos)
                while (barcosColocados < 9)
                {
                    int fila = rand.Next(0, 15);
                    int columna = rand.Next(0, 15);

                    if (tablero2[fila, columna] == 0)
                    {
                        tablero2[fila, columna] = 1;
                        barcosColocados++;
                    }
                }
                break;
            default:
                Console.WriteLine("Error en la selección del tablero.");
                break;
        }
    }
    Console.WriteLine("Barcos colocados en el tablero.");
}

void Paso3_ImprimirTablero()
{
    Console.Clear();
    string caracter_a_imprimir = "";
    for (int f = 0; f < tablero.GetLength(0); f++)
    {
        for (int c = 0; c < tablero.GetLength(1); c++)
        {
            switch (tablero[f, c])
            {
                case 0:
                    caracter_a_imprimir = "\t-";
                    break;
                case 1:
                    caracter_a_imprimir = "\t-";
                    break;
                case -1:
                    caracter_a_imprimir = "\t*";
                    break;
                case -2:
                    caracter_a_imprimir = "\tX";
                    break;
                default:
                    caracter_a_imprimir = "\t-";
                    break;
            }
            Console.Write(caracter_a_imprimir + " ");
        }
        Console.WriteLine();
    }
}
void MultiImprimirTablero()
{
    Console.Clear();
    string caracter_a_imprimir = "";
    for (int f = 0; f < tablero2.GetLength(0); f++)
    {
        for (int c = 0; c < tablero2.GetLength(1); c++)
        {
            switch (tablero2[f, c])
            {
                case 0:
                    caracter_a_imprimir = "\t-";
                    break;
                case 1:
                    caracter_a_imprimir = "\t-";
                    break;
                case -1:
                    caracter_a_imprimir = "\t0";
                    break;
                case -2:
                    caracter_a_imprimir = "\tX";
                    break;
                default:
                    caracter_a_imprimir = "\t-";
                    break;
            }
            Console.Write(caracter_a_imprimir + " ");
        }
        Console.WriteLine();
    }
}
void paso4Multi()
{
        bool[,] coordenadasIngresadas2 = new bool[tablero2.GetLength(0), tablero2.GetLength(1)];
        Console.Clear();
        MultiImprimirTablero(); barcosRestantes2 = barcosColocados;
        intentosJuegoActual = 0;
    for (int p = 1; p < jugadores; p++)
    {
        do
        {
            Console.WriteLine("\tIntentos: " + intentosJuegoActual + " - Barcos restantes: " + barcosRestantes2);
            Console.WriteLine($"\tTurno del jugador {nombres[p]}");

            Console.Write("\tIngresa la fila: ");
            while (!int.TryParse(Console.ReadLine(), out fila) || fila < 0 || fila >= tablero2.GetLength(0))
            {
                Console.WriteLine("\tError: Ingresa una fila válida");
                Console.Write("\tIngresa la fila: ");
            }

            Console.Write("\tIngresa la columna: ");
            while (!int.TryParse(Console.ReadLine(), out columna) || columna < 0 || columna < 0 || columna >= tablero2.GetLength(1))
            {
                Console.WriteLine("\tError: Ingresa una columna válida");
                Console.Write("\tIngresa la columna: ");
            }

            if (coordenadasIngresadas2[fila, columna])
            {
                Console.WriteLine("\tCoordenada ya ingresada");
            }
            else
            {
                coordenadasIngresadas2[fila, columna] = true;

                if (tablero2[fila, columna] == 1)
                {
                    Console.Beep();
                    tablero2[fila, columna] = -1;
                    barcosRestantes2--;
                    Console.WriteLine("\t¡Golpeaste un barco!");
                }
                else
                {
                    tablero2[fila, columna] = -2;
                    Console.WriteLine("\tFallaste");
                }
                intentosJuegoActual++; // incrementar los intentos del juego actual
                MultiImprimirTablero();
            }
        } while (barcosRestantes2 > 0);
        if (!puntajes.ContainsKey(nombres[p]))
        {
            puntajes[nombres[p]] = new List<int>();
        }
        puntajes[nombres[p]].Add(intentosJuegoActual);
    }
    gameover();
}
void paso4_ingresoCoordenadas()
{
    bool[,] coordenadasIngresadas = new bool[tablero.GetLength(0), tablero.GetLength(1)];
    for (int p = 0; p < jugadores; p++)
    {
        Console.Clear();
        Paso3_ImprimirTablero();

        int barcosRestantes = barcosColocados; 
        do
        {
            Console.WriteLine("\tIntentos: " + intentosJuegoActual + " - Barcos restantes: " + barcosRestantes);
            Console.WriteLine($"\tTurno del jugador {nombres[p]}");

            Console.Write("\tIngresa la fila: ");
            while (!int.TryParse(Console.ReadLine(), out fila) || fila < 0 || fila >= tablero.GetLength(0))
            {
                Console.WriteLine("\tError: Ingresa una fila válida");
                Console.Write("\tIngresa la fila: ");
            }

            Console.Write("\tIngresa la columna: ");
            while (!int.TryParse(Console.ReadLine(), out columna) || columna < 0 || columna < 0 || columna >= tablero.GetLength(1))
            {
                Console.WriteLine("\tError: Ingresa una columna válida");
                Console.Write("\tIngresa la columna: ");
            }

            if (coordenadasIngresadas[fila, columna])
            {
                Console.WriteLine("\tCoordenada ya ingresada");
            }
            else
            {
                coordenadasIngresadas[fila, columna] = true;

                if (tablero[fila, columna] == 1)
                {
                    Console.Beep();
                    tablero[fila, columna] = -1;
                    barcosRestantes--;
                    Console.WriteLine("\t¡Golpeaste un barco!");
                }
                else
                {
                    tablero[fila, columna] = -2;
                    Console.WriteLine("\tFallaste");
                }
                intentosJuegoActual++; // incrementar los intentos del juego actual
                Paso3_ImprimirTablero();
            }
        } while (barcosRestantes > 0);
        if (!puntajes.ContainsKey(nombres[p]))
        {
            puntajes[nombres[p]] = new List<int>();
        }
        puntajes[nombres[p]].Add(intentosJuegoActual);
        if (jugadores == 2)
        {
            do
            {
                Console.WriteLine($"\n\tDESTRUÍSTE TODOS LOS BARCOS {nombres[p]} obtuviste: {intentosJuegoActual} puntos");
                Console.WriteLine($"\n\tPresiona Enter cuando {nombres[1]} esté listo");
                Console.ReadKey();
                paso4Multi();
            } while (barcosRestantes2 > 0);
        }
    }
gameover();
}
    void error()
    {
        Console.Clear();
        Console.WriteLine("\tVaya, parece que has seleccionado una opción errónea\n\tSelecciona un válida");
    }
    void battleship()
    {

        mensajeinicial();
        Console.WriteLine("\n\tIngresa tu opción");
        opcion = Convert.ToInt32(Console.ReadLine());
        switch (opcion)
        {
            case 1:
                players();
                Paso1_CrearTablero();
                Paso2_ColocarBarcos();
                Paso3_ImprimirTablero();
                paso4_ingresoCoordenadas();
                break;
            case 2:
                Console.Clear();
                instrucciones();
                Console.ReadKey();
                break;

            case 3:
                Console.Clear();
                Console.WriteLine("\tMUCHAS GRACIAS POR JUGAR, VUELVE PRONTO!");
                continuar = false;
                break;

            default:
                error();
                break;
        }
    }
void gameover()
{
    Console.WriteLine("\n\n\tFin del juego");
    Console.WriteLine("\tPuntajes finales:");

    int maxPuntaje = int.MinValue;
    int minPuntaje = int.MaxValue;

    foreach (string nombre in puntajes.Keys)
    {
        int totalPuntaje = puntajes[nombre].Sum();
        if (jugadores == 1)
        {
            Console.WriteLine($"\t{nombre}: {totalPuntaje}");
        }
        if (totalPuntaje > maxPuntaje)
        {
            maxPuntaje = totalPuntaje;
            perdedor = nombre;
        }

        if (totalPuntaje < minPuntaje)
        {
            minPuntaje = totalPuntaje;
            ganador = nombre;
        }
    }
    if (jugadores == 2 && maxPuntaje != minPuntaje)
    {
        Console.WriteLine($"\tEl primer lugar con la menor cantidad de intentos es de {ganador} con: {minPuntaje} intentos");
        Console.WriteLine($"\n\tEl segundo lugar con la mayor cantidad de intentos es de {perdedor} con: {maxPuntaje} intentos");
    }
    if (jugadores == 2 && maxPuntaje == minPuntaje)
    {
        Console.WriteLine($"\t\tHa sido un empate entre{nombres[0]} y {nombres[1]} ");
    }
    Console.WriteLine("\n\n\tPresione cualquier tecla para salir...");
    Console.ReadKey();
    menufinal:
    Console.WriteLine("\n\tSELECCIONE LA OPCIÓN QUE DESEA\n\t1)Salir del juego.\n\t2)Volver al menú principal.\n\t3)Jugar de nuevo.");
    while (!int.TryParse(Console.ReadLine(), out opcion) || (opcion != 1 && opcion != 2 && opcion != 3))
    {   
        Console.Clear();
        Console.WriteLine("\tERROR, elige una opción válida");
        goto menufinal;
    }
    switch (opcion)
    {
        case 1:
            Console.Clear();
            Console.WriteLine("\n\t\tMUCHAS GRACIAS POR JUGAR, VUELVE PRONTO! :)");
            continuar = false;
            break;

        case 2:
            battleship();
            break;

        case 3:
            players();
            Paso1_CrearTablero();
            Paso2_ColocarBarcos();
            Paso3_ImprimirTablero();
            paso4_ingresoCoordenadas();
            break;

        default:
            error();
            break;
    }
}
battleship();