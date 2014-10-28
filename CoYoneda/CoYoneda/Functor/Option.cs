using System;

namespace CoYoneda
{
	public abstract class Option<T> : IFunctor<T>
	{
		public IFunctor<U> fmap<U>(Func<T, U> f)
		{
			if (this is None<T>) {
				return new None<U> ();
			}
			Some<T> input = (Some<T>)this;
			return new Some<U> (f(input.Value));
		}

		public T getValueOr(T def)
		{
			if (this is None<T>) {
				return def;
			}
			return ((Some<T>)this).Value;
		}
	}

	public sealed class Some<T> : Option<T>
	{
		public T Value { get; private set; }

		public Some(T i){
			Value = i;
		}

		public override string ToString ()
		{
			return "Some(" + Value + ")";
		}
	}

	public sealed class None<T> : Option<T>
	{
		public override string ToString ()
		{
			return "None";
		}
	}
}

