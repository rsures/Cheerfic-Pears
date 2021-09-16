using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

//Rahave Suresan
//Jan.29.2021
//Game Board Properities 

namespace Cheer_fic_Pears
{
    class Board
    {
        //fields
        private Square[,] mGameBoard; //parrall to mSquares
        private int mRows;
        private int mColumns;
        private int mSquareSize;
        private int[,] mSquaresValue; //parallel to mBoard



        //properties (all access only)
        public Square[,] GameBoard
        {
            get { return mGameBoard; }
        }
            
        public int Rows
        {
            get { return mRows; }
        }

        public int Columns
        {
            get { return mColumns; }
        }

        //constructors
        public Board (int Rows, int Columns, int SquareSize, int Offset, int [,] SquareValue)
        {
            this.mRows = Rows;
            this.mColumns = Columns;
            this.mSquareSize = SquareSize;
            this.mSquaresValue = SquareValue;

            //create array of squares
            mGameBoard = new Square[Rows, Columns];

            //assign values of each square
            for ( int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    mGameBoard[i, j] = new Square
                    {
                        Size = new Size(SquareSize, SquareSize),
                        Location = new Point(j * SquareSize + Offset, i * SquareSize + Offset),
                        BackColor = Color.FromArgb(206,92,92),
                    };
                }
            }
        }


        //Method 
        public void SetSquare (int Row, int Col)
        {
            //each number in SquareValue corresponds with an image
            Image UsePic;
            if (mSquaresValue[Row, Col] == 1)
                UsePic = Resource.PixelFruit1;
            else if (mSquaresValue[Row, Col] == 2)
                UsePic = Resource.PixelFruit2;
            else if (mSquaresValue[Row, Col] == 3)
                UsePic = Resource.PixelFruit3;
            else if (mSquaresValue[Row, Col] == 4)
                UsePic = Resource.PixelFruit4;
            else if (mSquaresValue[Row, Col] == 5)
                UsePic = Resource.PixelFruit5;
            else if (mSquaresValue[Row, Col] == 6)
                UsePic = Resource.PixelFruit6;
            else
                UsePic = Resource.Blank;

            //set images
            mGameBoard[Row, Col].Image = UsePic;
                
        }
        public void SetBoard ()
        {
            //set image for each square
            for (int i = 0; i < mRows; i++)
                for (int j = 0; j < mColumns; j++)
                    SetSquare(i, j);
         
        }


    }
}
