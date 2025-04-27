using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NekoBentoSolver
{
    public enum ExpansionPack
    {
        Main,
        FirstLevelPack,
        SecondLevelPack,
        ThirdLevelPack
    }

    public static class LevelCreator
    {
        public static (Gameboard, GamePieces) CreateLevel(ExpansionPack xpack, int level)
        {
            //Levels per xpack:
            //  Main: 96
            //  xpack 1: 24
            //  xpack 2: 24
            //  xpack 3: 24

            //Let's get some error-checking out of the way
            if (level <= 0) throw new Exception($"Seriously, level {level}? That's just silly.");

            //Moving on...
            if(Globals.verbosity >= 1)
            {
                Console.WriteLine($"Constructing: {xpack}, level {level}");
            }

            Gameboard gameboard;
            GamePieces pieces;  //pretty sure we COULD construct this now, were we so inclined.

            switch (xpack)
            {
                case ExpansionPack.Main:
                    switch (level)
                    {
                        case 1:
                            //  _ _ _
                            //Filled squares: none
                            //Pieces: Carrot
                            gameboard = new Gameboard(1, 3);    
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            break;
                        case 2:
                            //  _
                            //  _
                            //  _
                            //Filled squares: none
                            //Piece: Carrot
                            gameboard = new Gameboard(3, 1);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            break;
                        case 3:
                            //  x _ x
                            //  x _ x
                            //  x _ x
                            //  _ _ _
                            //Dimensions: 4 rows, 3 columns
                            //Filled squares: r2c1, r2c3, r3c1, r3c3, r4c1, r4c3
                            //Pieces: Carrot x2
                            gameboard = new Gameboard(4, 3);
                            gameboard.FillSquare(2, 1);
                            gameboard.FillSquare(2, 3);
                            gameboard.FillSquare(3, 1);
                            gameboard.FillSquare(3, 3);
                            gameboard.FillSquare(4, 1);
                            gameboard.FillSquare(4, 3);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            break;
                        case 4:
                            //  _ _ _
                            //  _ _ _
                            //  _ _ _
                            //Filled squares: none
                            //Pieces: Carrot, sushi
                            gameboard = new Gameboard(3, 3);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Sushi));
                            break;
                        case 5:
                            //  _ _ _ _ _ _
                            //  _ _ _ _ _ _
                            //  _ _ _ _ _ _
                            //  _ _ _ _ _ _
                            //Filled squares: none
                            //Pieces: Carrot x4, Sushi x2
                            gameboard = new Gameboard(4, 6);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Sushi));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Sushi));
                            break;
                        case 6:
                            //  _ _ _ x
                            //  _ _ _ _
                            //  _ _ _ _
                            //  _ _ _ _
                            //Filled squares: r4c4
                            //Pieces: Carrots x3, Sushi
                            gameboard = new Gameboard(4, 4);
                            gameboard.FillSquare(4, 4);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Sushi));
                            break;
                        case 7:
                            //  _ _ _ _ _
                            //  _ _ _ _ _
                            //  _ _ _ _ _
                            //  x x _ _ _
                            //Filled squares: r1c1, r1c2
                            //Pieces: Carrots x2, Sushi x2
                            gameboard = new Gameboard(4, 5);
                            gameboard.FillSquare(1, 1);
                            gameboard.FillSquare(1, 2);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Sushi));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Sushi));
                            break;
                        case 8:
                            //  _ _ _ _ _
                            //  _ _ _ _ _
                            //  _ _ _ _ _
                            //  _ _ _ _ _
                            //Filled squares: none
                            //Pieces: Carrots x2, Sushi, Tomato x2
                            gameboard = new Gameboard(4, 5);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Sushi));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            break;
                        case 9:
                            // _ _ _ _ _ x
                            // _ _ _ _ _ _
                            // _ _ _ _ _ _
                            // _ _ _ _ _ _
                            // x x x _ _ _
                            //Filled squares: r1c1, r1c2, r1c3, r5c6
                            //Pieces: Tomato x2, Sushi, Carrot x4
                            gameboard = new Gameboard(5, 6);
                            gameboard.FillSquare(1, 1);
                            gameboard.FillSquare(1, 2);
                            gameboard.FillSquare(1, 3);
                            gameboard.FillSquare(5, 6);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Sushi));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            break;
                        case 10:
                            // _ _ _ _ _
                            // _ _ _ _ _
                            // _ _ x _ _
                            // _ _ _ _ _
                            //Filled squares: r2c3
                            //Pieces: Carrot x3, Tomato, Sushi
                            gameboard = new Gameboard(4, 5);
                            gameboard.FillSquare(2 ,3);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Sushi));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            break;
                        case 11:
                            // _ _ _ _ _ _
                            // _ _ _ _ _ _ 
                            // x _ _ _ _ _
                            // x _ _ x _ _
                            //Filled squares: r1c1, r1c4, r2c1
                            //Pieces: Carrot , Tomato x3, Sushi
                            gameboard = new Gameboard(4, 6);
                            gameboard.FillSquare(1, 1);
                            gameboard.FillSquare(2, 1);
                            gameboard.FillSquare(1, 4);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Sushi));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            break;
                        case 12:
                            // _ _ _ _ 
                            // _ _ _ _ 
                            // x _ _ _ 
                            // x _ _ _ 
                            //Filled squares: r1c1, r2c2
                            //Pieces: Carrot x3, Tomato, Sushi
                            gameboard = new Gameboard(4, 4);
                            gameboard.FillSquare(1, 1);
                            gameboard.FillSquare(2, 1);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Sushi));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Pea));
                            break;
                        case 13:
                            // _ _ x _ 
                            // _ _ _ _ 
                            // _ _ _ _ 
                            // x _ _ x 
                            //Filled squares: r1c1, r1c4, r4c3
                            //Pieces: Carrot x3, Tomato, Sushi
                            gameboard = new Gameboard(4, 4);
                            gameboard.FillSquare(1, 1);
                            gameboard.FillSquare(1, 4);
                            gameboard.FillSquare(4 ,3);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Pea));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Pea));
                            break;
                        case 14:
                            //  _ _ _ _ _ _
                            //  _ _ _ _ _ _
                            //  _ _ _ _ _ _
                            //  _ _ _ _ _ _
                            //  _ _ _ _ _ _
                            //  _ _ _ _ _ _
                            //Filled squares: r1c3, r4c1, r4c6, r5c6, r6c6
                            //Pieces: Carrot, Sushi x3, Tomato x2, Pea x2
                            gameboard = new Gameboard(6, 6);
                            gameboard.FillSquare(1, 3);
                            gameboard.FillSquare(4, 1);
                            gameboard.FillSquare(4, 6);
                            gameboard.FillSquare(5, 6);
                            gameboard.FillSquare(6, 6);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Sushi));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Sushi));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Sushi));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Pea));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Pea));
                            break;
                        case 15:
                            // _ _ _ _ _
                            // _ _ _ _ _
                            // _ x _ _ _
                            // _ _ _ _ x
                            //Filled squares: r1c5, r2c2
                            //Pieces: Carrot x2, Tomato, Sushi, Pea x2
                            gameboard = new Gameboard(4, 5);
                            gameboard.FillSquare(1, 5);
                            gameboard.FillSquare(2 ,2);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Sushi));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Pea));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Pea));
                            break;
                        case 16:
                            // _ _ _ x x
                            // _ _ _ _ _
                            // _ _ _ _ _
                            // _ _ _ x _
                            // _ _ _ _ _
                            //Filled squares: r2c4, r5c4, r5c5
                            //Pieces: Carrot x2, Tomato x2, Sushi, Pea x2
                            gameboard = new Gameboard(5, 5);
                            gameboard.FillSquare(2, 4);
                            gameboard.FillSquare(5, 4);
                            gameboard.FillSquare(5, 5);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Sushi));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Pea));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Pea));
                            break;
                        case 17:
                            // _ _ x x 
                            // _ _ _ _ 
                            // _ _ _ _ 
                            // x _ _ _ 
                            //Filled squares: r1c1, r4c3, r4c4
                            //Pieces: Carrot, Tomato x2, Pea x2
                            gameboard = new Gameboard(4, 4);
                            gameboard.FillSquare(1, 1);
                            gameboard.FillSquare(4, 3);
                            gameboard.FillSquare(4, 4);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Pea));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Pea));
                            break;
                        case 18:
                            // x x _ _ _
                            // _ _ _ _ _
                            // _ _ _ _ _
                            // x _ _ _ _
                            // x x _ x x
                            //Filled squares: r1c1, r1c2, r1c4, r1c5, r2c1, r5c1, r5c2
                            //Pieces: Carrot, Tomato x2, Sushi, Pea
                            gameboard = new Gameboard(5, 5);
                            gameboard.FillSquare(1, 1);
                            gameboard.FillSquare(1, 2);
                            gameboard.FillSquare(1, 4);
                            gameboard.FillSquare(1, 5);
                            gameboard.FillSquare(2, 1);
                            gameboard.FillSquare(5, 1);
                            gameboard.FillSquare(5, 2);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Sushi));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Pea));                            
                            break;
                        //FUN FACT: Lvl 19 is the first level that uses a piece that doesn't fill the rectangle
                        case 19:
                            // _ x x _
                            // _ _ _ _
                            // _ _ _ _
                            // _ _ x _
                            //Filled squares: r1c3, r4c2, r4c3
                            //Pieces: Carrot, Tomato, L-Shaped Squid, Pea
                            gameboard = new Gameboard(4, 4);
                            gameboard.FillSquare(1, 3);
                            gameboard.FillSquare(4, 2);
                            gameboard.FillSquare(4, 3);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.LShapedSquid));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Pea));
                            break;
                        case 20:
                            // _ _ _ _ _
                            // _ x _ _ _
                            // _ _ _ _ _
                            // _ _ _ _ _
                            // _ _ _ x x
                            //Filled squares: r1c4, r1c5, r4c2
                            //Pieces: Carrot x2, Tomato, L-Shaped Squid, Pea, Sushi
                            gameboard = new Gameboard(5, 5);
                            gameboard.FillSquare(1, 4);
                            gameboard.FillSquare(1 ,5);
                            gameboard.FillSquare(4, 2);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.LShapedSquid));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Pea));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Sushi));
                            break;
                        case 21:
                            // _ x x _ _
                            // _ _ _ _ _
                            // _ _ _ _ _
                            // _ _ _ _ _
                            //Filled squares: r4c2, r4c3
                            //Pieces: Carrot, Tomato, L-Shaped Squid, Sushi
                            gameboard = new Gameboard(4, 5);
                            gameboard.FillSquare(4, 2);
                            gameboard.FillSquare(4, 3);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.LShapedSquid));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Sushi));
                            break;
                        case 22:
                            // x x _ _ _
                            // _ _ _ _ _
                            // _ _ _ x x
                            // _ _ _ _ x
                            // x x _ _ _
                            //Filled squares: r1c1, r1c2, r2c5, r3c4, r3c5, r5c1, r5c2
                            //Pieces: Carrot, Tomato, L-Shaped Squid, Sushi
                            gameboard = new Gameboard(5, 5);
                            gameboard.FillSquare(1, 1);
                            gameboard.FillSquare(1, 2);
                            gameboard.FillSquare(2, 5);
                            gameboard.FillSquare(3, 4);
                            gameboard.FillSquare(3, 5);
                            gameboard.FillSquare(5, 1);
                            gameboard.FillSquare(5, 2);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.LShapedSquid));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Sushi));
                            break;
                        case 23:
                            // x x _ _ x
                            // _ _ _ _ x
                            // _ _ _ _ _ 
                            // _ _ x _ _ 
                            // _ _ x _ _
                            //Filled squares: r1c3, r2c3, r4c5, r5c1, r5c2, r5c5
                            //Pieces: Carrot, Tomato, L-Shaped Squid, Sushi, Pea
                            gameboard = new Gameboard(5, 5);
                            gameboard.FillSquare(1, 3);
                            gameboard.FillSquare(2, 3);
                            gameboard.FillSquare(4, 5);
                            gameboard.FillSquare(5, 1);
                            gameboard.FillSquare(5, 2);
                            gameboard.FillSquare(5, 5);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.LShapedSquid));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Sushi));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Pea));
                            break;
                        case 24:
                            // _ _ _ _ x
                            // _ _ _ x x
                            // x _ _ _ _ 
                            // x _ _ _ _ 
                            // _ _ _ _ _
                            //Filled squares: r2c1, r3c1, r4c4, r4c5, r5c5
                            //Pieces: Carrot, L-Shaped Squid, Sushi, Pea
                            gameboard = new Gameboard(5, 5);
                            gameboard.FillSquare(2, 1);
                            gameboard.FillSquare(3, 1);
                            gameboard.FillSquare(4, 4);
                            gameboard.FillSquare(4, 5);
                            gameboard.FillSquare(5, 5);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Pea));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.LShapedBlueFish));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.LShapedSquid));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Sushi));
                            break;
                        case 25:
                            // x _ _ _ x
                            // x _ _ _ _ 
                            // _ _ _ _ _ 
                            // _ _ _ _ x 
                            // _ _ _ x x
                            //Filled squares: r1c4, r1c5, r2c5, r4c1, r5c1, r5c5
                            //Pieces: Tomato, Sushi, Carrot, L-Shaped Blue Fish, Pea
                            gameboard = new Gameboard(5, 5);
                            gameboard.FillSquare(1 ,4);
                            gameboard.FillSquare(1, 5);
                            gameboard.FillSquare(2, 5);
                            gameboard.FillSquare(4, 1);
                            gameboard.FillSquare(5, 1);
                            gameboard.FillSquare(5, 5);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Sushi));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.LShapedBlueFish));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Pea));
                            break;
                        case 26:
                            // _ _ _ _ _ _
                            // _ _ _ _ _ _ 
                            // _ _ _ _ x x 
                            // x _ _ _ _ _ 
                            // x _ _ _ _ _ 
                            // x _ x _ _ _
                            //Filled squares: r1c1, r1c3, r2c1, r3c1, r4c5, r4c6
                            //Pieces: Tomato x2, Sushi, Carrot x2, L-Shaped Blue Fish x2
                            gameboard = new Gameboard(6, 6);
                            gameboard.FillSquare(1, 1);
                            gameboard.FillSquare(1, 3);
                            gameboard.FillSquare(2, 1);
                            gameboard.FillSquare(3, 1);
                            gameboard.FillSquare(4, 5);
                            gameboard.FillSquare(4, 6);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.LShapedBlueFish));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Sushi));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.LShapedBlueFish));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            break;
                        case 27:
                            // _ _ _ _ _
                            // _ _ _ x _ 
                            // _ _ _ _ _ 
                            // _ _ _ _ _ 
                            // x _ _ _ x 
                            //Filled squares: r1c1, r1c5, r4c4
                            //Pieces: Carrot x2, L-Shaped Blue Fish, Pea x2, Egg
                            gameboard = new Gameboard(5, 5);
                            gameboard.FillSquare(1, 1);
                            gameboard.FillSquare(1, 5);
                            gameboard.FillSquare(4, 4);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.LShapedBlueFish));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Pea));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Pea));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Egg));
                            break;
                        case 28:
                            // x x _ _ x x
                            // _ _ _ _ _ _
                            // _ _ _ _ _ _                             
                            // _ _ _ _ x _ 
                            // _ _ _ x x _ 
                            //Filled squares: r1c4, r1c5, r2c5, r5c1, r5c2, r5c5, r5c6
                            //Pieces: Carrot, L-Shaped Blue Fish x2, Pea, Egg
                            gameboard = new Gameboard(5, 6);
                            gameboard.FillSquare(1, 4);
                            gameboard.FillSquare(1, 5);
                            gameboard.FillSquare(2, 5);
                            gameboard.FillSquare(5, 1);
                            gameboard.FillSquare(5, 2);
                            gameboard.FillSquare(5, 5);
                            gameboard.FillSquare(5, 6);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Egg));             //optimally sorted, I think 
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.LShapedBlueFish)); //TODO: ^add this comment wherever it's applicable
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.LShapedBlueFish));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Pea));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));                            
                            break;
                        case 29:
                            // _ _ _ _ _
                            // _ x x _ _
                            // _ _ _ _ x
                            // _ _ _ _ x
                            // _ _ _ _ _
                            // x _ _ _ _
                            // x x _ _ _
                            //Filled squares: r1c1, r1c2, r2c1, r4c5, r5c5, r6c2, r6c3
                            //Pieces: Tomato, Egg, Carrot, Sushi, L-Shaped Blue Fish, Pea
                            gameboard = new Gameboard(7, 5);
                            gameboard.FillSquare(1, 1);
                            gameboard.FillSquare(1, 2);
                            gameboard.FillSquare(2, 1);
                            gameboard.FillSquare(4, 5);
                            gameboard.FillSquare(5, 5);
                            gameboard.FillSquare(6, 2);
                            gameboard.FillSquare(6, 3);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Egg));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Sushi));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.LShapedBlueFish));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Pea));
                            break;
                        case 30:
                            // x _ _ _ _
                            // _ _ _ _ _
                            // _ _ _ _ _
                            // _ _ _ _ _
                            // x _ _ _ _
                            //Filled squares: r1c1, r5c1
                            //Pieces: Egg, Carrot, L-Shaped Blue Fish x2, Pea
                            gameboard = new Gameboard(5, 5);
                            gameboard.FillSquare(1, 1);
                            gameboard.FillSquare(5, 1);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Egg));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.LShapedBlueFish));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.LShapedBlueFish));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Pea));
                            break;
                        case 31:
                            throw new Exception("Level unimplemented atm.  Check level 32 for some new pieces though, mang.");
                        case 32:
                            // _ _ x _ _ x
                            // _ _ _ _ _ x
                            // _ _ _ _ _ _
                            // _ _ _ _ _ _
                            // _ _ x _ _ _
                            // x x _ _ _ x
                            //Filled squares: r1c1, r1c2, r1c6, r2c3, r5c6, r6c3, r6c6
                            //Pieces: Carrot, Octopus Tentacle, Sushi, Pea, Octopus Weiner, Tomato
                            gameboard = new Gameboard(6, 6);
                            gameboard.FillSquare(1, 1);
                            gameboard.FillSquare(1, 2);
                            gameboard.FillSquare(1, 6);
                            gameboard.FillSquare(2, 3);
                            gameboard.FillSquare(5, 6);
                            gameboard.FillSquare(6, 3);
                            gameboard.FillSquare(6, 6);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.OctopusWiener));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.OctopusTentacle));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Pea));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Sushi));                           
                            break;
                        case int n when n >= 33 && n <= 36: throw new Exception("Level unimplemented");
                        case 37:
                            // _ _ _ _ _ x
                            // _ x _ _ _ x
                            // _ _ x _ _ _
                            // _ _ _ _ _ _
                            // x _ _ _ _ _
                            // x _ _ x x x
                            //Filled squares: r1c1, r1c4, r1c5, r1c6, r2c1, r4c3, r5c2, r5c6, r6c6
                            //Pieces: L-Shaped Fish, Bent Wiener, Tomato, Sushi, Rolled Omelette, Carrot, 
                            gameboard = new Gameboard(6, 6);
                            gameboard.FillSquare(1, 1);
                            gameboard.FillSquare(1, 4);
                            gameboard.FillSquare(1, 5);
                            gameboard.FillSquare(1, 6);
                            gameboard.FillSquare(2, 1);
                            gameboard.FillSquare(4, 3);
                            gameboard.FillSquare(5, 2);
                            gameboard.FillSquare(5, 6);
                            gameboard.FillSquare(6, 6);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.LShapedBlueFish));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.BentWiener));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));                            
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.RolledOmelette));                            
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Sushi));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            break;
                        case 38: throw new Exception("Level unimplemented");
                        case 39:
                            // _ _ _ _ _ _
                            // _ _ x _ _ _
                            // _ _ _ _ x x
                            // x x _ _ _ _
                            // x _ _ _ _ _
                            // x _ _ _ _ x
                            //Filled squares: r1c1, r1c6, r2c1, r3c1, r3c2, r4c5, r4c6, r5c3
                            //Pieces: L-Shaped Squid, Sushi, Rolled Omelette, Carrot, Three Prong Thing, Shrimp x2
                            gameboard = new Gameboard(6, 6);
                            gameboard.FillSquare(1, 1);
                            gameboard.FillSquare(1, 6);
                            gameboard.FillSquare(2, 1);
                            gameboard.FillSquare(3, 1);
                            gameboard.FillSquare(3, 2);
                            gameboard.FillSquare(4, 5);
                            gameboard.FillSquare(4, 6);
                            gameboard.FillSquare(5, 3);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Sushi));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Shrimp));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.ThreeProngThing));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.LShapedSquid));                            
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Shrimp));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.RolledOmelette));                            
                            break;
                        case int n when n >= 40 && n <= 41: throw new Exception("Level unimplemented");
                        case 42:
                            // _ _ _ _ _ x
                            // x _ _ _ x x
                            // x _ _ _ _ _
                            // x _ _ _ _ _
                            // _ _ _ _ _ _
                            // _ _ _ x x _
                            //Filled squares: r1c4, r1c5, r3c1, r4c1, r5c1, r5c5, r5c6, r6c6
                            //Pieces: Shrimp, Tomto, Carrot, L-Shaped Blue Fish, Chicken Leg, Sushi
                            gameboard = new Gameboard(6, 6);
                            gameboard.FillSquare(1, 4);
                            gameboard.FillSquare(1, 5);
                            gameboard.FillSquare(3, 1);
                            gameboard.FillSquare(4, 1);
                            gameboard.FillSquare(5, 1);
                            gameboard.FillSquare(5, 5);
                            gameboard.FillSquare(5, 6);
                            gameboard.FillSquare(6, 6);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.ChickenLeg));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Shrimp));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.LShapedBlueFish));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Sushi));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            break;
                        case int n when n >= 43 && n <= 45: throw new Exception("Level unimplemented");
                        case 46:
                            // _ _ _ _ _ _ _
                            // _ _ _ _ x _ _
                            // _ _ _ _ _ _ _
                            // x _ _ _ _ x _
                            // x _ _ _ _ _ x
                            // x _ _ _ _ _ x
                            // _ _ _ _ _ _ x
                            //Filled squares: r1c7, r2c1, r2c7, r3c1, r3c7, r4c1, r4c6, r6c5
                            //Pieces: Shrimp, Tomto, Carrot, L-Shaped Blue Fish, Chicken Leg, Sushi
                            gameboard = new Gameboard(7, 7);
                            gameboard.FillSquare(1, 7);
                            gameboard.FillSquare(2, 1);
                            gameboard.FillSquare(2, 7);
                            gameboard.FillSquare(3, 1);
                            gameboard.FillSquare(3, 7);
                            gameboard.FillSquare(4, 1);
                            gameboard.FillSquare(4, 6);
                            gameboard.FillSquare(6, 5);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.BentWiener));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.ChickenLeg));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.LShapedBlueFish));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Shrimp));                            
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Fillet));
                            break;
                        case 47:
                            // _ _ _ _ _ _ _
                            // _ x x _ x x _
                            // _ _ _ _ _ _ _
                            // _ _ _ _ _ _ _
                            // x _ _ _ _ _ x
                            // x x _ _ _ _ x
                            //Filled squares: r1c1, r1c2, r1c7, r2c1, r2c7, r5c2, r5c3, r5c5, r5c6
                            //Pieces: Fried Prawn, Rolled Omelette, Shrime, Pea, L-shaped Blue Fish, Leafy Mushroom, Tomato, Chicken Leg
                            gameboard = new Gameboard(6, 7);
                            gameboard.FillSquare(1,1);
                            gameboard.FillSquare(1,2);
                            gameboard.FillSquare(1,7);
                            gameboard.FillSquare(2,1);
                            gameboard.FillSquare(2,7);
                            gameboard.FillSquare(5,2);
                            gameboard.FillSquare(5,3);
                            gameboard.FillSquare(5,5);
                            gameboard.FillSquare(5,6);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.FriedPrawn));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.RolledOmelette));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Shrimp));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Pea));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.LShapedBlueFish));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.LeafyMushroom));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.ChickenLeg));
                            break;
                        case int n when n >= 48 && n <= 52: throw new Exception("Level unimplemented");
                        case 53:
                            // x _ _ _ _ x x
                            // _ _ _ _ _ _ _
                            // x _ x x _ _ _
                            // _ _ _ _ _ _ _
                            // _ _ _ _ _ _ _
                            // _ _ _ x _ _ _
                            // _ _ _ x _ _ x
                            //Filled squares: r1c4, r1c7, r2c4, r5c1, r5c3, r5c4, r7c1, r7c6, r7c7
                            //Pieces: Octopus Weiner, Rolled Omelette x2, Marshmallow x2, Tomato, Shrimp x2, Sushi, Carrot
                            gameboard = new Gameboard(7, 7);
                            gameboard.FillSquare(1, 4);
                            gameboard.FillSquare(1, 7);
                            gameboard.FillSquare(2, 4);
                            gameboard.FillSquare(5 ,1);
                            gameboard.FillSquare(5, 3);
                            gameboard.FillSquare(5, 4);
                            gameboard.FillSquare(7, 1);
                            gameboard.FillSquare(7, 6);
                            gameboard.FillSquare(7, 7);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.OctopusWiener));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.RolledOmelette));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Marshmallows));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Shrimp));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Shrimp));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Marshmallows));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Sushi));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.RolledOmelette));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            break;
                        case int n when n >= 54 && n <= 55: throw new Exception("Level unimplemented");
                        case 56:
                            // _ _ _ _ _ x _ _
                            // _ _ _ x _ x _ _
                            // _ _ _ x _ _ _ _
                            // _ _ x _ _ _ _ _
                            // _ _ _ _ _ _ _ _
                            // x _ _ _ _ x _ _
                            // x x _ _ _ x x x
                            //Filled squares: r1c1, r1c2, r1c6, r1c7, r1c8, r2c1, r2c6, r4c3, r5c4, r6c4, r6c6, r7c6
                            //Pieces: 
                            gameboard = new Gameboard(7, 8);
                            gameboard.FillSquare(1,1);
                            gameboard.FillSquare(1,2);
                            gameboard.FillSquare(1,6);
                            gameboard.FillSquare(1,7);
                            gameboard.FillSquare(1,8);
                            gameboard.FillSquare(2,1);
                            gameboard.FillSquare(2,6);
                            gameboard.FillSquare(4,3);
                            gameboard.FillSquare(5,4);
                            gameboard.FillSquare(6,4);
                            gameboard.FillSquare(6,6);
                            gameboard.FillSquare(7,6);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.PeaPods));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.RolledOmelette));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Shrimp));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.ChickenLeg));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Sushi));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Pea));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Carrot));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Egg));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.BentWiener));                            
                            break;

                        case int n when n >= 57 && n <= 96: throw new Exception("Level unimplemented");
                        default: throw new Exception("There's only 96 levels in the main game, yo.");
                    }
                    break;
                case ExpansionPack.FirstLevelPack:
                    switch (level)
                    {
                        case int n when n >= 1 && n <= 24: throw new Exception("Level unimplemented");
                        default: throw new Exception("There's only 24 levels in the first level pack, yo.");
                    }
                    //break
                case ExpansionPack.SecondLevelPack:
                    switch (level)
                    {
                        case int n when n >= 1 && n <= 24: throw new Exception("Level unimplemented");
                        default: throw new Exception("There's only 24 levels in the second level pack, yo.");
                    }
                    //break;
                case ExpansionPack.ThirdLevelPack:
                    switch (level)
                    {   
                        //TODO, EVENTUALLY: do "case 6" from this drawing (See below)
                        //eg. 3rd DLC, 6th puzzle: 7 columns, 7 rows; r1c1 is the bottom-left square
                        //
                        //  _ _ _ _ _ _ _
                        //  _ _ _ x _ _ x
                        //  _ _ _ x _ _ _
                        //  _ _ _ _ _ _ _
                        //  _ _ _ _ _ _ x
                        //  x x _ _ x _ _
                        //  x _ _ _ _ _ _
                        //
                        //  Filled squares: r1c1, r2c1, r2c2, r2c5, r3c7, r5c4, r6c4, r6c7

                        case int n when n >= 1 && n <= 10: throw new Exception("Level unimplemented");
                        case 11:
                            //3rd DLC, 11th puzzle: 8 columns, 7 rows; r1c1 is the bottom-left square
                            //
                            //  x _ _ x _ _ _ x
                            //  _ _ _ _ _ _ _ x
                            //  _ _ _ _ _ _ _ _
                            //  x _ x _ _ x _ _
                            //  _ _ _ _ x x _ _
                            //  _ _ _ _ _ _ _ _
                            //  _ _ _ _ _ _ x x
                            //
                            //Filled squares: r1c7, r1c8, r3c5, r3c6, r4c1, r4c3, r4c6, r6c8, r7c1, r7c4, r7c8
                            //Pieces: 1x Tomato, 2x Octopus Weiner, 1x Dango, 1x Blue Fish, 1x Marshmallow, 1x Sushi, 1x Rolled Omellete, 1x Mushroom
                            gameboard = new(7, 8);
                            gameboard.FillSquare(1, 7);
                            gameboard.FillSquare(1, 8);
                            gameboard.FillSquare(3, 5);
                            gameboard.FillSquare(3, 6);
                            gameboard.FillSquare(4, 1);
                            gameboard.FillSquare(4, 3);
                            gameboard.FillSquare(4, 6);
                            gameboard.FillSquare(6, 8);
                            gameboard.FillSquare(7, 1);
                            gameboard.FillSquare(7, 4);
                            gameboard.FillSquare(7, 8);
                            pieces = new();
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Tomato));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.OctopusWiener));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.OctopusWiener));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Dango));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.LShapedBlueFish));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Marshmallows));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Sushi));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.RolledOmelette));
                            pieces.AddToBag(pieces.CreateGamePiece(GamePieceType.Mushroom));                            
                            break;
                        case int n when n >= 12 && n <= 24: throw new Exception("Level unimplemented");
                        default: throw new Exception("There's only 24 levels in the third level pack, yo.");
                    }
                    break;
                default: throw new Exception("LevelCreator.CreateLevel() says: Unrecognized value from ExpansionPack enum");
            }

            return (gameboard, pieces);
        }
    }
}
