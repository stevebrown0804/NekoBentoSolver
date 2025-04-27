using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NekoBentoSolver
{
    public static class Solver
    {
        //Method(s)
        //public static (bool, Gameboard) Solve(Gameboard gameboard, GamePieces gamePieces, PieceTracking pieceTracker)
        public static (bool, Gameboard) Solve(Gameboard gameboard, GamePieces gamePieces)
        {
            //Error checking
            if (gameboard == null || gamePieces == null) throw new Exception("Solver.Solve says: One (or both) of gameboard and/or gamePieces appears to be null -- and that, sir, is no good.");

            if (gameboard.UnfilledSquares != gamePieces.CoverageCount) throw new Exception($"The number of squares coverable by the bag of pieces ({gamePieces.CoverageCount}) doesn't equal the number of unfilled squares on the gameboard ({gameboard.UnfilledSquares})...and that seems like a problem.");
            //END error checking

            //stash the original gameboard, as we'll be overwriting its 'occupied' variables each pass of the solver
            Gameboard originalGameboard = gameboard.MakeACopy();
            //GamePieces originalUnrotatedUnpermutedGamePieces = gamePieces; //Still not sure if we need a MakeACopy or even hang on to this reference at all.  TBD!

            //Console.WriteLine($"Pre-rotation gamePieces.Pieces.Count: {gamePieces.Pieces.Count}");
            //uint multFactor = (uint) Helper.DetermineMultiplicativeFactorForPermutations(gamePieces);
            //UInt64 numPermutations = Helper.Factorial(gamePieces.Pieces.Count) * multFactor;
            //Console.WriteLine($"Multiplicative factor for rotations is: {multFactor}");
            //Console.WriteLine($"Total number of permutations is expected to be: {numPermutations}");

            //PREPPING FOR PERMUTATIONS
            //[btw] ChatGPT mentioned a "classic recursive solver" style of program...and that sounds like exactly what we need.
            //TODO: Recursively try the four rotations of non-rectangle filling pieces, I guess? TBD

            //rotate the pieces, adding the rotated versions to the bag
            GamePieces rotatedButUnpermutedPieces = new(gamePieces.RotatePiecesAndAddToTheBag());
            //END prep work

            Console.WriteLine($"rotatedButUnpermutedPieces contains {rotatedButUnpermutedPieces.Pieces.Count} items.");
            UInt64 estimatedNumPermutations = Helper.Factorial(rotatedButUnpermutedPieces.Pieces.Count);
            Console.WriteLine($"Estimated number of permutations is therefore: {estimatedNumPermutations:N0}");
            var bagOfBags = Helper.GetPermutations(rotatedButUnpermutedPieces.Pieces);

            //CODE STORAGE: 'materializing' the...enumerator?  not sure how to put it.  Taking the result of GetPermutations() and calling ToList() on it.
            //var bagOfBags = Helper.GetPermutations(rotatedButUnpermutedPieces.Pieces).ToList();
            //Console.WriteLine($"bagOfBags contains {bagOfBags.Count} items.");

            UInt64 bagCount = 0;
            bool solutionFound = false;
            Stopwatch singleBagTiming = new();
            uint numBagsToAverageOver = 20000;

            /*Parallel.ForEach(bagOfBags, aBag =>
            {*/

            foreach (var aBag in bagOfBags)
            {
                bagCount += 1;
                //Console.WriteLine($"Trying bag {bagCount}");
                if (bagCount % 1_000_000 == 0){
                    Console.WriteLine($"Trying bag {bagCount:N0} ({(100 * (double)bagCount / (double)estimatedNumPermutations):F2}%)");
                }

                //ADDING: We're going to take the timing of a 'small' number of bags, find the per-bag average and multiply that by the estimated number of permutations, thereby estimating the time that the program will take.  *thumbs up*
                //TODO, EVENTUALLY: Wrap this in a if(verbosity <= #)
                if (bagCount == 1)
                {
                    singleBagTiming.Start();
                }
                if(bagCount == numBagsToAverageOver + 1)
                {
                    singleBagTiming.Stop();
                    double estDurationPerBag = singleBagTiming.ElapsedMilliseconds / (double)numBagsToAverageOver;
                    Console.WriteLine($"singleBagTiming says: A single bag took {estDurationPerBag}ms (on avg).");
                    double estTotalDuration = estDurationPerBag * estimatedNumPermutations;
                    Console.WriteLine($"The estimated duration of processing all bags is: {estTotalDuration:F1}ms");
                }
                //END recently added

                gamePieces = new GamePieces(aBag);
                int totalPieces = gamePieces.RemainingPieces;  //atm this is the total number of pieces

                //CODE STORAGE: Using Helper.ShapeToString, which turns a bool[,] to a string
                //We're going to try to identify bags that might actually work to step through them
                /*if (gamePieces.Pieces[0].PieceShape != null)
                {
                    string pieceString = Helper.ShapeToString(gamePieces.Pieces[0].PieceShape);

                    //Console.WriteLine($"pieceString: {pieceString}");

                    if (gamePieces.Pieces[0].ThreeCharacterName == "LSS"
                        && pieceString != "1111\r\n0001\r\n" *//*(0deg)*//* 
                        && pieceString != "01\r\n01\r\n01\r\n11\r\n" *//*(90deg)*//*
                        && pieceString != "1111\r\n1000\r\n" *//*(180deg)*//*
                            //180deg seems like it should be "1000\r\n1111\r\n"
                        *//*{ { true, true, true, true }, { false, false, false, true } }*/
                        /*{ { false, true, false, true}, { false, true, true, true } }*//*
                        && (gamePieces.Pieces[1].ThreeCharacterName == "CCC" || gamePieces.Pieces[1].ThreeCharacterName == "PPP")
                        && gamePieces.Pieces[2].ThreeCharacterName == "TTT"
                        && (gamePieces.Pieces[3].ThreeCharacterName == "CCC" || gamePieces.Pieces[3].ThreeCharacterName == "PPP")
                    )
                    {
                        Console.WriteLine("Solver.Solve says: Candidate bag found!  Let's step through the code and see what happens.");
                    }
                }*/
                //END CODE STORAGE
                
                while (!solutionFound)
                {
                    //A quick check before we take out a piece...
                    /*if (gamePieces.RemainingPieces == 0)
                    {
                        if (Globals.verbosity >= 1)
                        {
                            Console.WriteLine("RemainingPieces == 0 but haven't found a solution yet. Iterating.");
                        }
                        //When did this one happen again?  It must have happened at ~some point~, otherwise why would we have written it. Hmm...
                        //Possibly causes:
                        //  Forgot to add pieces when we set up the level...except then the 'coverageCounter' or w/e it's called would have been caught elsewhere
                        //  Overwriting filled/occupied squares?  I don't think we have that problem nowadays.
                        //  Anything else?  Seems like maybe we could comment this out.
                        goto IterateTheForeachLoop;
                    }*/

                    //Alright, let's take out a piece....
                    GamePieceDescriptor currentPiece = gamePieces.RemoveFromBag(gamePieces.Pieces.First());

                    //...and start to check for places on the board where it can be placed
                    bool DidTheForLoopsTerminateNaturally = true;  //Reminder: if the for loops complete, that means we checked all positions on the gameboard without finding somewhere to place the piece
                    for (int currentRow = 1; currentRow <= gameboard.Rows; currentRow++)
                    {
                        for (int currentColumn = 1; currentColumn <= gameboard.Cols; currentColumn++)
                        {                            
                            //...then place it, if possible
                            //if (gameboard.CanThePieceBePlaced(rotatedPiece, currentRow, currentColumn, pieceTracker))
                            if (gameboard.CanThePieceBePlaced(currentPiece, currentRow, currentColumn))
                            {
                                gameboard.PlaceAPiece(gamePieces, currentPiece, currentRow, currentColumn);

                                if (Globals.verbosity >= 1)
                                {
                                    Console.WriteLine($"Solver.Solve says: A {currentPiece.FullName} has been placed at {currentRow}, {currentColumn}.");
                                    gameboard.PrintGameboard();
                                }

                                DidTheForLoopsTerminateNaturally = false;
                                goto DoneCheckingTheBoardWithThisPiece;
                            }
                            else
                            {
                                if (Globals.verbosity >= 2)
                                {
                                    Console.WriteLine($"Solver.Solve says: Unable to place {currentPiece.FullName} at {currentRow}, {currentColumn}.");
                                }
                            }

                        }//END for currentColumn
                    }//END for currentRow

                DoneCheckingTheBoardWithThisPiece:
                    if (DidTheForLoopsTerminateNaturally)
                    {
                        if (Globals.verbosity >= 1)
                        {
                            Console.WriteLine("Out of squares on the gameboard but haven't placed all the pieces; thus, no solution exists for this particular bag of pieces. Let's get a new bag.");
                        }
                        goto IterateTheForeachLoop;
                    }
                    else if (gameboard.UnoccupiedUnfilledSquares == 0)
                    {
                        if (Globals.verbosity >= 1)
                        {
                            Console.WriteLine("SUCCESS: All squares are either filled or occupied; we're done with this puzzle.");
                        }
                        solutionFound = true;
                        goto Terminate;
                    }
                    else if (gamePieces.RemainingPieces == 0) // ...but a sol'n WASN'T found (as that would have been identified by the previous if)
                    {
                        throw new Exception("Solver.Solve() says: Pretty sure something went wrong; we just placed a piece and no pieces remain, but there are unoccupied/unfilled squares and the puzzle isnt solved.  Let's check the level setup, if this happens.");
                    }

                }//END while (!solutionFound)

            IterateTheForeachLoop:
                if(Globals.verbosity >= 1)
                {
                    Console.WriteLine("Resetting the gameboard.");
                }
                gameboard = originalGameboard.MakeACopy();
                //pieceTracker.Reset();

            }//END foreach

        //});

        Terminate:
            Console.WriteLine($"It took {bagCount:N0} bags to solve that level, btw.");
            return (solutionFound, gameboard);
        
        }//END Solve()


        //Let's try reducing Solve() to some comments:
        //Solve(){
        //Stash the gameboard
        //Add the rotations of each piece to the bag
        //Permute the bag (to get a 'bag of bags')
        //For each bag in the bag of bags...
        //  Make a GamePieces object out of that bag
        //  while (!solutionFound){
        //      Remove a piece from the bag
        //      for each square on the board....
        //          Can the piece be placed there?
        //          If it can, place it and abort the for loops.
        //          If it can't, proceed to the next square on the board.
        //      Check to see if we're solved the puzzled or if we need to get a new bag.
        //      If we ~do~ need to get a new bag, reset the gameboard.
        //}

    }//END class Solver

}//END (namespace block)
