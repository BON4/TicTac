using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Lib;

/// <summary>
/// TODO
/// 1.Когда ходит АИ нужно чтобы возвращало координаты, чтобы могло программа могла отрисовать объект в нужных координате.
/// 
/// </summary>

namespace TicTacToeGraphics
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private TicTac game;
		private ICaretaker caretaker;
		private int Score_O_Count;
		private int Score_X_Count;

		double circle_width = 150;
		double circle_height = 150;

		double cross_width = 140;
		double cross_height = 140;

		public MainWindow()
		{
			Score_O_Count = 0;
			Score_X_Count = 0;
			InitializeComponent();
			NewGame();
		}

		private void NewGame()
		{
			game = new TicTac();
			caretaker = new Caretaker(game);
			Canvas1.Children.RemoveRange(8, VisualTreeHelper.GetChildrenCount(Canvas1) - 8);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			int count = VisualTreeHelper.GetChildrenCount(Canvas1);
			if (count - 8 > 0)
			{
				Canvas1.Children.RemoveAt(count-1);
			}
			caretaker.Undo();
		}

		//private void AI_On(object sender, RoutedEventArgs e)
		//{
		//	for (int i = 0; i < VisualTreeHelper.GetChildrenCount(Canvas1); i++)
		//	{
		//		var obj = VisualTreeHelper.GetChild(Canvas1, i);
		//		if (obj.GetType() == typeof(Button))
		//		{
		//			var child = VisualTreeHelper.GetChild(Canvas1, i) as Button;
		//			if (child.Name == "AI")
		//			{
		//				if (AI_Mode == true)
		//					AI_Mode = false;
		//				else if (AI_Mode == false)
		//					AI_Mode = true;

		//				if (AI_Mode == true)
		//					child.Background = Brushes.White;
		//				else if (AI_Mode == false)
		//					child.Background = Brushes.Red;
		//			}
		//		}
		//	}
		//	if (AI_Mode == true & caretaker.GetGoFirst() == true)
		//	{
		//		if (caretaker.GetTurn() == 0)
		//		{
		//			circle_draw(X - circle_width / 2, Y - circle_height / 2, circle_width, circle_height, Canvas1);
		//		}
		//		else if (caretaker.GetTurn() == 1)
		//		{
		//			cross_draw(X + cross_width - cross_width / 2, Y + cross_height - cross_height / 2, cross_width, cross_height, Canvas1);
		//		}
		//	}
		//}

		private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (caretaker.GetEnd())
			{
				NewGame();
				return;
			}

			System.Windows.Point p = Mouse.GetPosition(Canvas1);

			double X = Convert_ToGridCordinates(p.X);
			double Y = Convert_ToGridCordinates(p.Y);


			if (double.IsNaN(X) || double.IsNaN(Y))
			{
				return;
			}

			int X_matr = Convert_ToMatrixCordinates((int)X);
			int Y_matr = Convert_ToMatrixCordinates((int)Y);

			if(!game.MakePlase(X_matr + Y_matr*3 +1))
			{
				return;
			}

			if (caretaker.GetTurn() == 0)
			{
				circle_draw(X - circle_width / 2, Y - circle_height / 2, circle_width, circle_height, Canvas1);
			}
			else if (caretaker.GetTurn() == 1)
			{
				cross_draw(X + cross_width - cross_width / 2, Y + cross_height - cross_height / 2, cross_width, cross_height, Canvas1);
			}


			caretaker.Backup();

			int winner = game.Check_For_Win();


			if (winner == 0)
			{
				Score_O_Count++;
				caretaker.Backup();
			}
			else if (winner == 1)
			{
				Score_X_Count++;
				caretaker.Backup();
			}
		}

		private void CleanUp()
		{
			Canvas1.Children.Clear();
		}

		private void circle_draw(double x, double y, double width, double height, Canvas cv)
		{
			Ellipse circle = new Ellipse()
			{
				Width = width,
				Height = height,
				Stroke = System.Windows.Media.Brushes.Red,
				StrokeThickness = 6
			};

			cv.Children.Add(circle);

			circle.SetValue(Canvas.LeftProperty, x);
			circle.SetValue(Canvas.TopProperty, y);
		}

		private void cross_draw(double x, double y, double width, double height, Canvas cv)
		{
			Grid g = new Grid();

			Line line1 = new Line()
			{
				X1 = x,
				Y1 = y,
				X2 = x-width,
				Y2 = y-height,
				Stroke = System.Windows.Media.Brushes.Blue,
				StrokeThickness = 6
			};

			Line line2 = new Line()
			{
				X1 = x-width,
				Y1 = y,
				X2 = x,
				Y2 = y-height,
				Stroke = System.Windows.Media.Brushes.Blue,
				StrokeThickness = 6,
				
			};

			g.Children.Add(line1);
			g.Children.Add(line2);

			cv.Children.Add(g);
			//cv.Children.Add(line1);
			//cv.Children.Add(line2);
		}

		private double Convert_ToGridCordinates(double X)
		{
			if (0 <= X & X <= 200)
			{
				return 100.0;
			}
			else if (201 <= X & X <= 400)
			{
				return 300.0;
			}
			else if (401 <= X & X <= 600)
			{
				return 500.0;
			}
			else
				return double.NaN;
		}

		public int Convert_ToMatrixCordinates(int X)
		{
			return (X / 100) / 2;
		}
	}
}
