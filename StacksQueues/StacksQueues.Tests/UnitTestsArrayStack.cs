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
	public class UnitTestsArrayStack
	{
		[TestMethod]
		public void Push_Pop_One()
		{
			var ints = new ArrayStack<int> ();
			Assert.AreEqual (0, ints.Count);

			int element = 1;
			ints.Push (element);
			Assert.AreEqual (1, ints.Count);

			int poppedEl = ints.Pop ();
			Assert.AreEqual (poppedEl, element);

			Assert.AreEqual (0, ints.Count);
		}

		[TestMethod]
		public void Push_Pop_Many()
		{
			var strings = new ArrayStack<string> ();
			Assert.AreEqual (0, strings.Count);

			for( var i=0; i< 1000; i++ ) {
				strings.Push ("string" + i);
				Assert.AreEqual (i+1, strings.Count);
			}
			for( var i=0; i< 1000; i++ ) {
				strings.Pop ();
				Assert.AreEqual (999-i, strings.Count);
			}
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Pop_Empty_ShouldThrowException()
		{
			var ints = new ArrayStack<int> ();
			ints.Pop ();
		}

		[TestMethod]
		public void Push_Pop_InitialCapacity()
		{
			var ints = new ArrayStack<int> (1);
			Assert.AreEqual (0, ints.Count);

			int element1 = 1;
			ints.Push (element1);
			Assert.AreEqual (1, ints.Count);

			int element2 = 2;
			ints.Push (element2);
			Assert.AreEqual (2, ints.Count);

			{
				int poppedEl = ints.Pop ();
				Assert.AreEqual (poppedEl, element2);
				Assert.AreEqual (1, ints.Count);
			}

			{
				int poppedEl = ints.Pop ();
				Assert.AreEqual (poppedEl, element1);
				Assert.AreEqual (0, ints.Count);
			}


		}

		[TestMethod]
		public void Test_ToArray()
		{
			var ints = new ArrayStack<int> ();
			int[] arr = {3, 5, -2, 7};

			for( var i=0; i<arr.Length; i++) {
				ints.Push(arr[i]);
			}

			var stackArr = ints.ToArray();

			for( var i=0; i<arr.Length; i++) {
				Assert.AreEqual(stackArr[arr.Length-i-1], arr[i]);
			}

		}

		[TestMethod]
		public void Test_Empty_ToArray()
		{
			var dates = new ArrayStack<DateTime> ();

			var stackArr = dates.ToArray();

			Assert.AreEqual(0, stackArr.Length);

		}
	}
}

