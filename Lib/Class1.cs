using System;
using System.Collections.Generic;
using System.Linq;

namespace Lib
{
	public class TicTac
	{
		//1 - X, 0 - O
		private int[] tics;
		private bool EndGame;
		private int turn;
		private bool restored;
		public static Random random = new Random();

		public TicTac()
		{
			tics = new int[9];
			int[] arr = { -1, -1, -1, -1, -1, -1, -1, -1, -1 };
			arr.CopyTo(tics, 0);
			EndGame = false;
			turn = 0;
			restored = false;
		}

		public TicTac(int[] arr)
		{
			tics = new int[9];
			arr.CopyTo(tics, 0);
			EndGame = false;
			turn = 0;
			restored = false;
		}

		public void Print()
		{
			for (int i = 0; i < tics.Length; i++)
			{
				if (i == 3 | i == 6)
				{
					Console.Write($"\n {tics[i]} ");
				}
				else
				{
					Console.Write($" {tics[i]} ");
				}
			}
		}

		public bool MakePlase(int index = 0)
		{
			if (turn == 0)
			{
				if (IsEngaged(index))
					return false;
				PlaseO(index);
			}
			else
			{
				if (IsEngaged(index))
					return false;
				PlaseX(index);
			}
			return true;
		}

		public void PlaseX(int index = 0)
		{
			if (turn == 1)
			{
				Console.WriteLine("\nПоставь Х(1):");
				if (tics[index - 1] == 1 & tics[index - 1] != 0)
				{
					return;
				}
				else if (tics[index - 1] == -1)
				{
					tics[index - 1] = 1;
				}
				turn = 0;
			}
		}

		public void PlaseO(int index = 0)
		{
			if (turn == 0)
			{
				Console.WriteLine("\nПоставь O(0):");
				if (tics[index - 1] == 0 & tics[index - 1] != 1)
				{
					return;
				}
				else if (tics[index - 1] == -1)
				{
					tics[index - 1] = 0;
				}
				turn = 1;
			}
		}

		private int[,] Convert_to_2d(int[] arr)
		{
			int[,] new_arr = new int[3, 3];
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					new_arr[i, j] = arr[i * 3 + j];
				}
			}
			return new_arr;
		}

		public int Check_row(int[,] arr)
		{
			int countforP1 = 0;
			int countforP2 = 0;
			int dimention = arr.GetLength(0);

			for (int i = 0; i < dimention; i++)
			{
				countforP1 = 0;
				countforP2 = 0;
				for (int j = 0; j < dimention; j++)
				{
					if (arr[i, j] == 0)
					{
						countforP1++;
					}
					if (arr[i, j] == 1)
					{
						countforP2++;
					}

					if (countforP1 == dimention)
					{
						Console.WriteLine("Player O Wins !!");
						return 0;
					}
					if (countforP2 == dimention)
					{
						Console.WriteLine("Player X Wins !!");
						return 1;
					}
				}
			}
			return -1;
		}

		public int Check_col(int[,] arr)
		{
			int countforP1 = 0;
			int countforP2 = 0;
			int dimention = arr.GetLength(0);

			for (int i = 0; i < dimention; i++)
			{
				countforP1 = 0;
				countforP2 = 0;
				for (int j = 0; j < dimention; j++)
				{
					if (arr[j, i] == 0)
					{
						countforP1++;
					}
					if (arr[j, i] == 1)
					{
						countforP2++;
					}

					if (countforP1 == dimention)
					{
						Console.WriteLine("Player O Wins !!");
						return 0;
					}
					if (countforP2 == dimention)
					{
						Console.WriteLine("Player X Wins !!");
						return 1;
					}
				}
			}
			return -1;
		}

		public int Check_dieg(int[,] arr)
		{
			int countforP1 = 0;
			int countforP2 = 0;
			int dimention = arr.GetLength(0);


			for (int i = 0; i < dimention; i++)
			{
				if (arr[i, i] == 0)
				{
					countforP1++;
				}

				if (arr[i, i] == 1)
				{
					countforP2++;
				}

				if (countforP1 == dimention)
				{
					Console.WriteLine("Player O Wins !!");
					return 0;
				}
				if (countforP2 == dimention)
				{
					Console.WriteLine("Player X Wins !!");
					return 1;
				}
			}

			countforP1 = 0;
			countforP2 = 0;
			for (int i = 0; i < dimention; i++)
			{
				if (arr[i, dimention - i - 1] == 0)
				{
					countforP1++;
				}

				if (arr[i, dimention - i - 1] == 1)
				{
					countforP2++;
				}

				if (countforP1 == dimention)
				{
					Console.WriteLine("Player O Wins !!");
					return 0;
				}
				if (countforP2 == dimention)
				{
					Console.WriteLine("Player X Wins !!");
					return 1;
				}
			}
			return -1;
		}

		public int Check_For_Win()
		{
			int[,] arr = Convert_to_2d(tics);
			int dieg = Check_dieg(arr);
			int row = Check_row(arr);
			int col = Check_col(arr);
			if (row == 0 | dieg == 0 | col == 0)
			{
				Console.WriteLine("\nPlayer O Wins !!");
				EndGame = true;
				return 0;
			}
			else if (row == 1 | dieg == 1 | col == 1)
			{
				Console.WriteLine("\nPlayer X Wins !!");
				EndGame = true;
				return 1;
			}
			return -1;
		}

		private bool IsEngaged(int index)
		{
			if (tics[index - 1] == -1)
				return false;
			else
				return true;
		}

		public IMemento Save()
		{
			return new ConcreteMemento(tics, EndGame, turn, restored);
		}

		public void Restore(IMemento memento)
		{
			if (!(memento is ConcreteMemento))
			{
				throw new Exception($"Uknown memento class {memento.ToString()}");
			}

			tics = memento.GetState();
			EndGame = memento.GetEnd();
			turn = memento.GetTurn();
			restored = true;
			Console.WriteLine($"Orginator: My state change to >>> {memento.GetDate()}");
			Print();
		}
	}

	// Интерфейс Снимка предоставляет способ извлечения данных снимка.
	//Однако он не раскрывает состояние Создателя.
	public interface IMemento
	{
		string GetName();
		int[] GetState();
		DateTime GetDate();
		string GetArr();
		bool GetEnd();
		int GetTurn();
		bool GetRestored();
	}

	// Конкретный снимок содержит инфраструктуру для хранения состояния
	// Создателя.
	class ConcreteMemento : IMemento
	{
		private int[] _state;

		private bool _EndGame;

		private int _turn;

		private DateTime _date;

		private bool _restored; // TODO: Удалить это поле

		public ConcreteMemento(int[] state, bool EndGame, int turn, bool restored)
		{
			_state = new int[9];
			state.CopyTo(_state, 0);
			_date = DateTime.Now;
			_EndGame = EndGame;
			_turn = turn;
			_restored = restored;
		}

		public string GetArr()
		{
			string str_arr = "";
			foreach (int item in _state)
			{
				str_arr += $"{item}  ";
			}
			return str_arr;
		}

		public int[] GetState()
		{
			return _state;
		}

		public string GetName()
		{
			return $"{this._date} ///{GetArr()}";
		}

		public DateTime GetDate()
		{
			return _date;
		}

		public bool GetEnd()
		{
			return _EndGame;
		}

		public int GetTurn()
		{
			return _turn;
		}

		public bool GetRestored()
		{
			return _restored;
		}
	}

	public interface ICaretaker
	{
		bool GetEnd();
		int GetTurn();
		int[] GetArr();
		void Backup();
		void Undo();
		void ShowHistory();
	}


	public class Caretaker : ICaretaker
	{
		private LinkedList<IMemento> _mementos = new LinkedList<IMemento>();

		private TicTac _orginator;

		IMemento memento;

		public Caretaker(TicTac originator)
		{
			this._orginator = originator;
			memento = null;
			Backup();
		}

		public void Backup()
		{
			IMemento copy_check = this._orginator.Save();
			_mementos.AddLast(copy_check);
			memento = _mementos.Last(); //____________
		}

		private IMemento Get_Prev(IMemento mem)
		{
			LinkedListNode<IMemento> node = _mementos.FindLast(mem);
			if (node.Previous == null)
			{
				return null;
			}
			return node.Previous.Value;
		}

		public void Undo()
		{
			if (_mementos.Count == 0 | _mementos.Count == 1)
			{
				return;
			}

			if (memento == null)
			{
				memento = _mementos.Last();
			}
			else
			{
				memento = _mementos.Last();
				if (_mementos.Find(memento).Previous != null)
				{
					memento = _mementos.Find(memento).Previous.Value;
					_mementos.RemoveLast();
					ConcreteMemento concreteMemento = new ConcreteMemento(memento.GetState(),
						memento.GetEnd(),
						memento.GetTurn(),
						memento.GetRestored());

					memento = concreteMemento;
				}
			}

			if (memento == null)
			{
				memento = _mementos.Last();
			}
			else
			{
				//Console.WriteLine($"\nRestoring state to {memento.GetTurn()}");

				try
				{
					_orginator.Restore(memento);
				}
				catch (Exception)
				{
					Undo();
				}
			}
		}

		public void ShowHistory()
		{
			Console.WriteLine("Caretaker: Here's the list of mementos:");

			foreach (var memento_item in this._mementos)
			{
				Console.WriteLine(memento_item.GetName());
			}
		}

		public bool GetEnd()
		{
			if (memento != null)
				return memento.GetEnd();
			else
				return false;
		}

		public int GetTurn()
		{
			if (memento != null)
				return memento.GetTurn();
			else
				return -1;
		}

		public int[] GetArr()
		{
			if (memento != null)
				return memento.GetState();
			else
				return null;
		}
	}
}
