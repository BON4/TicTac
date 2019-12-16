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
using Lib;

namespace TicTacToe
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

			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(Container); i++)
			{
				var obj = VisualTreeHelper.GetChild(Container, i);
				if (obj.GetType() == typeof(Button))
				{
					var child = VisualTreeHelper.GetChild(Container, i) as Button;
					child.Content = string.Empty;
					child.Background = Brushes.White;
					child.Foreground = Brushes.Blue;
				}
			}

			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(TextContainer); i++)
			{
				var obj = VisualTreeHelper.GetChild(TextContainer, i);
				if (obj.GetType() == typeof(TextBlock))
				{
					var child = VisualTreeHelper.GetChild(TextContainer, i) as TextBlock;
					if (child.Name == "Score_O")
						child.Text = $"{Score_O_Count / 2}";
					else if (child.Name == "Score_X")
						child.Text = $"{Score_X_Count / 2}";
					else if (child.Name == "Turn_O")
						child.Text = "^";
					else if (child.Name == "Turn_X")
						child.Text = "^";
				}
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (caretaker.GetEnd())
			{
				NewGame();
				return;
			}

			var button = (Button)sender;

			int column = Grid.GetColumn(button);
			int row = Grid.GetRow(button)+1;

			game.MakePlase((row * 3 + column) - 2);

			if (caretaker.GetTurn() == 0)
			{
				button.Content = "O";
				button.Foreground = Brushes.Blue;
			}
			else if (caretaker.GetTurn() == 1)
			{
				button.Content = "X";
				button.Foreground = Brushes.Orange;
			}

			caretaker.Backup();

			int winner = game.Check_For_Win();


			if (winner == 0)
				Score_O_Count++;
			else if (winner == 1)
				Score_X_Count++;

			if (caretaker.GetEnd())
			{
				NewGame();
				return;
			}
		}

		private void Button_Backup_Click(object sender, RoutedEventArgs e)
		{
			caretaker.Undo();
			Backup_State();
		}

		private void Backup_State()
		{
			int[] arr = caretaker.GetArr();
			LinkedList<Button> button_arr = new LinkedList<Button>();

			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(Container); i++)
			{
				var obj = VisualTreeHelper.GetChild(Container, i);
				if (obj.GetType() == typeof(Button))
				{
					var child = VisualTreeHelper.GetChild(Container, i) as Button;
					button_arr.AddLast(child);
				}
			}

			int enumerator = 0;
			foreach(var item in button_arr)
			{
				if(arr[enumerator] == -1)
					item.Content = string.Empty;
				else if(arr[enumerator] == 0)
					item.Content = "O";
				else if (arr[enumerator] == 1)
					item.Content = "X";
				enumerator++;
			}
		}

	}
}
