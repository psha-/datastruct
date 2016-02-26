using System;

#if MSTEST
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Category = Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute;
#else
using NUnit.Framework;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestContext = System.Object;
using TestProperty = NUnit.Framework.PropertyAttribute;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using ExpectedException = NUnit.Framework.AssertionException;
#endif

namespace StacksQueues.Tests
{
	using System.Collections;
	using System.Collections.Generic;

	[TestClass]
	public class UnitTestsLinkedStack
	{

		[TestMethod]
		public void Push_Pop_One()
		{
			var intStack = new LinkedStack<int> ();
			Assert.AreEqual (0, intStack.Count);

			int element = 1;
			intStack.Push (element);
			Assert.AreEqual (1, intStack.Count);

			int poppedEl = intStack.Pop ();
			Assert.AreEqual (poppedEl, element);

			Assert.AreEqual (0, intStack.Count);
		}

		[TestMethod]
		public void Push_Pop_Many()
		{
			var stringStack = new LinkedStack<string> ();
			Assert.AreEqual (0, stringStack.Count);

			for( var i=0; i< 1000; i++ ) {
				stringStack.Push ("string" + i);
				Assert.AreEqual (i+1, stringStack.Count);
			}
			for( var i=0; i< 1000; i++ ) {
				stringStack.Pop ();
				Assert.AreEqual (999-i, stringStack.Count);
			}
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Pop_Empty_ShouldThrowException()
		{
			var intStack = new LinkedStack<int> ();
			intStack.Pop ();
		}

		[TestMethod]
		public void Push_Pop_Two()
		{
			var intStack = new LinkedStack<int> ();
			Assert.AreEqual (0, intStack.Count);

			int element1 = 1;
			intStack.Push (element1);
			Assert.AreEqual (1, intStack.Count);

			int element2 = 2;
			intStack.Push (element2);
			Assert.AreEqual (2, intStack.Count);

			{
				int poppedEl = intStack.Pop ();
				Assert.AreEqual (poppedEl, element2);
				Assert.AreEqual (1, intStack.Count);
			}

			{
				int poppedEl = intStack.Pop ();
				Assert.AreEqual (poppedEl, element1);
				Assert.AreEqual (0, intStack.Count);
			}


		}

		[TestMethod]
		public void Test_ToArray()
		{
			var intStack = new LinkedStack<int> ();
			int[] arr = {3, 5, -2, 7};

			for( var i=0; i<arr.Length; i++) {
				intStack.Push(arr[i]);
			}

			var stackArr = intStack.ToArray();

			for( var i=0; i<arr.Length; i++) {
				Assert.AreEqual(stackArr[arr.Length-i-1], arr[i]);
			}

		}

		[TestMethod]
		public void Test_Empty_ToArray()
		{
			var dateStack = new LinkedStack<DateTime> ();

			var stackArr = dateStack.ToArray();

			Assert.AreEqual(0, stackArr.Length);

		}
	}
}

