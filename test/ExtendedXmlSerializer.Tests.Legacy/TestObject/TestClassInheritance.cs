﻿// MIT License
// 
// Copyright (c) 2016 Wojciech Nagórski
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

using System.Xml.Serialization;

namespace ExtendedXmlSerializer.Tests.Legacy.TestObject
{
	public class TestClassInheritanceBase
	{
		public int Id { get; set; }

		public virtual void Init()
		{
			Id = 1;
		}
	}

	public class TestClassInheritance : TestClassInheritanceBase
	{
		public int Id2 { get; set; }

		public override void Init()
		{
			Id = 2;
			Id2 = 3;
		}
	}

	public class TestClassInheritanceWithOrderBase
	{
		[XmlElement(Order = 2)]
		public int Id { get; set; }

		public virtual void Init()
		{
			Id = 1;
		}
	}

	public class TestClassInheritanceWithOrder : TestClassInheritanceWithOrderBase
	{
		public static TestClassInheritanceWithOrder Create()
		{
			var result = new TestClassInheritanceWithOrder();
			result.Init();
			return result;
		}

		[XmlElement(Order = 1)]
		public int Id2 { get; set; }

		public override void Init()
		{
			Id = 2;
			Id2 = 3;
		}
	}

	public interface IInterfaceWithOrder
	{
		int Id { get; set; }
	}

	public class TestClassInterfaceInheritanceWithOrder : IInterfaceWithOrder
	{
		public int Id2 { get; set; }
		public int Id { get; set; }
	}
}