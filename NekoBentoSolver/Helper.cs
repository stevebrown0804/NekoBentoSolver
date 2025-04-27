using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NekoBentoSolver
{
    static public class Helper
    {
        static public GamePieceDescriptor RotatePiece(PieceRotation rotation, GamePieceDescriptor piece)
        {
            if (rotation == PieceRotation.Unspecified) throw new Exception("Helper.RotatePiece() says: Hey, uh...PieceRotation.Unspecified is really just for initializing stuff.  You really don't want to call RotatePiece with it as the argument.  Because if you do, I'll throw.  (Like I am atm!)");

            //Moving on...
            GamePieceDescriptor rotatedPiece;
            
            if (Globals.verbosity >= 1)
            {
                Console.WriteLine($"Rotating a {piece.FullName} by {rotation.ToString()}");
            }

            if (piece.DoesThePieceFillTheArray)
            {
                switch (rotation)
                {
                    case PieceRotation.ZeroDegrees: return piece;
                    case PieceRotation.NinetyDegrees:
                        rotatedPiece = new GamePieceDescriptor(piece.ID, piece.PieceType, piece.FullName, piece.ThreeCharacterName, piece.NumberOfSquaresCoveredByThePiece, (piece.ArrayDimensions.Item2, piece.ArrayDimensions.Item1), piece.DoesThePieceFillTheArray);  //Note that we swapped the rectangle's dimensions to rotate it by 90 degrees
                        break;
                    default: throw new Exception("Helper.RotatePiece() says: That rotation isn't necessary for rectangular pieces  (It'll be the same size/shape as a 0 or 90 degree rotation.");
                }
            }
            else
            {
                if (piece.PieceShape == null) throw new Exception("Helper.RotatePiece() says: piece.DoesThePieceFillTheArray is false but piece.PieceShape is null...and that shouldn't be.");

                bool[,] rotatedPieceShape;
                switch (rotation)
                {
                    case PieceRotation.ZeroDegrees: return piece;
                    case PieceRotation.NinetyDegrees:
                        rotatedPieceShape = Helper.RotateA2DArrayBy90Degrees(piece.PieceShape);
                        rotatedPiece = new GamePieceDescriptor(piece.ID, piece.PieceType, piece.FullName, piece.ThreeCharacterName, piece.NumberOfSquaresCoveredByThePiece, (piece.ArrayDimensions.Item2, piece.ArrayDimensions.Item1), piece.DoesThePieceFillTheArray, rotatedPieceShape);
                        break;
                    case PieceRotation.OneHundredEightyDegress:
                        rotatedPieceShape = Helper.RotateA2DArrayBy90Degrees(Helper.RotateA2DArrayBy90Degrees(piece.PieceShape));
                        rotatedPiece = new GamePieceDescriptor(piece.ID, piece.PieceType, piece.FullName, piece.ThreeCharacterName, piece.NumberOfSquaresCoveredByThePiece, (piece.ArrayDimensions.Item1, piece.ArrayDimensions.Item2), piece.DoesThePieceFillTheArray, rotatedPieceShape);
                        break;
                    case PieceRotation.TwoHundredSeventyDegrees:
                        rotatedPieceShape = Helper.RotateA2DArrayBy90Degrees(Helper.RotateA2DArrayBy90Degrees(Helper.RotateA2DArrayBy90Degrees(piece.PieceShape)));
                        rotatedPiece = new GamePieceDescriptor(piece.ID, piece.PieceType, piece.FullName, piece.ThreeCharacterName, piece.NumberOfSquaresCoveredByThePiece, (piece.ArrayDimensions.Item2, piece.ArrayDimensions.Item1), piece.DoesThePieceFillTheArray, rotatedPieceShape);
                        break;
                    default: throw new Exception("Helper.RotatePiece() says: How did this throw happen?  Didn't we already check for Unspecified?  hmm...");
                }
            }

            rotatedPiece.Rotation = rotation;
            return rotatedPiece;
        }

        //Provided by ChatGPT
        static public bool[,] RotateA2DArrayBy90Degrees(bool[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            bool[,] result = new bool[cols, rows];

            for (int y = 0; y < cols; y++)
                for (int x = 0; x < rows; x++)
                    result[y, x] = matrix[rows - x - 1, y];

            return result;
        }

        //Provided by ChatGPT
        //Recall that there are non-recursive options, if this one has performance issues.
        //But as we're just using one permuation at a time in a foreach loop....I don't ~think~ it will.  But we'll see.
        //[Follow-up] Yeah, this method's implementation isn't the bottleneck, I don't think.  We could call ToList() on it and materialize the list and it's pretty quick....or, at least, it is with bagOfBags that aren't ENORMOUS.  (Too big to fit in this laptop's 40GB of RAM.)
        public static IEnumerable<List<T>> GetPermutations<T>(List<T> list)
        {
            if (list.Count == 1)
            {
                yield return new List<T>(list);
            }
            else
            {
                for (int i = 0; i < list.Count; i++)
                {
                    var current = list[i];
                    var remaining = new List<T>(list);
                    remaining.RemoveAt(i);

                    foreach (var permutation in GetPermutations(remaining))
                    {
                        permutation.Insert(0, current);
                        yield return permutation;
                    }
                }
            }

        }//END GetPermutations()

        //Courtest of ChatGPT
        public static string ShapeToString(bool[,] shape)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < shape.GetLength(0); i++)
            {
                for (int j = 0; j < shape.GetLength(1); j++)
                    sb.Append(shape[i, j] ? "1" : "0");
                sb.AppendLine(); // Optional line breaks
            }
            return sb.ToString();
        }

        public static int DetermineMultiplicativeFactorForPermutations(GamePieces pieces)
        {
            int factor = 1;
            foreach (var aPiece in pieces.Pieces)
            {
                //var type = aPiece.PieceType;

                switch (aPiece.PieceType)
                {
                    //Square
                    case GamePieceType.Pea:
                    case GamePieceType.Tomato:
                    case GamePieceType.Egg: factor *= 1; break;
                    //Rectangular; requires original + 1 rotation
                    case GamePieceType.RolledOmelette:
                    case GamePieceType.Carrot:
                    case GamePieceType.Sushi:
                    case GamePieceType.Dango:
                    case GamePieceType.Fillet: factor *= 2; break;
                    //Irregular, requires original + 3 rotations
                    case GamePieceType.LShapedSquid:
                    case GamePieceType.LShapedBlueFish:
                    case GamePieceType.OctopusWiener:
                    case GamePieceType.OctopusTentacle:
                    case GamePieceType.BentWiener:
                    case GamePieceType.ThreeProngThing:
                    case GamePieceType.Shrimp:
                    case GamePieceType.ChickenLeg:
                    case GamePieceType.LeafyMushroom:
                    case GamePieceType.FriedPrawn:
                    case GamePieceType.Marshmallows:
                    case GamePieceType.PeaPods:
                    case GamePieceType.Broccoli:
                    case GamePieceType.Mushroom:
                    case GamePieceType.Leaf:
                    case GamePieceType.UShapedLeaves:
                    case GamePieceType.SquareCucumbers: factor *= 4; break;
                    default: throw new Exception("Unimplemented piece type, I guess?  Or possbily just an unrecognized one.  *Shrug*");
                }                
            }
            return factor;
        }

        public static UInt64 Factorial(int n)
        {
            UInt64 res = 1;
            for (int i = n; i > 0; i--)
            {
                res *= (UInt64)i;
            }
            return res;
        }

    }//END class Helper

}//END (namespace)
