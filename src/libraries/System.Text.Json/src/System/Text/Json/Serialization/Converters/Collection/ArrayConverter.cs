// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections;
using System.Collections.Generic;

namespace System.Text.Json.Serialization.Converters
{
    /// <summary>
    /// Converter for <cref>System.Array</cref>.
    /// </summary>
    internal sealed class ArrayConverter<TCollection, TElement> : IEnumerableDefaultConverter<TElement[], TElement>
    {
        internal override bool CanHaveMetadata => false;

        protected override void Add(in TElement value, ref ReadStack state)
        {
            ((List<TElement>)state.Current.ReturnValue!).Add(value);
        }

        internal override bool SupportsCreateObjectDelegate => false;
        protected override void CreateCollection(ref Utf8JsonReader reader, ref ReadStack state, JsonSerializerOptions options)
        {
            state.Current.ReturnValue = new List<TElement>();
        }

        protected override void ConvertCollection(ref ReadStack state, JsonSerializerOptions options)
        {
            List<TElement> list = (List<TElement>)state.Current.ReturnValue!;
            state.Current.ReturnValue = list.ToArray();
        }

        protected override bool OnWriteResume(Utf8JsonWriter writer, TElement[] array, JsonSerializerOptions options, ref WriteStack state)
        {
            int index = state.Current.EnumeratorIndex;

            JsonConverter<TElement> elementConverter = GetElementConverter(ref state);
            if (elementConverter.CanUseDirectReadOrWrite && state.Current.NumberHandling == null)
            {
                // Fast path that avoids validation and extra indirection.
                for (; index < array.Length; index++)
                {
                    elementConverter.Write(writer, array[index], options);
                }
            }
            else
            {
                for (; index < array.Length; index++)
                {
                    TElement element = array[index];
                    if (!elementConverter.TryWrite(writer, element, options, ref state))
                    {
                        state.Current.EnumeratorIndex = index;
                        return false;
                    }

                    state.Current.EndCollectionElement();

                    if (ShouldFlush(writer, ref state))
                    {
                        state.Current.EnumeratorIndex = ++index;
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
