using Raylib_cs;
using System.Numerics;

namespace Conway_s_Game_of_Life
{
    internal class Program
    {
        const int FPS = 1;

        const int WIDTH = 64;
        const int HEIGHT = 64;
        
        const int SCALE = 10; // Pixel size.

        static void Main(string[] args)
        {
            bool Start = false

            // Initializing the window.
            Raylib.InitWindow(WIDTH * SCALE, HEIGHT * SCALE, "Conway's Game of Life");
            Raylib.SetTargetFPS(FPS);

            // Representing the screen within an array.
            bool[,] screen = new bool[WIDTH, HEIGHT];

            //Test:
            screen[20, 20] = true;
            screen[20, 22] = true;
            screen[20, 21] = true;

            // Main loop.
            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.WHITE);

                if (Start)
                {
                    Draw(screen);
                    screen = NextCycle(screen);
                }
                else
                {
                    if(Raylib.IsMouseButtonPressed(0)) 
                    {
                        Vector2 pos = Raylib.GetMousePosition();
                    }
                }
                

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }

        static bool[,] NextCycle(bool[,] screen) 
        {
            bool[,] returnable = screen.Clone() as bool[,];

            for(int i = 1; i < WIDTH - 1; i++)
            {
                for(int j = 1; j < HEIGHT - 1; j++)
                {
                    // Check the number of the living neighbours.
                    int counter = 0;

                    if (screen[i + 1, j])
                        counter++;
                    if (screen[i - 1, j])
                        counter++;
                    if (screen[i + 1, j + 1])
                        counter++;
                    if (screen[i + 1, j - 1])
                        counter++;
                    if (screen[i - 1, j + 1])
                        counter++;
                    if (screen[i - 1, j - 1])
                        counter++;
                    if (screen[i, j + 1])
                        counter++;
                    if (screen[i, j - 1])
                        counter++;

                    if (screen[i, j]) // Alive cell.
                    {
                        if (counter > 3 || counter < 2)
                            returnable[i, j] = false;
                    }
                    else // Dead cell.
                    {
                        if(counter == 3)
                            returnable[i, j] = true;
                    }
                }
            }


            return returnable;
        }

        static void Draw(bool[,] screen)
        {
            for (int i = 1; i < WIDTH - 1; i++)
            {
                for (int j = 1; j < HEIGHT - 1; j++)
                {
                    if (screen[i, j])
                        Raylib.DrawRectangle(i * SCALE, j * SCALE, SCALE, SCALE, Color.BLACK);
                }
            }
        }
    }
}