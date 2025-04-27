using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;


namespace NekoBentoSolver
{
    static public class Globals
    {
        static readonly public byte verbosity = 0;   //<--Set this here, programmer!
                        //Maybe someday we'll take a cmdline arg to set this...but until that day comes, set it here.
        //  0: Minimal
        //  1: Console logging
        //  2: Highly verbose console logging
        //  3: Obnoxiously verbose console logging
        //  4: Over-the-top verbose console logging
        //  5: Destructive console logging (eg. calling 'EmptyTheBag' but not refilling it before continuing, so the program throws)

        static readonly public bool lightMemoryUsage = true;
        static readonly public bool doTiming = true;        
    }
    //TODO, EVENTUALLY: Add some cmd-line args.  Xpac/level, verbosity, "light vs heavy memory usage," Timing [yes/no], ...(that's all that comes to mind at the moment)

    class NekoBentoSolverTLC
    {
        static void Main(string[] args)
        {
            try
            {
                //Stuff to get from cmd-line args, once we implement that
                ExpansionPack xpac = ExpansionPack.ThirdLevelPack; // ExpansionPack.Main; // 
                int level = 11; // 

                //bool doTiming = false;  //TODO: Implement doTiming
                //int verbosity = Globals.verbosity;  //TODO: Migrate this (verbosity to a cmd-line arg, eventually
                //bool lightMemoryUsage = Globals.lightMemoryUsage; //TODO: Implement this (lightMemoryUsage)

                //Other stuff
                Gameboard gameboard;
                GamePieces pieces;
                (gameboard, pieces) = LevelCreator.CreateLevel(xpac, level);
                //PieceTracking pieceTracker = new PieceTracking(gameboard.Rows, gameboard.Cols);

                if(Globals.verbosity >= 0)
                {
                    Console.WriteLine($"Solving expansion pack: {xpac}, Level: {level}...");
                }

                if(Globals.verbosity >= 0)
                {
                    Console.WriteLine("Starting gameboard:");
                    gameboard.PrintGameboard();
                }

                //At this point, we might check to make sure the list contains the piece we expect. WHAT SAY YOU!
                //  RETORT: I say: Or we could just start writing unit tests! WHAT SAY YOU NOW!
                //  RETORT TO RETORT: ooooh, I am slain, Horatio.

                if(Globals.verbosity >= 5)
                {
                    //TODO, MAYBE...EVENTUALLY Wouldn't this be better as a unit test (or two of them)?
                    Console.WriteLine($"Before calling EmptyTheBag(), we find that the number of game pieces is: {pieces.Pieces.Count}");
                    pieces.EmptyTheBag();
                    Console.WriteLine($"Having just called EmptyTheBag(), we now find that the number of game pieces is: {pieces.Pieces.Count}");
                    //That was fun.
                }

                //Start the stopwatch
                Stopwatch sw = Stopwatch.StartNew();

                //Solve!
                //(bool wasASolutionFound, Gameboard solution) = Solver.Solve(gameboard, pieces, pieceTracker); //woot! solve that puzzle
                (bool wasASolutionFound, Gameboard solution) = Solver.Solve(gameboard, pieces); //woot! solve that puzzle

                //...and stop the stopwatch.
                sw.Stop();
                if(Globals.verbosity >= 0)      //TODO, MAYBE Set this (the timing stuff) back to verbosity 1, if it gets annoying
                {
                    Console.WriteLine($"Elapsed time of Solver.Solve(): {sw.Elapsed /*Milliseconds*/} ms");
                }

                //...then display the results (of solving the puzzle).
                if (!wasASolutionFound)
                {
                    Console.Write("The last pass of the (unsolved) puzzle:\n");
                }
                else
                {
                    Console.Write("Solved puzzle:\n");
                }                
                solution.PrintGameboard();
            }
            catch (Exception e)
            {
                Console.WriteLine("NekoBentoSolver says: An error happened:\n" + e.ToString());
            }
        }
    }

} //namespace NekoBentoSolver
