using Raylib_cs;
using System.Numerics;

namespace Conway_s_Game_of_Life
{
    internal class Program
    {
        const int InFPS = 64;
        
        const int SCALE = 10; // Pixel size.

        static private bool start = false;

        static public bool Start
        {
            get { return start; }
            set { start = value; }
        }

        static private int ofps = 4;

        static public int OutFPS
        {
            get { return ofps = 4; }
            set { ofps = value; }
        }

        static private int width = 64;

        static public int WIDTH
        {
            get { return width; }
            set { width = value; }
        }

        static private int height = 64;

        static public int HEIGHT
        {
            get { return height; }
            set { height = value; }
        }



        static void Main(string[] args)
        {
            Console.Title = "Conway's Game of life Console";

            // Start the application.
            Console.WriteLine("Welcome to Conway's game of life.");
            Console.Write("Enter the width of the window you want to work with (in pixels): ");
            WIDTH = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter the height of the window you want to work with (in pixels): ");
            HEIGHT = Convert.ToInt32(Console.ReadLine());

            // Initializing the window.
            Raylib.InitWindow(WIDTH * SCALE, HEIGHT * SCALE, "Conway's Game of Life");
            Raylib.SetTargetFPS(InFPS);

            // Representing the screen within an array.
            bool[,] screen = new bool[WIDTH, HEIGHT];

            Console.Clear();
            Console.WriteLine("Welcome to Conway's game of life! \n Wikipedia Article: https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life \n To get a list of commands press H. \n P.s, Long press the key to get the command working.");

            // Main loop.
            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.WHITE);

                if (Start)
                    screen = NextCycle(screen);

                screen = GetInput(screen);

                Draw(screen);
                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }

        static bool[,] NextCycle(bool[,] screen) 
        {

            bool[,] returnable = (bool[,])screen.Clone();

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

        static bool[,] GetInput(bool[,] screen)
        {
            Vector2 pos = Raylib.GetMousePosition();
            int i = (int)Math.Floor(pos.X / SCALE);
            int j = (int)Math.Floor(pos.Y / SCALE);

            if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT) && (i > 0 && i < WIDTH && j > 0 && j < HEIGHT)) // Add pixels
                screen[i, j] = true;

            else if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_RIGHT_BUTTON)) // Delete pixels
                screen[i, j] = false;

            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE)) // Start the animation
            {
                Start = true;
                Raylib.SetTargetFPS(OutFPS);
                Console.WriteLine("Animation is on.");
            }

            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_X) && Start == true) // Stop the animation
            {
                Start = false;
                Raylib.SetTargetFPS(InFPS);
                Console.WriteLine("Animation is off.");
            }

            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_RIGHT) && Start == false) // Proceed to the next cycle
                screen = NextCycle(screen);

            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_ESCAPE))
                Environment.Exit(0);

            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_H))
                PrintCommands();

            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_F))
            {
                Start = false;
                Console.WriteLine("Animation is off.");
                Console.Write("Enter the new simulation FPS: ");
                OutFPS = Convert.ToInt32(Console.ReadLine());
            }

            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_C))
                screen = new bool[WIDTH, HEIGHT];

            return screen;
        }

        static void PrintCommands()
        {
            Console.WriteLine("\n\n");
            Console.WriteLine("C                  - Clears the grid."); 
            Console.WriteLine("Esc                - Closes the simulation Window.");
            Console.WriteLine("F                  - Changes the simulation FPS.");
            Console.WriteLine("H                  - Prints this list.");
            Console.WriteLine("Left mouse button  - Adds cells.");
            Console.WriteLine("Right arrow        - Shows the next cycle.");
            Console.WriteLine("Right mouse button - Removes cells.");
            Console.WriteLine("SPACE              - Starts the simulation.");
            Console.WriteLine("X                  - Stops the simulation.");
            Console.WriteLine("\n\n");
        }
    }
}