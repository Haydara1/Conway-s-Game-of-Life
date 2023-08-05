using Raylib_cs;

namespace Conway_s_Game_of_Life
{
    internal class Program
    {
        const int FPS = 60;

        const int WIDTH = 64;
        const int HEIGHT = 64;

        static void Main(string[] args)
        {
            Raylib.InitWindow(WIDTH, HEIGHT, "Conway's Game of Life");
            Raylib.SetTargetFPS(FPS);

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.WHITE);

                

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }
    }
}