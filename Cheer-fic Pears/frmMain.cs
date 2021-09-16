using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

//Rahave Suresan
//Jan.29.2021
//Form for Game

namespace Cheer_fic_Pears
{
    public partial class frmMain : Form
    {
        //Global Variables
        Board oBoard;
        int[,] SquareValues;

        int SquareSize = 60;
        int Offset = 60;
        int Score;
        int TimeLength;
        int AnimationCounter, AnimationLength, ComboCounter;

        List<Point> MasterMatchList;
        List<int> SquaresToDrop;
        List<int> NeedToRemove;
        List<Label> PointList;


        Point FirstClick, SecondClick, Difference;
        bool GameOver = false;
        Square Marker;

        //Game Timer
        Stopwatch GameTimer = new Stopwatch();

        enum States
        {
            Normal,
            Swapping,
            SwappingBack,
            Removing,
            Dropping
        };
        States GameState = States.Normal;

        public frmMain(int GameLength)
        {
            InitializeComponent();

            //initialize Square Values
            SquareValues = new int[9, 9];

            oBoard = new Board(9, 9, SquareSize, Offset, SquareValues);
            MasterMatchList = new List<Point>();
            PointList = new List<Label>();
            TimeLength = GameLength;

            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    this.Controls.Add(oBoard.GameBoard [i,j]);

            Intalize();

            if (GameLength < 10)
                lblTimeLimit.Text = String.Format("0{0}:00:000", GameLength);
        }


        private void Intalize ()
        {
            //initalize board
            Random Temp = new Random();
            int RandomNum = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    do
                    {
                        //RandomNum = Temp.Next(1, 7);
                        //SquareValues[i, j] = RandomNum;
                        do
                        {
                            RandomNum = Temp.Next(1, 7);
                        } while (SquareValues[i, j] == RandomNum);
                        SquareValues[i, j] = RandomNum;
                    }
                    while (CheckMatch(i, j, true)); //randmoize till no match

                }
            }

            //Set Values
            Score = 0;
            FirstClick = new Point(-1, -1);//invalid
            SecondClick = new Point(-1, -1);

            //start game timer
            GameTimer.Start();
            tmrGame.Enabled = true;

            //setup board
            oBoard.SetBoard();
        }

        public void Reset ()
        {
            //Clear SquareValues
            Array.Clear(SquareValues, 0, SquareValues.Length);

            //randomize
            Random Temp = new Random();
            int RandomNum;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    do
                    {
                        //RandomNum = Temp.Next(1, 7);
                        //SquareValues[i, j] = RandomNum;
                        do
                        {
                            RandomNum = Temp.Next(1, 7);
                        } while (SquareValues[i, j] == RandomNum);
                        SquareValues[i, j] = RandomNum;
                    }
                    while (CheckMatch(i, j, true)); //randmoize till no match

                }
            }

            //rest values
            Score = 0;
            //ComboCounter = 0;
            lblScore.Text = "0";
            //lblComboText.Text = "Combo";
            //lblCombo.Text = " 0 (0x)";
            FirstClick = new Point(-1, -1);
            SecondClick = new Point(-1, -1);

            //restart timers
            GameTimer.Reset();
            GameTimer.Start();
            tmrGame.Start();

            //set board
            oBoard.SetBoard();
        }

        //Menu Items/Game Controls
        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            //Game Keyboard ShortCuts
            if (e.KeyCode == Keys.R)
                mnuResetGame.PerformClick();
            if (e.KeyCode == Keys.Escape)
                mnuExitGame.PerformClick();
        }

        private void mnuResetGame_Click(object sender, EventArgs e)
        {
            if (GameState == States.Normal)
                Reset();
            else
                MessageBox.Show("Game cannot be reseted. Wait for moves to be complete.");
        }

        private void mnuExitGame_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void mnuHelp_Click(object sender, EventArgs e)
        {
            tmrGame.Stop();
            MessageBox.Show("How to Play: \nThe goal is to find a match of 3 or more fruit. When possible match is found, click on the fruit and the adjacent fruit to swap them. If no match is found, they will swap back. \n" +
                             "\nPoint System: \nPoints correspond with how many fruit are in a match. A match of three is 30 points. \n" +
                             "\nControls: \n Press R to restart the game.\n Press the escape key to return to main menu");
            tmrGame.Start();
           
        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            tmrGame.Stop();
            if (GameOver == false && MessageBox.Show("Do you want to exit the game?", "Exit Game", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                e.Cancel = true;
            tmrGame.Start();
        }

        private bool CheckMatch (int Row, int Col, bool RestState)
        {
            if (CheckDirections(Row, Col, RestState, true) | CheckDirections (Row, Col, RestState, false))
                return true;
            else
                return false;
        }


        private bool CheckDirections (int Row, int Col, bool RestState, bool CheckVertical)
        {
            //Store Match Type
            int MatchType = SquareValues[Row, Col];

            //delet list for points
            List<Point> MatchList = new List<Point> { new Point(Col, Row) };

            //Checking Vertical Direction
            if (CheckVertical == true)
            {
                //up
                int CheckRow = Row - 1;
                while (CheckRow >= 0 && SquareValues[CheckRow, Col] == MatchType)
                {
                    //add to list
                    MatchList.Add(new Point(Col, CheckRow));
                    //Check next row above
                    CheckRow--;
                }

                //down
                CheckRow = Row + 1;
                while (CheckRow <= 8 && SquareValues[CheckRow, Col] == MatchType)
                {
                    //add to list
                    MatchList.Add(new Point(Col, CheckRow));
                    //check next row below
                    CheckRow++;
                }
            }
            else //Check Horizontal directions
            {
                //right
                int CheckCol = Col + 1;
                while (CheckCol <= 8 && SquareValues[Row, CheckCol] == MatchType)
                {
                    //add to list
                    MatchList.Add(new Point(CheckCol, Row));
                    //check next row to right
                    CheckCol++;
                }

                //left
                CheckCol = Col - 1;
                while (CheckCol >= 0 && SquareValues[Row, CheckCol] == MatchType)
                {
                    //add to list
                    MatchList.Add(new Point(CheckCol, Row));
                    //check row to the left
                    CheckCol--;
                }
            }
            //If 3 or more match then true
            if (MatchList.Count >= 3)
            {
                //during the game
                if (RestState == false)
                {
                    //add points to grand match list
                    MasterMatchList.AddRange(MatchList);

                    //matching points
                    foreach (Point ThisPoint in MatchList)
                        oBoard.GameBoard[ThisPoint.Y, ThisPoint.X].Point = 10 * MatchList.Count;
                }

                return true;
            }
            else
                return false;
        }

        private void frmMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (oBoard == null)
                return;

            if (GameState != States.Normal)
                return;

            int PosX = e.X * oBoard.Columns + Offset;
            int PosY = e.Y * oBoard.Rows + Offset;

            if (e.X < Offset && e.X > PosX && e.Y < Offset && e.Y > PosY)
                return;

            int X = (e.X - Offset) / SquareSize;
            int Y = (e.Y - Offset) / SquareSize;

            //Square the user clicked
            Point Temp = new Point(X, Y);

            // if First click "null"
            if (FirstClick == new Point (-1,-1))
            {
                FirstClick = Temp;

                //add 
                Marker = new Square
                {
                    Size = new Size(60, 60),
                    Image = Resource.Border,
                    BackColor = Color.Transparent
                };
                oBoard.GameBoard[FirstClick.Y, FirstClick.X].Controls.Add(Marker);
                Marker.BringToFront();
            }
            else
            {
                //store Second Click and remove marker 
                SecondClick = Temp;
                Marker.Dispose();

                //check if squares check are beside eachother
                if ((Math.Abs(FirstClick.X -SecondClick.X)) == 1 && FirstClick.Y == SecondClick.Y || 
                     (Math.Abs(FirstClick.Y - SecondClick.Y) == 1 && FirstClick.X == SecondClick.X))
                {
                    //swap
                    Swap(true);
                }
                else
                {
                    //reset value of first click
                    FirstClick = new Point(-1, -1);
                    
                }
                
            }

        }

        //Animations
        private Bitmap OpacityLevel (Image Image, int Alpha)
        {
            //store image and color
            Bitmap Original = new Bitmap(Image);
            Bitmap NewImage = new Bitmap(Image.Width, Image.Height);
            Color OriginalColour;
            Color NewColour;

            //loop through pixel
            for (int i = 0; i < Image.Width; i++)
            {
                for (int j = 0; j < Image.Height; j++)
                {
                    //retrieve orginal colour 
                    OriginalColour = Original.GetPixel(i, j);
                    //add alpha (controls opaqueness)
                    NewColour = Color.FromArgb(Alpha, OriginalColour.R, OriginalColour.G, OriginalColour.B);
                    //set pixel
                    NewImage.SetPixel(i, j, NewColour);
                }
            }
            //return image with alpha
            return NewImage;
        }
        private void tmrAnimation_Tick(object sender, EventArgs e)
        {
            //animation based on game state
            if (GameState == States.Swapping || GameState == States.SwappingBack)
            {
                //move squares pixel to other orginal starting postion
                oBoard.GameBoard[FirstClick.Y, FirstClick.X].Left -= Difference.X * 2;
                oBoard.GameBoard[SecondClick.Y, SecondClick.X].Left += Difference.X *2 ;
                oBoard.GameBoard[FirstClick.Y, FirstClick.X].Top -= Difference.Y * 2;
                oBoard.GameBoard[SecondClick.Y, SecondClick.X].Top += Difference.Y *2;

                AnimationCounter++;
                if (AnimationCounter == AnimationLength)
                {
                    //stop timer
                    tmrAnimation.Stop();
                    if (GameState == States.Swapping)
                    {
                        AfterSwap(true);
                    }
                    else
                    {
                        AfterSwap(false);
                    }
                } 
            }
            else if ( GameState == States.Removing)
            {
                //new images (possible)
                Image NewPixelFruit1 = null;
                Image NewPixelFruit2 = null;
                Image NewPixelFruit3 = null;
                Image NewPixelFruit4 = null;
                Image NewPixelFruit5 = null;
                Image NewPixelFruit6 = null;

                //alpah (colour compotent) linked with animation tick, opaciness decrease ever 15 counts
                int A = (255 - AnimationCounter * 15 - 1);

                //create images for needed squares
                foreach (int Value in NeedToRemove)
                {
                    if (Value == 1)
                        NewPixelFruit1 = OpacityLevel(Resource.PixelFruit1, A);
                    else if (Value == 2)
                        NewPixelFruit2 = OpacityLevel(Resource.PixelFruit2, A);
                    else if (Value == 3)
                        NewPixelFruit3 = OpacityLevel(Resource.PixelFruit3, A);
                    else if (Value == 4)
                        NewPixelFruit4 = OpacityLevel(Resource.PixelFruit4, A);
                    else if (Value == 5)
                        NewPixelFruit5 = OpacityLevel(Resource.PixelFruit5, A);
                    else if (Value == 6)
                        NewPixelFruit6 = OpacityLevel(Resource.PixelFruit6, A);
                }

                //assign image to square 
                foreach (Point Point in MasterMatchList)
                {
                    
                    if (SquareValues[Point.Y, Point.X] == 1)
                         oBoard.GameBoard[Point.Y, Point.X].Image = NewPixelFruit1;
                    if (SquareValues[Point.Y, Point.X] == 2)
                        oBoard.GameBoard[Point.Y, Point.X].Image = NewPixelFruit2;
                    if (SquareValues[Point.Y, Point.X] == 3)
                        oBoard.GameBoard[Point.Y, Point.X].Image = NewPixelFruit3;
                    if (SquareValues[Point.Y, Point.X] == 4)
                        oBoard.GameBoard[Point.Y, Point.X].Image = NewPixelFruit4;
                    if (SquareValues[Point.Y, Point.X] == 5)
                        oBoard.GameBoard[Point.Y, Point.X].Image = NewPixelFruit5;
                    if (SquareValues[Point.Y, Point.X] == 6)
                        oBoard.GameBoard[Point.Y, Point.X].Image = NewPixelFruit6;
                }

                AnimationCounter++;

                //if animation finished, stop timer 
                if (AnimationCounter == AnimationLength)
                {
                    tmrAnimation.Stop();
                    AfterRemoval();
                }
                
            }
            else if (GameState == States.Dropping)
            {
                foreach (int Col in SquaresToDrop)
                {
                    //move squares that weren't delete down
                    int EmptySpace = 0;
                    for (int Row = 8; Row >= 0; Row--)
                    {
                        if (SquareValues[Row, Col] == 0)
                            EmptySpace++;
                        else
                            oBoard.GameBoard[Row, Col].Top += 2 * EmptySpace;
                    }
                }

                AnimationCounter++;

                //animation done 
                if (AnimationCounter == AnimationLength / 2)
                {
                    tmrAnimation.Stop();
                    AfterDropSquares();
                    
                }
            }

        }
        private void BeginAnimation (States Type, int Length)
        {
            //reset values called again
            AnimationCounter = 0;
            GameState = Type;
            AnimationLength = Length;

            //start timer
            tmrAnimation.Start();
            
        }

        private void Swap (bool CorrectSwap)
        {
            //difference between squares (how much you will move by)
            Difference.X = FirstClick.X - SecondClick.X;
            Difference.Y = FirstClick.Y - SecondClick.Y;

            //animation
            if (CorrectSwap == true)
                BeginAnimation(States.Swapping, SquareSize / 2);
            else
                BeginAnimation(States.SwappingBack, SquareSize / 2);
        }

        private void AfterSwap (bool FirstSwap)
        {
            //swap in array
            int Temp = SquareValues[FirstClick.Y, FirstClick.X];
            SquareValues[FirstClick.Y, FirstClick.X] = SquareValues[SecondClick.Y, SecondClick.X];
            SquareValues[SecondClick.Y, SecondClick.X] = Temp;

            //reset locations and change image
            oBoard.GameBoard[FirstClick.Y, FirstClick.X].Location = new Point(Offset + FirstClick.X * SquareSize, Offset + FirstClick.Y * SquareSize );
            oBoard.GameBoard[SecondClick.Y, SecondClick.X].Location = new Point(Offset + SecondClick.X * SquareSize, Offset + SecondClick.Y * SquareSize);
            oBoard.SetSquare(FirstClick.Y, FirstClick.X);
            oBoard.SetSquare(SecondClick.Y, SecondClick.X);

            if (FirstSwap)
            {
                //check for a match 3 or more
                if (CheckMatch(FirstClick.Y, FirstClick.X, false) | CheckMatch(SecondClick.Y, SecondClick.X, false))
                {
                    //combo info
                    //ComboCounter = 1;
                    //lblCombo.Text = "1 Combo (1x)";
                    RemoveSquare();
                }
                else
                    Swap(false);
            }
            else
            {
                //not possible, let user click again
                FirstClick = new Point(-1, -1);
                GameState = States.Normal;
            }


        }

        private void RemoveSquare ()
        {
            MasterMatchList = MasterMatchList.Distinct().ToList();

            //list for values needed to be removed from squarevalues 
            NeedToRemove = new List<int>();
            foreach (Point Point in MasterMatchList)
                NeedToRemove.Add(SquareValues[Point.Y, Point.X]);
            NeedToRemove = NeedToRemove.Distinct().ToList();

            //begin animation
            BeginAnimation(States.Removing, 15);
        }

        private void AfterRemoval ()
        {
            //loop to point to see which to remove
            foreach(Point ThisPoint in MasterMatchList)
            {
                SquareValues[ThisPoint.Y, ThisPoint.X] = 0;

                //calcaulte point (square point * combo)
                int ThisPointValue = (int)(oBoard.GameBoard[ThisPoint.Y, ThisPoint.X].Point); //* Math.Pow(2, ComboCounter - 1));

                //label to display point
                Label lblPointValue = new Label
                {
                    //properties
                    Text = ThisPointValue.ToString(),
                    AutoSize = true,
                    Font = new Font("Microsft Sans Serif", 12, FontStyle.Regular),
                    ForeColor = Color.Black

                };

                //resize and center
                if (ThisPointValue < 100)
                    lblPointValue.Size = new Size(30, 20);
                else if (ThisPointValue < 1000)
                    lblPointValue.Size = new Size(40, 20);
                else
                    lblPointValue.Size = new Size(50, 20);

                //center label
                int X = ThisPoint.X * SquareSize + Offset + SquareSize / 2 - lblPointValue.Width / 2;
                int Y = ThisPoint.Y * SquareSize + Offset + SquareSize / 2 - lblPointValue.Height / 2;
                lblPointValue.Location = new Point(X,Y);

                //add to list 
                PointList.Add(lblPointValue);
                //draw label
                Controls.Add(lblPointValue);
                //bring to front
                lblPointValue.BringToFront();

                //Adding Score
                Score += ThisPointValue;
                //reset Square Point
                oBoard.GameBoard[ThisPoint.Y, ThisPoint.X].Point = 0;
                //set square
                oBoard.SetSquare(ThisPoint.Y, ThisPoint.X);

            }

            lblScore.Text = Score.ToString();
            SquaresToDrop = new List<int>(MasterMatchList.Select (x => x.X).Distinct().ToList());
            //reset for next time
            MasterMatchList = new List<Point>();
            //begin the dropping of squares
            DropSquare();
        }
        private void DropSquare()
        {
            BeginAnimation (States.Dropping, SquareSize);
        }


        private void AfterDropSquares()
        {
            //clear pointlist and remake list
            foreach (Label PointLabel in PointList)
                PointLabel.Dispose();
            PointList = new List<Label>();

            Random r = new Random(); // to generate new square value for empty
            List<Point> MovedSquaresToCheck = new List<Point>();

            foreach (int Col in SquaresToDrop)
            {
                int EmptySpaceInCol = 0;

                //reset location of squares in columns 
                for (int Row = 0; Row < 9; Row++)
                {
                    oBoard.GameBoard[Row, Col].Location = new Point( Offset + Col * SquareSize, Offset + Row * SquareSize);
                    if (SquareValues[Row, Col] == 0)
                        EmptySpaceInCol++;
                }

                for (int Row = 8; Row >= 0; Row--)
                {
                    if (Row >= EmptySpaceInCol)
                    {
                        if (SquareValues[Row, Col] == 0)
                        {
                            for (int RowAbove = Row - 1; RowAbove >= 0; RowAbove--)
                            {
                                if (SquareValues[RowAbove, Col] != 0)
                                {
                                    //move down 
                                    SquareValues[Row, Col] = SquareValues[RowAbove, Col];
                                    SquareValues[RowAbove, Col] = 0;
                                    //update image 
                                    oBoard.SetSquare(Row, Col);
                                    //add to list to check for match
                                    MovedSquaresToCheck.Add(new Point(Col, Row));
                                    break;
                                }
                            }
                        }
                        
                    }
                    else
                    {
                        // for an empty space, assign new random value 
                        SquareValues[Row, Col] = r.Next(1, 7);
                        //add to list to check for match 
                        MovedSquaresToCheck.Add(new Point(Col, Row));
                        //set new image
                        oBoard.SetSquare(Row, Col);
                    }
                   
                }

            }
            CheckNewSquares(MovedSquaresToCheck);
        }

        private void CheckNewSquares (List <Point> CheckSquares)
        {
            //check for match 
            foreach (Point Square in CheckSquares)
                CheckMatch(Square.Y, Square.X, false);
            //clear list
            CheckSquares = new List<Point>();

            if (MasterMatchList.Count > 0)
            {
                //increment combo
                //ComboCounter++;
                //lblCombo.Text = (ComboCounter).ToString() + "(" + Math.Pow(2, ComboCounter - 1) + "x)";
                RemoveSquare();
            }
            else
            {
                //clear First click
                FirstClick = new Point(-1, -1);
                GameState = States.Normal; 

            }
        }

        private void EndGame (bool TimeUp)
        {
            tmrGame.Stop();
            tmrAnimation.Stop();
            GameTimer.Stop();

            if (TimeUp == true)
                MessageBox.Show("Time is up. Well Done");
            else
                MessageBox.Show("No more possible move.");
            GameOver = true;
        }

        private void tmrGame_Tick(object sender, EventArgs e)
        {
            TimeSpan ts = GameTimer.Elapsed;
            lblTime.Text = String.Format("{0:00}:{1:00}:{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds);
            if (ts.Minutes >= TimeLength)
                EndGame(true);
        }

        
    }
}
