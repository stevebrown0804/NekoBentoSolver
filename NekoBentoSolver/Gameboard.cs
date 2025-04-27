using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NekoBentoSolver
{
    public class A_Single_Square
    {
        //Properties
        public int Row { get; set; }
        public int Column { get; set; }
        public bool Filled { get; set; }
        public bool Occupied { get; set; }
        public GamePieceType? PieceOccupiedBy { get; set; }
        
        //Constructors
        public A_Single_Square()
        {
            throw new("NG, yo.  There's no zero-arg constructor for A_Single_Square");
        }
        public A_Single_Square(int row, int col, bool filled)
        {
            if(Globals.verbosity >= 3)
            {
                Console.WriteLine("In A_Single_Square(int row, int col, bool filled) constructor");
            }            

            Row = row;
            Column = col;
            Filled = filled;
            Occupied = false;
        }
        public A_Single_Square(int row, int col)
        {
            if (Globals.verbosity >= 3)
            {
                Console.WriteLine("In A_Single_Square(int row, int col) constructor. Square is defaulting to 'filled = false.'");
            }

            Row = row;
            Column = col;
            Filled = false;
            Occupied = false;
        }
    } //class A_Single_Square

    public class Gameboard
    {
        //Properties
        public int Rows { get; set; }
        public int Cols { get; set; }
        public int UnfilledSquares { get; set; }
        public int UnoccupiedUnfilledSquares { get; set; }
        public int PlacedPieces { get; set; }

        //other fields
        private A_Single_Square[] theBoard;

        //Constuctors
        public Gameboard()
        {
            throw new Exception("Gameboard says: NG, sir.  A zero-arg constructor for Gameboard isn't defined.");
        }
        public Gameboard(int rows, int cols)
        {
            if(Globals.verbosity >= 3)
            {
                Console.WriteLine("In Gameboard(int, int) constructor");
            }           

            if (rows <= 0 || cols <= 0) throw new Exception("Gameboard(int, int) says: Rows and columns each need to be at least 1, yo.");

            Rows = rows;
            Cols = cols;
            UnfilledSquares = Rows * Cols;
            UnoccupiedUnfilledSquares = UnfilledSquares;
            PlacedPieces = 0;

            //Declare/define theBoard
            theBoard = new A_Single_Square[rows*cols];
            for (int i = 0; i < theBoard.Length; i++)
            {
                int row = (i / cols) + 1;
                int col = (i % cols) + 1;
                theBoard[i] = new A_Single_Square(row, col);
            }
        }

        //Methods
        public void FillSquare(int row, int col)
        {
            if(Globals.verbosity >= 2)
            {
                Console.WriteLine($"In Gameboard.FillSquare(); args are {row}, {col}");
            }

            if (row > Rows || col > Cols) throw new Exception("Gameboard.FillSquare() says: Index out of range, yo.");

            theBoard[(row - 1) * Cols + (col - 1)].Filled = true;

            UnfilledSquares -= 1;
            UnoccupiedUnfilledSquares -= 1;
        }

        public bool IsSquareFilled(int row, int col)
        {
            if (row > Rows || col > Cols) throw new Exception("GameBoard.IsSquareFilled() says: Index out of range, yo.");

            //return theBoard[row - 1].IsSquareFilled(col);
            return theBoard[(row - 1) * Cols + (col - 1)].Filled;
        }

        public void PrintGameboard()
        {
            if(Globals.verbosity >= 2)
            {
                Console.WriteLine("In Gameboard.PrintGameboard");
            }

            //Reminder: Start at the top row, then work down to 1
            for (int row = Rows; row >= 1; row--)
            {
                for (int col = 1; col <= Cols; col++)
                {
                    int index = (row - 1) * Cols + (col - 1);
                    A_Single_Square theSquare = theBoard[index];

                    if (theSquare.Filled)
                    {
                        Console.Write("xxx ");
                    }
                    else if (theSquare.Occupied)
                    {
                        string threeCharName;
                        switch (theSquare.PieceOccupiedBy)
                        {
                            case GamePieceType.Pea:
                                threeCharName = Lookups.LookUpThreeLetterPieceCode(GamePieceType.Pea); break;
                            case GamePieceType.RolledOmelette:
                                threeCharName = Lookups.LookUpThreeLetterPieceCode(GamePieceType.RolledOmelette); break;
                            case GamePieceType.Tomato:
                                threeCharName = Lookups.LookUpThreeLetterPieceCode(GamePieceType.Tomato); break;
                            case GamePieceType.Carrot:
                                threeCharName = Lookups.LookUpThreeLetterPieceCode(GamePieceType.Carrot); break;
                            case GamePieceType.Sushi:
                                threeCharName = Lookups.LookUpThreeLetterPieceCode(GamePieceType.Sushi); break;
                            case GamePieceType.Egg:
                                threeCharName = Lookups.LookUpThreeLetterPieceCode(GamePieceType.Egg); break;
                            case GamePieceType.Dango:
                                threeCharName = Lookups.LookUpThreeLetterPieceCode(GamePieceType.Dango); break;
                            case GamePieceType.Fillet:
                                threeCharName = Lookups.LookUpThreeLetterPieceCode(GamePieceType.Fillet); break;
                            case GamePieceType.LShapedSquid:
                                threeCharName = Lookups.LookUpThreeLetterPieceCode(GamePieceType.LShapedSquid); break;
                            case GamePieceType.LShapedBlueFish:
                                threeCharName = Lookups.LookUpThreeLetterPieceCode(GamePieceType.LShapedBlueFish); break;
                            case GamePieceType.OctopusWiener:
                                threeCharName = Lookups.LookUpThreeLetterPieceCode(GamePieceType.OctopusWiener); break;
                            case GamePieceType.OctopusTentacle:
                                threeCharName = Lookups.LookUpThreeLetterPieceCode(GamePieceType.OctopusTentacle); break;
                            case GamePieceType.BentWiener:
                                threeCharName = Lookups.LookUpThreeLetterPieceCode(GamePieceType.BentWiener); break;
                            case GamePieceType.ThreeProngThing:
                                threeCharName = Lookups.LookUpThreeLetterPieceCode(GamePieceType.ThreeProngThing); break;
                            case GamePieceType.Shrimp:
                                threeCharName = Lookups.LookUpThreeLetterPieceCode(GamePieceType.Shrimp); break;
                            case GamePieceType.ChickenLeg:
                                threeCharName = Lookups.LookUpThreeLetterPieceCode(GamePieceType.ChickenLeg); break;
                            case GamePieceType.LeafyMushroom:
                                threeCharName = Lookups.LookUpThreeLetterPieceCode(GamePieceType.LeafyMushroom); break;
                            case GamePieceType.FriedPrawn:
                                threeCharName = Lookups.LookUpThreeLetterPieceCode(GamePieceType.FriedPrawn); break;
                            case GamePieceType.Marshmallows:
                                threeCharName = Lookups.LookUpThreeLetterPieceCode(GamePieceType.Marshmallows); break;
                            case GamePieceType.PeaPods:
                                threeCharName = Lookups.LookUpThreeLetterPieceCode(GamePieceType.PeaPods); break;
                            case GamePieceType.Broccoli:
                                threeCharName = Lookups.LookUpThreeLetterPieceCode(GamePieceType.Broccoli); break;
                            case GamePieceType.Mushroom:
                                threeCharName = Lookups.LookUpThreeLetterPieceCode(GamePieceType.Mushroom); break;
                            case GamePieceType.Leaf:
                                threeCharName = Lookups.LookUpThreeLetterPieceCode(GamePieceType.Leaf); break;
                            case GamePieceType.UShapedLeaves:
                                threeCharName = Lookups.LookUpThreeLetterPieceCode(GamePieceType.UShapedLeaves); break;
                            case GamePieceType.SquareCucumbers:
                                threeCharName = Lookups.LookUpThreeLetterPieceCode(GamePieceType.SquareCucumbers); break;

                            default: throw new Exception("Gameboard.PrintRow() says: As-of-yet unimplemented piece type.)");
                        }
                        Console.Write($"{threeCharName} ");
                    }
                    else
                    {
                        Console.Write("___ ");
                    }
                }
                Console.WriteLine(); // Newline after each row
            }
        }

        public Gameboard MakeACopy()
        {            
            Gameboard copyGameboard = new Gameboard(Rows, Cols);

            for (int index = 0; index < Rows * Cols; index++)
            {
                copyGameboard.theBoard[index].Filled = theBoard[index].Filled;
                copyGameboard.theBoard[index].Occupied = theBoard[index].Occupied;
            }

            copyGameboard.UnfilledSquares = UnfilledSquares;
            copyGameboard.UnoccupiedUnfilledSquares = UnoccupiedUnfilledSquares;

            if(Globals.verbosity >= 3)
            {
                Console.WriteLine("Printing out copyGameboard:");
                copyGameboard.PrintGameboard();
            }
            if (Globals.verbosity >= 4)
            {
                Console.WriteLine("After printing out copyGameboard:");
            }

            return copyGameboard;
        }

        //public bool CanThePieceBePlaced(GamePieceDescriptor piece, int row, int col, PieceTracking pieceTracker)
        public bool CanThePieceBePlaced(GamePieceDescriptor piece, int row, int col)
        {
            //Console.WriteLine($"About to check a {piece.PieceType} at ({row},{col}).");

            //First we'll check with the PieceTracker to see if we've checked this piece's type at this square
            /*if (pieceTracker.PieceTracker[(row, col, piece.PieceType, piece.Rotation)] == 0)
            {
                //No, we haven't checked this piecetype/rotation yet

                //Console.WriteLine($"The dictionary entry was 0, so we haven't checked this square ({row},{col}) with this pieceType ({piece.PieceType}) and rotation ({piece.Rotation}) yet.  Proceeding!");

                pieceTracker.PieceTracker[(row, col, piece.PieceType, piece.Rotation)] = piece.ID;   //So, we'll mark it as having been checked (by registering the current piece's ID)
            }
            else
            {
                //Yes, we've already checked this piecetype/rotation
                return false;

                //Is it the current piece, though?  If so, let it pass.  If not, abort.
                *//*if (pieceTracker.PieceTracker[(row, col, piece.PieceType)] == (piece.ID, piece.Rotation))
                {
                    //go ahead!  this piece has dibs on checking this square
                    //Console.WriteLine($"Turns out, we've checked this square ({row},{col}) with the current piece (ID {piece.ID})!  (A different rotation though...hopefully.)");
                }
                else
                {
                    //What to do then?  Just...return? hmm....
                    //Console.WriteLine("Gameboard.CanThePieceBePlaced() says: We've already checked this piecetype in this spot (with a piece with a different ID). Let's not check again!");
                    //Console.WriteLine($"We've already check this piecetype{piece.PieceType} (but a different ID, {PieceTracker[(row, col, piece.PieceType)]}) at this square ({row},{col}). Aborting!");

                    return false;
                }*//*
            }*/

            //Next, check to see if it even fits on the gameboard
            if (row + (piece.ArrayDimensions.Item1 - 1) > Rows) return false;
            if (col + (piece.ArrayDimensions.Item2 - 1) > Cols) return false;

            if (piece.DoesThePieceFillTheArray)
            {
                //Then, check to see if all of the squares are unfilled and unoccupied
                for (int i = row; i < row + piece.ArrayDimensions.Item1; i++)
                {
                    for (int j = col; j < col + piece.ArrayDimensions.Item2; j++)
                    {
                        if (theBoard[(i - 1) * Cols + (j - 1)].Filled || theBoard[(i - 1) * Cols + (j - 1)].Occupied) return false;
                    }
                }

                if (Globals.verbosity >= 2)
                {
                    Console.WriteLine($"Gameboard.CanThePieceByPlaced() says: Good news! A {piece.PieceType} ~can~ be placed with lower-left corner ({row}, {col})");
                }
            }
            else //The piece does NOT fill the rectangle
            {
                if (piece.PieceShape == null) throw new Exception("piece.PieceShape is null, despite being in the !piece.DoesThePieceFillTheArray part of the code");
                //bool[,] pieceShape = piece.PieceShape;

                int lengthRows = piece.PieceShape.GetLength(0);
                int lengthCols = piece.PieceShape.GetLength(1);
                //Written by ChatGPT
                for (int i = 0; i < lengthRows; i++) // Piece row
                {
                    for (int j = 0; j < lengthCols; j++) // Piece col
                    {
                        if (!piece.PieceShape[i, j]) continue; // Skip gaps in the piece

                        int boardRow = row + i - 1;
                        int boardCol = col + j - 1;

                        // Check bounds for List access
                        if (boardRow < 0 || boardCol < 0 || boardRow >= Rows || boardCol >= Cols)
                            return false;

                        var aSquare = theBoard[(boardRow) * Cols + (boardCol)];
                        if (aSquare.Filled || aSquare.Occupied)
                            return false;
                    }
                }
            }

            //If it passes both those things then return true
            return true;
        }

        public void PlaceAPiece(GamePieces pieces, GamePieceDescriptor piece, int row, int col)
        {
            if(Globals.verbosity >= 2)
            {
                Console.WriteLine($"Gameboard.PlaceAPiece() says: Placing a {piece.PieceType} at ({row}, {col})");
            }

            PlacedPieces += 1;

            //Remove any other rotations of the current piece (identified by the piece's ID)
            for(int i = pieces.Pieces.Count - 1; i >= 0; i--)
            {
                if (pieces.Pieces[i].ID == piece.ID)
                {
                    if (Globals.verbosity >= 2)
                    {
                        Console.WriteLine($"Found a {pieces.Pieces[i].FullName} with ID {pieces.Pieces[i].ID}, which matches the 'piece' argument's ID.  Removing!");
                    }
                    pieces.Pieces.RemoveAt(i);
                }
            }

            //Mark squares as occupied; starting at row, col, extending by (rows, cols), rotated by 'rotation')

            //Written by ChatGPT
            if (piece.DoesThePieceFillTheArray)
            {
                // Rectangular piece: mark the full bounding box
                for (int i = row; i < row + piece.ArrayDimensions.Item1; i++)
                {
                    for (int j = col; j < col + piece.ArrayDimensions.Item2; j++)
                    {
                        MarkAsOccupied(piece.PieceType, i, j);
                    }
                }
            }
            else
            {
                if (piece.PieceShape == null) throw new Exception("piece.PieceShape is null, but we're in the !piece.DoesThePieceFillTheArray portion of the code.");

                // Non-rectangular piece: use PieceShape to mark only filled squares
                for (int i = 0; i < piece.PieceShape.GetLength(0); i++) // rows in the shape
                {
                    for (int j = 0; j < piece.PieceShape.GetLength(1); j++) // cols in the shape
                    {
                        if (!piece.PieceShape[i, j]) continue; // skip empty spots

                        int boardRow = row + i;
                        int boardCol = col + j;

                        MarkAsOccupied(piece.PieceType, boardRow, boardCol);
                    }
                }
            }
        }

        public void MarkAsOccupied(GamePieceType pieceType, int row, int col)
        {
            if(Globals.verbosity >= 2)
            {
                Console.WriteLine($"Gameboard.MarkAsOccupied() says: Marking ({row}, {col}) as occupied by a {pieceType}");
            }

            theBoard[(row - 1) * Cols + (col - 1)].PieceOccupiedBy = pieceType;
            theBoard[(row - 1) * Cols + (col - 1)].Occupied = true;
            UnoccupiedUnfilledSquares -= 1;
        }        

    } //class Gameboard

    public class PieceTracking
    {
        public Dictionary<(int, int, GamePieceType, PieceRotation), uint> PieceTracker { get; } // set; }
        public int Rows = 0;
        public int Cols = 0;

        public PieceTracking()
        {
            throw new Exception("PieceTracking() says: The 0-arg constructor isn't implemented.");
        }

        public PieceTracking(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;

            PieceTracker = [];
            Reset();
        }

        public void Reset()
        {
            //PieceTracker = [];    //Which to use?  Does it make a difference?  I can't really tell...so w/e.
            //PieceTracker.Clear();

            for (int i = 1; i <= Rows; i++)
            {
                for (int j = 1; j <= Cols; j++)
                {
                    /*foreach (GamePieceType pieceType in Enum.GetValues(typeof(GamePieceType)))
                    {
                        PieceTracker[(i, j, pieceType, PieceRotation.ZeroDegrees)] = 0;
                        PieceTracker[(i, j, pieceType, PieceRotation.NinetyDegrees)] = 0;
                        PieceTracker[(i, j, pieceType, PieceRotation.OneHundredEightyDegress)] = 0;
                        PieceTracker[(i, j, pieceType, PieceRotation.TwoHundredSeventyDegrees)] = 0;
                    }*/
                    for (int k = 0; k < 25; k++)
                    {
                        //PieceTracker[(i, j, (GamePieceType)k)] = (0, PieceRotation.Unspecified);
                        PieceTracker[(i, j, (GamePieceType)k, PieceRotation.ZeroDegrees)] = 0;
                        PieceTracker[(i, j, (GamePieceType)k, PieceRotation.NinetyDegrees)] = 0;
                        PieceTracker[(i, j, (GamePieceType)k, PieceRotation.OneHundredEightyDegress)] = 0;
                        PieceTracker[(i, j, (GamePieceType)k, PieceRotation.TwoHundredSeventyDegrees)] = 0;
                    }
                }
            }
        }
    }

}//END (namespace)
