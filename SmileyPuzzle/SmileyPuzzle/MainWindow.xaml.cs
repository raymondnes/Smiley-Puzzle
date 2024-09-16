using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {

		Random myRand = new Random();
        public MainWindow()
        {
            InitializeComponent();
			((TextBox)FindName("LogText")).Text = "";
		}

		string logtexts = "";
		void l(string t)
		{
			//Note the "reverselogging": uppermost row is the latest one!
			logtexts = t + "\n" + logtexts;
		}


		static int[,] pieces = { {1, 2, 3 },
								{ 4, 5, 6 },
								{ 7, 8, 9 } };

		bool pieceswap(int i, int j, int x, int y)
		{
			if (
				((i >= 0) && (i < pieces.GetLength(0))) &&
				((x >= 0) && (x < pieces.GetLength(0))) &&
				((j >= 0) && (j < pieces.GetLength(1))) &&
				((y >= 0) && (y < pieces.GetLength(1))))
			{
				int t = pieces[i, j];
				pieces[i, j] = pieces[x, y];
				pieces[x, y] = t;
				return true;
			}
			else
			{
				l("Njah... either (" + i + "," + j + ") or (" + x + "," + y + ") does not even exist!");
				return false;
			}
		}

		void draw()
		{
			int i, j;
			for (i = 0; i < pieces.GetLength(0); i++)
			{
				for (j = 0; j < pieces.GetLength(1); j++)
				{
					((Image)FindName("I" + i + j)).Source = new BitmapImage(new Uri(@"/SmileyPuzzle;component/Images/piece" + pieces[i,j] + ".png", UriKind.Relative));
				}
			}
			((TextBox)FindName("LogText")).Text = logtexts;
		}

		void stironce()
		{
			//Read this block carefully, when you understand it a lot of timewasting Googling will be unneccessary!
			//You can add logging while figuring this out, but also remember that Console.WriteLine is at your disposal!!!

			int i, j;
			j = 0;
			bool foundit = false;
			for (i = 0; i < pieces.GetLength(0); i++)
			{
				for (j = 0; j < pieces.GetLength(1); j++)
				{
					if (pieces[i, j] == 0)
					{
						foundit = true;
						break;
					}
				}
				if (foundit) break;
			}
			bool success = false;
			
			while (!success)
			{
				double direction = myRand.NextDouble();
				if (direction <= 0.25) { if (i > 0) success = pieceswap(i, j, i - 1, j); }
				else if (direction <= 0.50) { if (j + 1 < pieces.GetLength(1)) success = pieceswap(i, j, i, j + 1); }
				else if (direction <= 0.75) { if (i + 1 < pieces.GetLength(0)) success = pieceswap(i, j, i + 1, j); }
				else if (direction <= 1) { if (j > 0) success = pieceswap(i, j, i, j - 1); }
			}
		}

		private void buttonclickdummyForWPF(object sender, RoutedEventArgs e)
		{
			stir();
		}
		void stir()
		{
			l("Move pieces randomly.\n");
			if (pieces[2, 2] == 9)
			{
				l("It appears we're in a start state, so let's cut some corners.");
				pieces[2, 2] = 0;
			}
			int i;
			for (i = 0; i < 100; i++) stironce();
			draw();
		}


		private void imageclickdummyForWPF(object sender, MouseButtonEventArgs e)
		{

			int num = -1;
			if (sender == FindName("I00")) num = 1;
			if (sender == FindName("I01")) num = 2;
			if (sender == FindName("I02")) num = 3;

			if (sender == FindName("I10")) num = 4;
			if (sender == FindName("I11")) num = 5;
			if (sender == FindName("I12")) num = 6;

			if (sender == FindName("I20")) num = 7;
			if (sender == FindName("I21")) num = 8;
			if (sender == FindName("I22")) num = 9;
			squareclick(num);
			draw();
		}


		/* To solve this exercise you don't need to change anyof the code above this row, but you do have to read and understand it in order to implement the method missing below.
		   In addition to the method you'll need a variable or two, because depending on your implementation approach you'll want to keep track of earlier click in addition to the "current" one 
		   (the demonstration in the classroom used that approach).
		   Once you got a couple of clicked locations stashed away, you have to check whether the move is allowed, because the code above does not do that for you. The above code does give
		   you some pointers for implementing that, though!
		   Finally a warning: As WPF-code this roll-of-bubblegum is not all that academic, but the point here is not to make bestin-in-class WPF-code: you'll get a lookalike in
		   JavaScript later and the structure of that code can now be exactly the same.
		*/
		
		void squareclick(int n)
		{
			l("Your task is to implement the method \"squareclick\" from which this message was just logged. So, open up MainWindow.xaml.cs and scroll to the end of the file to start working!");
		// Find the position of the clicked piece (n)
		    int emptyX = -1, emptyY = -1, pieceX = -1, pieceY = -1;

		    // Find the coordinates of the empty space (0) and the clicked piece
		    for (int i = 0; i < pieces.GetLength(0); i++)
		    {
		        for (int j = 0; j < pieces.GetLength(1); j++)
		        {
		            if (pieces[i, j] == 0)
		            {
		                emptyX = i;
		                emptyY = j;
		            }
		            else if (pieces[i, j] == n)
		            {
		                pieceX = i;
		                pieceY = j;
		            }
		        }
		    }

		    // Check if the clicked piece is adjacent to the empty space
		    if ((Math.Abs(emptyX - pieceX) == 1 && emptyY == pieceY) || 
		        (Math.Abs(emptyY - pieceY) == 1 && emptyX == pieceX))
		    {
		        // Swap the clicked piece with the empty space
		        pieceswap(pieceX, pieceY, emptyX, emptyY);
		        l("Moved piece " + n + " to empty space.");
			}
			else
			{
				// Invalid move: clicked piece is not adjacent to the empty space
		        l("Piece " + n + " is not adjacent to the empty space.");
			}
			
			draw(); // Redraw the board after the move.
		}
		


    }
}
