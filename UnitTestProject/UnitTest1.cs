using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lib;
using System.Linq;

namespace UnitTestProject
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void Empty_Constructor_Case()
		{
			int[] arr = { -1, -1, -1, -1, -1, -1, -1, -1, -1};
			TicTac game = new TicTac();
			Caretaker caretaker = new Caretaker(game);
			Assert.IsTrue(Enumerable.SequenceEqual(caretaker.GetArr(), arr));
		}

		[TestMethod]
		public void Array_Constructor_Case()
		{
			int[] arr = { 0, -1, -1, -1, -1, -1, -1, -1, 1};
			TicTac game = new TicTac(arr);
			Caretaker caretaker = new Caretaker(game);
			Assert.IsTrue(Enumerable.SequenceEqual(caretaker.GetArr(), arr));
		}

		[TestMethod]
		public void Plase_Correct()
		{
			int[] arr = { 0, -1, -1, -1, -1, -1, -1, -1, -1};
			TicTac game = new TicTac();
			Caretaker caretaker = new Caretaker(game);
			game.MakePlase(1);
			caretaker.Backup();
			Assert.IsTrue(Enumerable.SequenceEqual(caretaker.GetArr(), arr));
		}

		[TestMethod]
		public void Row_Winner()
		{
			int[] arr = { 0, 1, -1,  0,  1, -1,  0, -1, -1 };
			TicTac game = new TicTac(arr);
			Caretaker caretaker = new Caretaker(game);
			Assert.AreEqual(game.Check_For_Win(), 0);
		}

		[TestMethod]
		public void Col_Winner()
		{
			int[] arr = { 0, 0, 0, -1, -1, -1, -1, -1, -1 };
			TicTac game = new TicTac(arr);
			Caretaker caretaker = new Caretaker(game);
			Assert.AreEqual(game.Check_For_Win(), 0);
		}

		[TestMethod]
		public void Dieg_Winner()
		{
			int[] arr = { 0,  1, -1, -1,  0,  1, -1, -1, 0 };
			TicTac game = new TicTac(arr);
			Caretaker caretaker = new Caretaker(game);
			Assert.AreEqual(game.Check_For_Win(), 0);
		}

		[TestMethod]
		public void Undo_Empty_Array()
		{
			int[] arr = { -1, -1, -1, -1, -1, -1, -1, -1, -1 };
			TicTac game = new TicTac();
			Caretaker caretaker = new Caretaker(game);
			caretaker.Undo();
			caretaker.Undo();
			Assert.IsTrue(Enumerable.SequenceEqual(caretaker.GetArr(), arr));
		}

		[TestMethod]
		public void Undo_Array()
		{
			int[] arr = { -1, -1, -1, -1, -1, -1, -1, -1, -1 };
			TicTac game = new TicTac();
			Caretaker caretaker = new Caretaker(game);
			game.MakePlase(1);
			caretaker.Backup();
			caretaker.Undo();
			Assert.IsTrue(Enumerable.SequenceEqual(caretaker.GetArr(), arr));
		}

		[TestMethod]
		public void BackUp_Correct()
		{
			int[] arr = { 0, -1, -1, -1, -1, -1, -1, -1, -1 };
			TicTac game = new TicTac();
			Caretaker caretaker = new Caretaker(game);
			game.MakePlase(1);
			caretaker.Backup();
			Assert.IsTrue(Enumerable.SequenceEqual(caretaker.GetArr(), arr));
		}

		[TestMethod]
		public void Turn_Changed()
		{
			int[] arr = { 0, -1, -1, -1, -1, -1, -1, -1, -1 };
			TicTac game = new TicTac();
			Caretaker caretaker = new Caretaker(game);
			game.MakePlase(1);
			caretaker.Backup();
			Assert.AreEqual(caretaker.GetTurn(), 1);
		}


	}
}
