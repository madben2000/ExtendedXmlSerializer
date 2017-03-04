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

using ExtendedXmlSerialization.Core.Sources;

namespace ExtendedXmlSerialization.ContentModel.Members
{
	class MemberProfiles : CacheBase<MemberDescriptor, MemberProfile>
	{
		readonly IMemberContent _content;
		readonly IMemberEmitSpecifications _emit;
		readonly IMemberSerializers _serializers;
		readonly IAliases _aliases;
		readonly IMemberOrder _order;

		public MemberProfiles(IMemberEmitSpecifications emit,
		                      IMemberContent content,
		                      IMemberSerializers serializers,
		                      IAliases aliases,
		                      IMemberOrder order)
		{
			_content = content;
			_emit = emit;
			_serializers = serializers;
			_aliases = aliases;
			_order = order;
		}

		protected sealed override MemberProfile Create(MemberDescriptor parameter)
		{
			var metadata = parameter.Metadata;
			var order = _order.Get(metadata);
			var name = _aliases.Get(metadata) ?? metadata.Name;

			var content = _content.Get(parameter);

			var serializer = _serializers.Create(name, parameter, content);

			var result = new MemberProfile(_emit.Get(parameter), name, parameter.Writable, order, metadata,
			                               parameter.MemberType, content, serializer, serializer);
			return result;
		}
	}
}