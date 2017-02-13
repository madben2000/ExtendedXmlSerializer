// MIT License
// 
// Copyright (c) 2016 Wojciech Nag�rski
//                    Michael DeMond
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;

namespace ExtendedXmlSerialization.Core.Sources
{
	public class DelegateAlterations<TParameter, TResult> :
		ReferenceCache<IAlteration<TResult>, IAlteration<Func<TParameter, TResult>>>
	{
		public static DelegateAlterations<TParameter, TResult> Default { get; } = new DelegateAlterations<TParameter, TResult>();
		DelegateAlterations() : base(x => new DelegateAlteration(x)) {}

		class DelegateAlteration : IAlteration<Func<TParameter, TResult>>
		{
			readonly IAlteration<TResult> _alteration;

			public DelegateAlteration(IAlteration<TResult> alteration)
			{
				_alteration = alteration;
			}

			public Func<TParameter, TResult> Get(Func<TParameter, TResult> parameter)
				=> new DelegateAlterationSource(parameter, _alteration).Get;
		}

		sealed class DelegateAlterationSource : DelegatedSource<TParameter, TResult>
		{
			readonly IAlteration<TResult> _alteration;

			public DelegateAlterationSource(Func<TParameter, TResult> source, IAlteration<TResult> alteration) : base(source)
			{
				_alteration = alteration;
			}

			public override TResult Get(TParameter parameter) => _alteration.Get(base.Get(parameter));
		}
	}
}