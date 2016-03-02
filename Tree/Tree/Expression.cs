using System;
using System.Collections.Generic;

namespace TreeStruct
{
	public class Expression
	{

		private class Node
		{
			public string Value { get; private set; }
			public List<Node> Children;

			public Node(string val)
			{
				Value = val;
			}

		}

		private string Input;

		private static bool TryExtractNumber( ref string tok, ref string number)
		{
			double num;
			for (var end=tok.Length; end > 0; end--) {
				if( double.TryParse (tok.Substring(0, end), out num)) {
					number = tok.Substring (0, end);
					tok = tok.Substring (end);
					return true;
				}
			}
			return false;
		}

		private static int Precedence(string op)
		{
			switch(op) {

			case "*":
			case "/":
				return 3;
			case "+":
			case "-":
				return 2;
			default:
				return -1;

			}
		}
		private static bool IsLParenthesis(string tok)
		{
			return "(" == tok;
		}

		private static bool IsRParenthesis(string tok)
		{
			return ")" == tok;
		}
		private static bool IsParenthesis(string tok)
		{
			return IsLParenthesis (tok) || IsRParenthesis (tok);
		}

		private static bool TryExtractOp(ref string tok, ref string op )
		{
			if( tok.Length > 0 && -1 != Precedence(tok.Substring (0, 1))) {
				op = tok.Substring (0, 1);
				tok = tok.Substring (1);
				return true;
			}
			return false;
		}

		private static bool TryExtractLParenthesis(ref string tok, ref string p )
		{
			if( tok.Length > 0 && IsLParenthesis(tok.Substring (0, 1))) {
				p = tok.Substring (0, 1);
				tok = tok.Substring (1);
				return true;
			}
			return false;
		}

		private static bool TryExtractRParenthesis(ref string tok, ref string p )
		{
			if( tok.Length > 0 && IsRParenthesis(tok.Substring (0, 1))) {
				p = tok.Substring (0, 1);
				tok = tok.Substring (1);
				return true;
			}
			return false;
		}

		public Expression (string input)
		{
			Input = input;
		}

		public double Calculate()
		{
			var output = ToPostfix ();

		}

		private void ToTree( Queue<string> postfix )
		{
			var children = new List<Node> ();
			Node node = new Node(postfix.Dequeue());

			while (postfix.Count > 0) {
				if (-1 == Precedence (postfix.Peek ())) {
					node = new Node (postfix.Dequeue ());
					node.Children = children;
					children = new List<Node> ();
				}
				node = new Node(postfix.Dequeue())
			}
		}

		private Queue<string> ToPostfix()
		{
			var output = new Queue<string>();
			var stack = new Stack<string> ();
			var remaining = Input;
			while (remaining.Length > 0) {
				string number = " ";
				if( TryExtractNumber(ref remaining, ref number)) {
					output.Enqueue (number);
				}
				string punct = " ";
				if (TryExtractOp (ref remaining, ref punct)) {
					while (stack.Count > 0 && -1 != Precedence(stack.Peek()) && Precedence(punct) <= Precedence(stack.Peek())) {
						output.Enqueue (stack.Pop ());
					}
					stack.Push (punct);
				}

				if( TryExtractLParenthesis(ref remaining, ref punct) ) {
					stack.Push (punct);
				}
				if( TryExtractRParenthesis(ref remaining, ref punct) ) {
					while (!IsLParenthesis( stack.Peek())) {
						output.Enqueue (stack.Pop ());
					}
					if (0 == stack.Count) {
						throw new InvalidOperationException ("Parenthesis mismaatch");
					}
					stack.Pop ();
				}
			}
			while (stack.Count > 0) {
				string punct = " ";
				if (IsParenthesis( stack.Peek ())) {
					throw new InvalidOperationException ("Parenthesis mismaatch");
				}
				output.Enqueue (stack.Pop ());
			}
			return output;
		}

	}
}

