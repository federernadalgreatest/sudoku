// See https://aka.ms/new-console-template for more information
using sudoku;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

Console.WriteLine("Hello, World!");
int[,] sudokuGrid = {
{3, 0, 6, 5, 0, 8, 4, 0, 0},
{5, 2, 0, 0, 0, 0, 0, 0, 0},
{0, 8, 7, 0, 0, 0, 0, 3, 1},
{0, 0, 3, 0, 1, 0, 0, 8, 0},
{9, 0, 0, 8, 6, 3, 0, 0, 5},
{0, 5, 0, 0, 9, 0, 6, 0, 0},
{1, 3, 0, 0, 0, 0, 2, 5, 0},
{0, 0, 0, 0, 0, 0, 0, 7, 4},
{0, 0, 5, 2, 0, 6, 3, 0, 0}
};
int[,] sudokuGrid2 = {{ 5,3,0,0,7,0,0,0,0},
          { 6,0,0,1,9,5,0,0,0},
          { 0,9,8,0,0,0,0,6,0},
          { 8,0,0,0,6,0,0,0,3},
          { 4,0,0,8,0,3,0,0,1},
          { 7,0,0,0,2,0,0,0,6},
          { 0,6,0,0,0,0,2,8,0},
          { 0,0,0,4,1,9,0,0,5},
          { 0,0,0,0,8,0,0,7,9}};

for (int i = 0; i < 3; i++)
{
    Stopwatch stopWatch = new Stopwatch();

    stopWatch.Start();


    resoudreSudoku(sudokuGrid);
    //resoudreSudoku(sudokuGrid2);

    stopWatch.Stop();
    TimeSpan ts = stopWatch.Elapsed;

    // Format and display the TimeSpan value.
    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
        ts.Hours, ts.Minutes, ts.Seconds,
        ts.Milliseconds);

    Console.WriteLine("RunTime " + elapsedTime);
}




void resoudreSudoku(int [,] p_grid)
{
    Coordonnees coordonnees = new Coordonnees();
    coordonnees.AxeX = 0;
    coordonnees.AxeY = 0;
    bool estResout = trouverChiffreCoordonnee(coordonnees, p_grid,0);
    Console.WriteLine(estResout);
}


bool trouverChiffreCoordonnee(Coordonnees p_coordonnees, int[,] p_grid,int p_nbCasesParcourus)
{
    p_nbCasesParcourus++;
    if (p_grid[p_coordonnees.AxeX, p_coordonnees.AxeY] == 0)
    {
        List<int> nbDisponible = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        eliminerChiffresCarreActuel(p_coordonnees, nbDisponible, p_grid);
        for (int x = 0; x < 9; x++)
        {
            if (p_grid[x, p_coordonnees.AxeY] != 0)
            {
                nbDisponible.Remove(p_grid[x, p_coordonnees.AxeY]);
            }
        }
        for (int y = 0; y < 9; y++)
        {
            if (p_grid[p_coordonnees.AxeX, y] != 0)
            {
                nbDisponible.Remove(p_grid[p_coordonnees.AxeX, y]);
            }
        }

        if (nbDisponible.Count == 0)
        {
            return false;
        }

        if (nbDisponible.Count == 1)
        {
            p_grid[p_coordonnees.AxeX, p_coordonnees.AxeY] = nbDisponible[0];
            p_nbCasesParcourus = 1;
        }

        if (nbDisponible.Count == 2 && p_nbCasesParcourus > 80)
        {
            Random rand = new Random();
            double nb = rand.NextDouble();
            nb = Math.Round(nb);
            for (int i = 0;i< nbDisponible.Count; i++)
            {
                Coordonnees temporaireCoord = new Coordonnees();
                temporaireCoord.AxeX = p_coordonnees.AxeX;
                temporaireCoord.AxeY = p_coordonnees.AxeY;
                int[,] temporaireGrille = (int[,])p_grid.Clone();

                if (nb == i)
                {
                    temporaireGrille[p_coordonnees.AxeX, p_coordonnees.AxeY] = nbDisponible[(int)nb];
                }
                else
                {
                    temporaireGrille[p_coordonnees.AxeX, p_coordonnees.AxeY] = nbDisponible[i];
                }

                if (trouverChiffreCoordonnee(temporaireCoord, temporaireGrille, p_nbCasesParcourus))
                {
                    i = nbDisponible.Count;
                    p_grid = temporaireGrille;
                }
            }
        }
    }

    if (p_coordonnees.AxeX < 8)
    {
        p_coordonnees.AxeX++;
    }
    else if (p_coordonnees.AxeY < 8 && p_coordonnees.AxeX == 8)
    {
        p_coordonnees.AxeX = 0;
        p_coordonnees.AxeY++;
    }
    else
    {
        p_coordonnees.AxeX = 0;
        p_coordonnees.AxeY = 0;
    }
   
    if(grilleContientZero(p_grid) == false)
    {
        //afficherGrille(p_grid);
        return true;
    }

    return trouverChiffreCoordonnee(p_coordonnees, p_grid, p_nbCasesParcourus); ; 
}


void eliminerChiffresCarreActuel(Coordonnees p_coord, List<int> p_nbDisponible, int[,] p_grid)
{
    int indiceDepartX = 0;
    int indiceDepartY = 0;

    if (p_coord.AxeX < 6 && p_coord.AxeX > 2)
    {
        indiceDepartX = 3;
    }
    if (p_coord.AxeX > 5)
    {
        indiceDepartX = 6;
    }

    if (p_coord.AxeY < 6 && p_coord.AxeY > 2)
    {
        indiceDepartY = 3;
    }
    if (p_coord.AxeY > 5)
    {
        indiceDepartY = 6;
    }

    for (int x = indiceDepartX; x < indiceDepartX+3; x++)
    {
        if (p_grid[x, indiceDepartY] != 0)
        {
            p_nbDisponible.Remove(p_grid[x, indiceDepartY]);
        }
        for (int y = indiceDepartY; y < indiceDepartY + 3; y++)
        {
            if (p_grid[x, y] != 0)
            {
                p_nbDisponible.Remove(p_grid[x, y]);
            }
        }
    }
}


bool grilleContientZero(int[,] p_grid)
{
    bool contientZero = false;
    for (int x = 0; x < 9; x++)
    {
        for (int y = 0; y < 9; y++)
        {
            if (p_grid[x, y] == 0)
            {
                contientZero = true;
                x = 9;
                y = 9;
            }
        }
    }

    return contientZero;
}


void afficherGrille(int[,] p_grid)
{
    Console.WriteLine();
    Console.WriteLine("----------------------------------------");
    Console.WriteLine();
    for (int y = 0;  y <9 ; y++)
    {
        for (int x = 0; x< 9; x++)
        {
            if (x < 8)
            {
                Console.Write(p_grid[x, y]);
            }
            else
            {
                Console.WriteLine(p_grid[x, y]);
            }
        }
    }
}
