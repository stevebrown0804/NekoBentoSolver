using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NekoBentoSolver
{
    public enum GamePieceType
    {
        Pea,
        RolledOmelette,
        Tomato,
        Carrot,
        Sushi, 
        Egg,
        Dango,
        Fillet,
        LShapedSquid,
        LShapedBlueFish,
        OctopusWiener,
        OctopusTentacle,
        BentWiener,
        ThreeProngThing, //Mushroom, maybe?  really not sure
        Shrimp, 
        ChickenLeg,
        LeafyMushroom,
        FriedPrawn,
        Marshmallows,
        PeaPods,
        Broccoli,
        Mushroom,
        Leaf,
        UShapedLeaves,
        SquareCucumbers,

        //TODO, EVENTUALLY: Add more enum types (to GamePieceType)
    }

    public enum PieceRotation
    {
        Unspecified,
        ZeroDegrees,
        NinetyDegrees,
        OneHundredEightyDegress,
        TwoHundredSeventyDegrees
    }

    public static class Lookups
    {
        public static string LookUpThreeLetterPieceCode(GamePieceType pieceType)
        {
            switch (pieceType)
            {
                case GamePieceType.Pea: return "Pea";
                case GamePieceType.RolledOmelette: return "ROm";
                case GamePieceType.Tomato: return "Tmt";
                case GamePieceType.Carrot: return "Crt";
                case GamePieceType.Sushi: return "Ssh";
                case GamePieceType.Egg: return "Egg";
                case GamePieceType.Dango: return "Dng";
                case GamePieceType.Fillet: return "Flt";
                case GamePieceType.LShapedSquid: return "LSS";
                case GamePieceType.LShapedBlueFish: return "LBF";
                case GamePieceType.OctopusWiener: return "OcW";
                case GamePieceType.OctopusTentacle: return "OcT";
                case GamePieceType.BentWiener: return "BnW";
                case GamePieceType.ThreeProngThing: return "TPT";
                case GamePieceType.Shrimp: return "Shr";
                case GamePieceType.ChickenLeg: return "ChL";
                case GamePieceType.LeafyMushroom: return "LfM";
                case GamePieceType.FriedPrawn: return "FPr";
                case GamePieceType.Marshmallows: return "Mlw";
                case GamePieceType.PeaPods: return "Pod";
                case GamePieceType.Broccoli: return "Brc";
                case GamePieceType.Mushroom: return "Msh";
                case GamePieceType.Leaf: return "Lff";
                case GamePieceType.UShapedLeaves: return "USL";
                case GamePieceType.SquareCucumbers: return "SqC";

                default: throw new Exception("Lookups.LookUpThreeLetterPieceCode() says: As-of-yet unimplemented piece type.");
                //TODO, EVENTUALLY: Add more three-letter lookups here (GamePieces.LookUpThreeLetterPieceCode())
            }
        }
    }


    public class GamePieces
    {
        //stuff behind properties
        private uint _nextID;
        
        //Properties
        public int CoverageCount { get; set; }      //Fancy way to say "the number of squares that the pieces can cover, assuming no overlap"
        public List<GamePieceDescriptor> Pieces { get; }
        public uint NextID {get => _nextID++;}
        public int RemainingPieces { get => Pieces.Count;}


        //Constructors
        public GamePieces()
        {
            Pieces = new List<GamePieceDescriptor>();
            CoverageCount = 0;
            _nextID = 1; //start with 1 and have 0 represent 'unassigned?' *shrug* sounds ok, for the moment.  we'll see.
        }
        public GamePieces(List<GamePieceDescriptor> pieces)
        {
            if (Globals.verbosity >= 2)
            {
                Console.WriteLine($"Wrapping a List<GamePiecesDescriptor> in a GamePieces object.");
            }

            if (pieces.Count == 0) throw new Exception("GamePieces 1-arg constructor says: Pieces.Count was zero.  And now I'm wondering why we're using this constructor.");

            Pieces = pieces;
            CoverageCount = 0;
            foreach (GamePieceDescriptor aPiece in Pieces)
            {
                CoverageCount += aPiece.NumberOfSquaresCoveredByThePiece;   
                //TODO: Pretty sure rotations are being multi-counted; let's stash the pre-rotations CoverageCount, pass it in and set it that way
            }

            if (Pieces.Count < 0) throw new Exception("GamePieces 1-arg constructor says: pieces.Count was negative? O_o");

            //Let's check for the highest ID that was used in pieces
            uint highID = pieces[0].ID;  //we're guaranteed that at least one element exists thanks to the check near the top of this fn
            for(int i = 1; i < pieces.Count; i++)
            {
                if (pieces[i].ID > highID){
                    if(Globals.verbosity >= 2)
                    {
                        Console.WriteLine($"GamePieces 1-arg constructor says: highID was {highID} but we found one higher: {pieces[i].ID}.  Replacing.");
                    }
                    highID = pieces[i].ID;
                }
            }
            _nextID = highID + 1;
        }


        //Methods
        public GamePieceDescriptor CreateGamePiece(GamePieceType gamePieceType)
        {
            switch (gamePieceType)
            {
                //Those that call the n-arg constructor...
                case GamePieceType.Pea: 
                    return new GamePieceDescriptor(NextID, GamePieceType.Pea, "Pea", Lookups.LookUpThreeLetterPieceCode(GamePieceType.Pea), 1, (1,1), true);
                case GamePieceType.RolledOmelette: 
                    return new GamePieceDescriptor(NextID, GamePieceType.RolledOmelette, "Rolled Omelette", Lookups.LookUpThreeLetterPieceCode(GamePieceType.RolledOmelette), 2, (2,1), true);
                case GamePieceType.Tomato: 
                    return new GamePieceDescriptor(NextID, GamePieceType.Tomato, "Tomato", Lookups.LookUpThreeLetterPieceCode(GamePieceType.Tomato), 4, (2,2), true);
                case GamePieceType.Carrot: 
                    return new GamePieceDescriptor(NextID, GamePieceType.Carrot, "Carrot", Lookups.LookUpThreeLetterPieceCode(GamePieceType.Carrot), 3, (1, 3), true);
                case GamePieceType.Sushi: 
                    return new GamePieceDescriptor(NextID, GamePieceType.Sushi, "Sushi", Lookups.LookUpThreeLetterPieceCode(GamePieceType.Sushi), 6, (2, 3), true);
                case GamePieceType.Egg:
                    return new GamePieceDescriptor(NextID, GamePieceType.Egg, "Egg", Lookups.LookUpThreeLetterPieceCode(GamePieceType.Egg), 9, (3, 3), true);
                case GamePieceType.Dango: 
                    return new GamePieceDescriptor(NextID, GamePieceType.Dango, "Dango", Lookups.LookUpThreeLetterPieceCode(GamePieceType.Dango), 5, (5, 1), true);
                case GamePieceType.Fillet:
                    return new GamePieceDescriptor(NextID, GamePieceType.Fillet, "Fillet", Lookups.LookUpThreeLetterPieceCode(GamePieceType.Fillet), 12, (2, 6), true);

                //...and those that call the (n+1)-arg constructor
                case GamePieceType.LShapedSquid:
                    //TODO, MAYBE: Move this code into a helper method to draw a piece and add it to logging

                    //TMP, maybe? *Shrug*
                    /*Console.WriteLine("L-Shared Squid piece:");
                    bool[,] LSSArray = { { true, true, true, true }, { false, false, false, true } };
                    for(int i = LSSArray.GetLength(0) - 1; i>= 0; i--)
                    {
                        for(int j = 0; j < LSSArray.GetLength(1); j++)          //Is this right?  TBD...eventually.
                        {
                            Console.Write(LSSArray[i, j] ? "x " : "_ " );
                        }
                        Console.WriteLine();
                    }*/
                    return new GamePieceDescriptor(NextID, GamePieceType.LShapedSquid, "L-Shaped Squid", Lookups.LookUpThreeLetterPieceCode(GamePieceType.LShapedSquid), 5, (2, 4), false, new bool[,] { { true, true, true, true }, { false, false, false, true } });
                //END TMP
                case GamePieceType.LShapedBlueFish:
                    return new GamePieceDescriptor(NextID, GamePieceType.LShapedBlueFish, "L-Shaped Blue Fish", Lookups.LookUpThreeLetterPieceCode(GamePieceType.LShapedBlueFish), 5, (2, 4), false, new bool[,] { { true, false, false, false }, { true, true, true, true } });
                case GamePieceType.OctopusWiener:
                    return new GamePieceDescriptor(NextID, GamePieceType.OctopusWiener, "Octopus Wiener", Lookups.LookUpThreeLetterPieceCode(GamePieceType.OctopusWiener), 7, (3, 3), false, new bool[,] {{true, true, true}, {false, true, true}, {false, true, true}});
                case GamePieceType.OctopusTentacle:
                    return new GamePieceDescriptor(NextID, GamePieceType.OctopusTentacle, "Octopus Tentacle", Lookups.LookUpThreeLetterPieceCode(GamePieceType.OctopusTentacle), 8, (2, 5), false, new bool[,] { { true, true, true, true, true }, { true, false, false, true, true }});
                case GamePieceType.BentWiener:
                    return new GamePieceDescriptor(NextID, GamePieceType.BentWiener, "Bent Wiener", Lookups.LookUpThreeLetterPieceCode(GamePieceType.BentWiener), 7, (2, 5), false, new bool[,] { { true, true, true, true, true }, { true, false, false, false, true } });
                case GamePieceType.ThreeProngThing:
                    return new GamePieceDescriptor(NextID, GamePieceType.ThreeProngThing, "Three-prong thing", Lookups.LookUpThreeLetterPieceCode(GamePieceType.ThreeProngThing), 6, (4, 3), false, new bool[,] { { false, true, false}, { true, true, false}, { false, true, true }, { false, true, false } });
                case GamePieceType.Shrimp:
                    return new GamePieceDescriptor(NextID, GamePieceType.Shrimp, "Shrimp", Lookups.LookUpThreeLetterPieceCode(GamePieceType.Shrimp), 3, (2, 2), false, new bool[,] { { true, false }, { true, true} });
                case GamePieceType.ChickenLeg:
                    return new GamePieceDescriptor(NextID, GamePieceType.ChickenLeg, "Chicken Leg", Lookups.LookUpThreeLetterPieceCode(GamePieceType.ChickenLeg), 7, (2, 4), false, new bool[,] { { true, true, true, false }, { true, true, true, true } });
                case GamePieceType.LeafyMushroom:
                    return new GamePieceDescriptor(NextID, GamePieceType.LeafyMushroom, "Leafy Mushroom", Lookups.LookUpThreeLetterPieceCode(GamePieceType.LeafyMushroom), 6, (3, 3), false, new bool[,] { { true, true, true}, {false, true, true }, { false, true, false } });
                case GamePieceType.FriedPrawn:
                    return new GamePieceDescriptor(NextID, GamePieceType.FriedPrawn, "Fried Prawn", Lookups.LookUpThreeLetterPieceCode(GamePieceType.FriedPrawn), 5, (3, 3), false, new bool[,] {{ true, false, false }, { true, false, false }, {true, true, true }});
                case GamePieceType.Marshmallows:
                    return new GamePieceDescriptor(NextID, GamePieceType.Marshmallows, "Marshmallows", Lookups.LookUpThreeLetterPieceCode(GamePieceType.Marshmallows), 5, (2, 3), false, new bool[,] { { true, true, true }, { true, false, true }});
                case GamePieceType.PeaPods:
                    return new GamePieceDescriptor(NextID, GamePieceType.PeaPods, "Pea Pods", Lookups.LookUpThreeLetterPieceCode(GamePieceType.PeaPods), 6, (3, 3), false, new bool[,] { { true, true, true }, { true, false, true }, { false, false, true } });
                case GamePieceType.Broccoli:
                    return new GamePieceDescriptor(NextID, GamePieceType.Broccoli, "Broccoli", Lookups.LookUpThreeLetterPieceCode(GamePieceType.Broccoli), 7, (3, 3), false, new bool[,] { { false, true, false }, { true, true, true }, { true, true, true } });
                case GamePieceType.Mushroom:
                    return new GamePieceDescriptor(NextID, GamePieceType.Mushroom, "Mushroom", Lookups.LookUpThreeLetterPieceCode(GamePieceType.Mushroom), 4, (2, 3), false, new bool[,] { { true, true, true }, { false, true, false }});
                case GamePieceType.Leaf:
                    return new GamePieceDescriptor(NextID, GamePieceType.Leaf, "Leaf", Lookups.LookUpThreeLetterPieceCode(GamePieceType.Leaf), 7, (3, 3), false, new bool[,] { { true, false, false }, { true, true, true }, { true, true, true } });
                case GamePieceType.UShapedLeaves:
                    return new GamePieceDescriptor(NextID, GamePieceType.UShapedLeaves, "U-Shaped Leaves", Lookups.LookUpThreeLetterPieceCode(GamePieceType.UShapedLeaves), 7, (3, 3), false, new bool[,] { { true, true, true }, { true, false, true }, { true, false, true } });
                case GamePieceType.SquareCucumbers:
                    return new GamePieceDescriptor(NextID, GamePieceType.SquareCucumbers, "Square Cucumbers", Lookups.LookUpThreeLetterPieceCode(GamePieceType.SquareCucumbers), 3, (2, 2), false, new bool[,] { { true, true }, { false, true } });


                //TODO, EVENTUALLY: Add more cases as we add more enum types
                default: throw new Exception("GamePieceDescriptor.GimmeAGamePiece() says: unrecognized enum type, yo.");
            }
        }

        public void AddToBag(GamePieceDescriptor piece)
        {
            Pieces.Add(piece);
            CoverageCount += piece.NumberOfSquaresCoveredByThePiece;            
        }

        public GamePieceDescriptor RemoveFromBag(GamePieceDescriptor piece)
        {
            Pieces.Remove(piece);   //removes the first instance of piece...which should be fine, seeing has how multiple pieces (of the same type) will be identical

            if(Globals.verbosity >= 1)
            {
                Console.WriteLine($"Pulled a {piece.FullName} from the bag.");
            }

            return piece;
        }

        public void EmptyTheBag()
        {
            Pieces.Clear();
            CoverageCount = 0;
        }

        public void SortTheBag()
        {
            Pieces.Sort((p1, p2) => p1.NumberOfSquaresCoveredByThePiece.CompareTo(p2.NumberOfSquaresCoveredByThePiece));
            //DONE, BUT MAYBE WE'LL TRY AGAIN LATER: Try out this SortTheBag() and see if/how it works
            //FOLLOW-UP: It was slow!  Too slow to use, atm. (Solver.Solve() took ~200ms with it enabled, ~10ms with it commented out)
        }

        public List<GamePieceDescriptor> RotatePiecesAndAddToTheBag()
        {
            List<GamePieceDescriptor> newPieces = [];

            foreach (GamePieceDescriptor aPiece in Pieces)
            {
                //rotate it all 4 (or 2, atm) ways

                //If it's square then we don't need to rotate it at all (as all rotations are symmetric)
                if (aPiece.DoesThePieceFillTheArray)
                {
                    if (aPiece.ArrayDimensions.Item1 == aPiece.ArrayDimensions.Item2)
                    {
                        if (Globals.verbosity >= 2) Console.WriteLine($"Piece ({aPiece.FullName}, {aPiece.ID}) is square; skipping rotations.");
                    }
                    else
                    {
                        //If it's rectangular (but non-square) and it "fills the rectangle," we only need to do a 90 degree rotation
                        if (Globals.verbosity >= 2) Console.WriteLine($"Piece ({aPiece.FullName}, {aPiece.ID}) is rectangular but fills the rectangle; adding a 90 degree rotation.");
                        newPieces.Add(Helper.RotatePiece(PieceRotation.NinetyDegrees, aPiece));
                    }
                }
                else //Non-square, doesn't fill the rectangle
                {
                    //Otherwise, we'll do all rotations

                    //90 degree
                    newPieces.Add(Helper.RotatePiece(PieceRotation.NinetyDegrees, aPiece));

                    //180 degree
                    newPieces.Add(Helper.RotatePiece(PieceRotation.OneHundredEightyDegress, aPiece));

                    //270 degree
                    newPieces.Add(Helper.RotatePiece(PieceRotation.TwoHundredSeventyDegrees, aPiece));
                }
            }

            //Once we're done rotating pieces, add them to Pieces
            foreach (GamePieceDescriptor aPieceAgain in newPieces)
            {
                Pieces.Add(aPieceAgain);
            }

            return Pieces;
        }

        //TODO...AGAIN, 'MAYBE': Write a MakeACopy() for GamePieces
        //I was thinking that we might need this if we want to reset the bag between unsuccessful rotations, but then I thought, "We'll want a way to ID each piece
        // so that if we find a successful rotation, we can stop trying that piece (by removing pieces with the same ID from the bag).
        //If we implement that, I'm pretty sure we won't need MakeACopy().
        // Hmm.

    } //class GamePieces

    public class GamePieceDescriptor
    {
        //Properties
        public GamePieceType PieceType { get; }
        public uint ID { get; set; }
        public String FullName { get; }
        public String ThreeCharacterName { get; }
        public int NumberOfSquaresCoveredByThePiece { get; }
        public (int, int) ArrayDimensions { get; }
        public bool DoesThePieceFillTheArray { get; }
        public bool[,]? PieceShape { get; }
        public PieceRotation Rotation { get; set; }

        //Constructors
        public GamePieceDescriptor()
        {
            throw new Exception("GamePieceDescrictor says: The zero-arg constructor isn't defined, mang.");
        }
        //Constructor one: The one for rectangular pieces (ie. they "fill the array")
        public GamePieceDescriptor(uint ID, GamePieceType pieceType, string fullName, string threeCharacterName, int numberOfSquares, (int, int) arrayDimensions, bool doesThePieceFillTheArray)
        {
            if (!doesThePieceFillTheArray) throw new Exception("GamePieceDescriptor's n-arg constructor says: If the piece doesn't fill the array then you need to use the (n+1)-arg constructor and specify the piece's shape, yo.");

            this.ID = ID;
            PieceType = pieceType;
            FullName = fullName;
            ThreeCharacterName = threeCharacterName;
            NumberOfSquaresCoveredByThePiece = numberOfSquares;
            ArrayDimensions = arrayDimensions;
            DoesThePieceFillTheArray = doesThePieceFillTheArray;
            Rotation = PieceRotation.ZeroDegrees;   //Pretty sure any game piece created via constructor has zero rotation....right? TBD!

            //Console.WriteLine($"GamePieceDescrictor's n-arg constructor says: Have a: {FullName}");
        }

        //Constructor two: The one for non-rectangular pieces (ie. they DON'T fill the array)
        //DONE? THAT WAS QUICK: Write the (n+1)-arg constructor for GamePieceDescriptor

        public GamePieceDescriptor(uint ID, GamePieceType pieceType, string fullName, string threeCharacterName, int numberOfSquares, (int, int) arrayDimensions, bool doesThePieceFillTheArray, bool[,] pieceShape)
        {
            if (doesThePieceFillTheArray) throw new Exception("GamePieceDescriptor's (n+1)-arg constructor says: If the piece fills the array then use the n-arg constructor (and don't bother specifying the piece's shape -- it's rectangular, and that's all we need to know for those pieces).");

            this.ID = ID;
            PieceType = pieceType;
            FullName = fullName;
            ThreeCharacterName = threeCharacterName;
            NumberOfSquaresCoveredByThePiece = numberOfSquares;
            ArrayDimensions = arrayDimensions;
            DoesThePieceFillTheArray = doesThePieceFillTheArray;
            PieceShape = pieceShape;
            Rotation = PieceRotation.ZeroDegrees;   //Again, I'm pretty sure any game piece created via constructor has zero rotation...but TBD.

            //Console.WriteLine($"GamePieceDescrictor's (n+1)-arg constructor says: Have a: {FullName}");
        }

    }
   
}//END (namespace)
