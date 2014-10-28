using System;

namespace CoYoneda
{
	public interface IFunctor<T>
	{
		IFunctor<U> fmap<U>(Func<T, U> f);
	}
}

