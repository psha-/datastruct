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
	public class UnitTestsLinkedQueue
	{

		[TestMethod]
		public void Push_Pop_One()
		{
			var ints = new LinkedQueue<int> ();
			Assert.AreEqual (0, ints.Count);

			int element = 1;
			ints.Enqueue (element);
			Assert.AreEqual (1, ints.Count);

			int poppedEl = ints.Dequeue ();
			Assert.AreEqual (poppedEl, element);

			Assert.AreEqual (0, ints.Count);
		}

		[TestMethod]
		public void Push_Pop_Many()
		{
			var strigs = new LinkedQueue<string> ();
			Assert.AreEqual (0, strigs.Count);

			for( var i=0; i< 1000; i++ ) {
				strigs.Enqueue ("string" + i);
				Assert.AreEqual (i+1, strigs.Count);
			}
			for( var i=0; i< 1000; i++ ) {
				strigs.Dequeue ();
				Assert.AreEqual (999-i, strigs.Count);
			}
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Pop_Empty_ShouldThrowException()
		{
			var ints = new LinkedQueue<int> ();
			ints.Dequeue ();
		}

		[TestMethod]
		public void Push_Pop_Two()
		{
			var ints = new LinkedQueue<int> ();
			Assert.AreEqual (0, ints.Count);

			int element1 = 1;
			ints.Enqueue (element1);
			Assert.AreEqual (1, ints.Count);

			int element2 = 2;
			ints.Enqueue (element2);
			Assert.AreEqual (2, ints.Count);

			{
				int poppedEl = ints.Dequeue ();
				Assert.AreEqual (poppedEl, element1);
				Assert.AreEqual (1, ints.Count);
			}

			{
				int poppedEl = ints.Dequeue ();
				Assert.AreEqual (poppedEl, element2);
				Assert.AreEqual (0, ints.Count);
			}


		}

		[TestMethod]
		public void Test_ToArray()
		{
			var ints = new LinkedQueue<int> ();
			int[] arr = {3, 5, -2, 7};

			for( var i=0; i<arr.Length; i++) {
				ints.Enqueue(arr[i]);
			}

			var intsArr = ints.ToArray();

			for( var i=0; i<arr.Length; i++) {
				CollectionAssert.AreEqual(arr, intsArr);
			}

		}

		[TestMethod]
		public void Test_Empty_ToArray()
		{
			var dates = new LinkedQueue<DateTime> ();

			var datesArr = dates.ToArray();

			Assert.AreEqual(0, datesArr.Length);

		}
	}
}

