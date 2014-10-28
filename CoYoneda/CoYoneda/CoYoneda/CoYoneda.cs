using System;

namespace CoYoneda
{
	//Coyoneda<T, F<Object>>
	public class Coyoneda<T, U> : IFunctor<T>
	{
		public Func<Object, T> Function { get; private set; } 
		public U InitialValue { get; private set; } //InitialValue :: U :: F<Object>

		private Coyoneda (Func<Object, T> f, U u) 
		{
			Function = f;
			InitialValue = u;
		}

		public static Coyoneda<T, U> liftCoyoneda(U container){
			Func<Object, T> f = (x => (T)x);
			return new Coyoneda<T, U>(f, container);
		}

		public IFunctor<V> fmap<V>(Func<T, V> g)
		{
			return new Coyoneda<V, U> (x => g (Function (x)), InitialValue);
		}
	}
}

