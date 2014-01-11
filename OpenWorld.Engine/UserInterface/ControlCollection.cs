using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.UserInterface
{
	/// <summary>
	/// Represents a container for controls.
	/// </summary>
	public sealed class Container : ICollection<Control>
	{
		private readonly Control owner;
		private readonly List<Control> list = new List<Control>();

		internal Container(Control owner)
		{
			this.owner = owner;
		}

		/// <summary>
		/// Adds a control to the container.
		/// </summary>
		/// <param name="item">The control to be added</param>
		public void Add(Control item)
		{
			if (item == null)
				throw new ArgumentNullException("item");
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
			throw new NotImplementedException();
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
			if (item.parent != this.owner)
				return false; // Nothing to do here
			this.list.Remove(item);
			item.parent = null;
			return true;
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
			get { return false; }
		}

		/// <summary>
		/// Gets the parent control of the contained controls.
		/// </summary>
		public Control Owner
		{
			get { return owner; }
		}
	}
}
