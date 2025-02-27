﻿using Havit.Collections;

namespace Havit.Blazor.Components.Web.Bootstrap;

/// <summary>
/// Sorting extension methods.
/// </summary>
public static class SortingItemExtensions
{
	/// <summary>
	/// Creates GenericPropertyComparer for the sorting by <see cref="SortingItem{TItem}.SortString"/> and <see cref="SortingItem{TItem}.SortDirection"/> properties.
	/// </summary>
	public static GenericPropertyComparer<TItem> ToGenericPropertyComparer<TItem>(this IEnumerable<SortingItem<TItem>> source)
	{
		Contract.Requires<ArgumentNullException>(source != null, nameof(source));

		return new GenericPropertyComparer<TItem>(source.ToSortItems());
	}

	/// <summary>
	/// Converts <see cref="SortingItem{TItem}"/> to <see cref="SortItem"/>.
	/// </summary>
	private static SortItem[] ToSortItems<TItem>(this IEnumerable<SortingItem<TItem>> source)
	{
		Contract.Requires<ArgumentNullException>(source != null, nameof(source));

		if (source.Any(item => item.SortKeySelector != null))
		{
			throw new InvalidOperationException($"Cannot convert sorting item while it contains a {nameof(SortingItem<TItem>.SortKeySelector)}.");
		}

		return source.Select(item => new SortItem(item.SortString, item.SortDirection)).ToArray();
	}
}
