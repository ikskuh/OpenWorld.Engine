using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.UserInterface
{
	/// <summary>
	/// Represents a container for controls.
	/// </summary>
	public sealed class Container : IList<Control>
	{
		private bool isLocked = false;
		private readonly Control owner;
		private readonly List<Control> list = new List<Control>();

		internal Container(Control owner)
		{
			this.owner = owner;
		}

		/// <summary>
		/// Locks the collection. Controls can't be removed or added.
		/// </summary>
		public void Lock()
		{
			this.isLocked = true;
		}

		/// <summary>
		/// Adds a control to the container.
		/// </summary>
		/// <param name="item">The control to be added</param>
		public void Add(Control item)
		{
			if (item == null)
				throw new ArgumentNullException("item");
			if (this.IsReadOnly)
				throw new InvalidOperationException("Cannot add a control to a locked collection");
			if (item.Parent == this.owner)
				return; // Nothing to do here

			if (item.parent != null)
				item.parent.Controls.Remove(item);
			item.parent = this.owner;
			this.list.Add(item);
		}

		/// <summary>
		/// Removes all controls from the container.
		/// </summary>
		public void Clear()
		{
			if (this.IsReadOnly)
				throw new InvalidOperationException("Cannot clear a locked collection");
			foreach (var child in this.list)
				child.parent = null; // Manual removal of parent
			this.list.Clear();
		}

		/// <summary>
		/// Checks if a control is in the container.
		/// </summary>
		/// <param name="item">The item to be checked for.</param>
		/// <returns>True if the item is in the container.</returns>
		public bool Contains(Control item)
		{
			return this.list.Contains(item);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="array"></param>
		/// <param name="arrayIndex"></param>
		public void CopyTo(Control[] array, int arrayIndex)
		{
			for (int i = 0; i < array.Length; i++)
				array[arrayIndex + i] = this.list[i];
		}

		/// <summary>
		/// Removes a control from the container.
		/// </summary>
		/// <param name="item">The item to be removed.</param>
		/// <returns>True if the item was removed.</returns>
		public bool Remove(Control item)
		{
			if (item == null)
				throw new ArgumentNullException("item");
			if (this.IsReadOnly)
				throw new InvalidOperationException("Cannot remove a control from a locked collection");
			if (item.parent != this.owner)
				return false; // Nothing to do here
			this.list.Remove(item);
			item.parent = null;
			return true;
		}

		/// <summary>
		/// Gets the index of a control in the collection.
		/// </summary>
		/// <param name="item">Control which index should be found.</param>
		/// <returns>Index of the control or -1 if not found.</returns>
		public int IndexOf(Control item)
		{
			return this.list.IndexOf(item);
		}

		/// <summary>
		/// Inserts a control at an index.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="item"></param>
		public void Insert(int index, Control item)
		{
			if (item == null)
				throw new ArgumentNullException("item");
			if (this.IsReadOnly)
				throw new InvalidOperationException("Cannot add a control to a locked collection");
			if (item.Parent == this.owner)
				throw new ArgumentException("item is already in the collection.", "item");

			if (item.parent != null)
				item.parent.Controls.Remove(item);
			item.parent = this.owner;
			this.list.Insert(index, item);
		}

		/// <summary>
		/// Removes an item at the index.
		/// </summary>
		/// <param name="index">Index of the item.</param>
		public void RemoveAt(int index)
		{
			if (this.IsReadOnly)
				throw new InvalidOperationException("Cannot remove a control from a locked collection");
			this.Remove(this.list[index]);
		}

		/// <summary>
		/// Gets an enumerator over the controls in the container.
		/// </summary>
		/// <returns>Enumerator</returns>
		public IEnumerator<Control> GetEnumerator()
		{
			return list.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>
		/// Gets the count of controls in the container.
		/// </summary>
		public int Count
		{
			get { return this.list.Count; }
		}

		/// <summary>
		/// Always false.
		/// </summary>
		public bool IsReadOnly
		{
			get { return this.isLocked; }
		}

		/// <summary>
		/// Gets the parent control of the contained controls.
		/// </summary>
		public Control Owner
		{
			get { return owner; }
		}

		/// <summary>
		/// Gets or sets the element at index.
		/// </summary>
		/// <param name="index">Index in the collection.</param>
		/// <returns>Element at index.</returns>
		public Control this[int index]
		{
			get { return this.list[index]; }
			set {

				if (value.Parent == this.owner)
					return; // Nothing to do here

				if(this.list[index] != null)
					this.list[index].parent = null;

				if (value.parent != null)
					value.parent.Controls.Remove(value);
				value.parent = this.owner;
				this.list[index] = value;
			}
		}
	}
}
