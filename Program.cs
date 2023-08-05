using Raylib_cs;

namespace Conway_s_Game_of_Life
{
    internal class Program
    {
        const int FPS = 60;

        const int WIDTH = 64;
        const int HEIGHT = 64;
        
        const int SCALE = 10; // Pixel size.

        static void Main(string[] args)
        {
            // Initializing the window.
            Raylib.InitWindow(WIDTH * SCALE, HEIGHT * SCALE, "Conway's Game of Life");
            Raylib.SetTargetFPS(FPS);

            // Representing the screen within an array.
            bool[,] screen = new bool[WIDTH, HEIGHT];

            // Main loop.
            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.WHITE);

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }

        static bool[,] NextCycle(bool[,] screen) 
        {
            bool[,] returnable = screen;

            for(int i = 1; i < WIDTH - 1; i++)
            {
                for(int j = 1; j < HEIGHT - 1; j++)
                {
                    // Check the number of the living neighbours.
                    short counter = 0;

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
                        if (counter > 3 || counter == 1)
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
    }
}