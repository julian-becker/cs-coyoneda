using System;
using System.Collections.Generic;

namespace CoYoneda
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			TryOption ();
			TryCoyonedaOption ();
			TryCoyonedaList ();
		}

		// ----

		private static IFunctor<string> DoFmap(IFunctor<int> v)
		{
			return v.fmap (x => x * 2).fmap (x => "Value is '" + x + "'");
		}

		// ----

		private static void TryOption()
		{
			Console.WriteLine (" -- try Option --");
			var sample1 = new Some<int> (100);
			var sample2 = new None<int> ();

			var result1 = (Option<string>)DoFmap (sample1);
			var result2 = (Option<string>)DoFmap (sample2);

			Console.WriteLine (result1);
			Console.WriteLine (result2);
		}

		private static void TryCoyonedaOption()
		{
			Console.WriteLine (" -- try CoYoneda<Option> --");
			var sample1 
				= Coyoneda<int, Option<Object>>.liftCoyoneda (new Some<Object>(100));
			var sample2
				= Coyoneda<int, Option<Object>>.liftCoyoneda (new None<Object>());

			var result1 = (Coyoneda<string, Option<Object>>)DoFmap (sample1);
			var result2 = (Coyoneda<string, Option<Object>>)DoFmap (sample2);

			Console.WriteLine (MapCoyonedaOption<string>(result1));
			Console.WriteLine (MapCoyonedaOption<string>(result2));
		}

		private static void TryCoyonedaList()
		{
			Console.WriteLine (" -- try CoYoneda<List> --");
			var sample 
				= Coyoneda<int, List<Object>>.liftCoyoneda (new List<Object> { 100, 200, 300 });
			var result = (Coyoneda<string, List<Object>>)DoFmap (sample);

			foreach(string item in MapCoyonedaList<string>(result))
			{
				Console.WriteLine(item);
			}
		}

		// --- Convert Coyoneda ---

		private static Option<T> MapCoyonedaOption<T>(Coyoneda<T, Option<Object>> coyoneda)
		{
			return (Option<T>)coyoneda.InitialValue.fmap(coyoneda.Function);
		}

		private static List<T> MapCoyonedaList<T>(Coyoneda<T, List<Object>> coyoneda)
		{
			List<T> res = new List<T>();
			foreach (Object o in coyoneda.InitialValue) 
			{
				res.Add (coyoneda.Function (o));
			}
			return res;
		}
	}
}
