using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines a directed graph.
	/// </summary>
	/// <typeparam name="TNode">Type of the derived class.</typeparam>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Di")]
	public abstract class DiGraph<TNode>
		where TNode : DiGraph<TNode>
	{
		private readonly DiGraph<TNode>.Collection children;
		private TNode parent;

		/// <summary>
		/// Instantiates a new DiGraph.
		/// </summary>
		protected DiGraph()
		{
			this.children = new Collection((TNode)this);
		}

		/// <summary>
		/// Gets or sets the parent of this node.
		/// </summary>
		public TNode Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				if (this.parent == value)
					return; // Nothing to do here

				// this.parent gets set in Collection.Remove and Collection.Add
				if (this.parent != null)
					this.parent.Children.Remove((TNode)this);
				if (value != null)
					value.Children.Add((TNode)this);
			}
		}

		/// <summary>
		/// Gets the children of this collection.
		/// </summary>
		public DiGraph<TNode>.Collection Children { get { return this.children; } }

		/// <summary>
		/// Defines a collection of graph nodes.
		/// </summary>
		public sealed class Collection : IList<TNode>
		{
			private readonly TNode owner;
			private readonly List<TNode> list = new List<TNode>();
			private bool isLocked = false;

			internal Collection(TNode owner)
			{
				this.owner = owner;
			}

			/// <summary>
			/// Locks the collection. Nodes can't be removed or added.
			/// </summary>
			public void Lock()
			{
				this.isLocked = true;
			}

			/// <summary>
			/// Adds a node to the collection.
			/// </summary>
			/// <param name="item">The node to be added</param>
			public void Add(TNode item)
			{
				if (item == null)
					throw new ArgumentNullException("item");
				if (this.IsReadOnly)
					throw new InvalidOperationException("Cannot add a node to a locked collection");
				if (item.Parent == this.owner)
					return; // Nothing to do here

				if (item.parent != null)
					item.parent.Children.Remove(item);
				item.parent = this.owner;
				this.list.Add(item);
			}

			/// <summary>
			/// Removes all nodes from the collection.
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
			/// Checks if a node is in the collection.
			/// </summary>
			/// <param name="item">The item to be checked for.</param>
			/// <returns>True if the item is in the collection.</returns>
			public bool Contains(TNode item)
			{
				return this.list.Contains(item);
			}

			/// <summary>
			/// 
			/// </summary>
			/// <param name="array"></param>
			/// <param name="arrayIndex"></param>
			public void CopyTo(TNode[] array, int arrayIndex)
			{
				if (array == null)
					throw new ArgumentNullException("array");
				for (int i = 0; i < array.Length; i++)
					array[arrayIndex + i] = this.list[i];
			}

			/// <summary>
			/// Removes a node from the collection.
			/// </summary>
			/// <param name="item">The item to be removed.</param>
			/// <returns>True if the item was removed.</returns>
			public bool Remove(TNode item)
			{
				if (item == null)
					throw new ArgumentNullException("item");
				if (this.IsReadOnly)
					throw new InvalidOperationException("Cannot remove a node from a locked collection");
				if (item.parent != this.owner)
					return false; // Nothing to do here
				this.list.Remove(item);
				item.parent = null;
				return true;
			}

			/// <summary>
			/// Gets the index of a node in the collection.
			/// </summary>
			/// <param name="item">Node which index should be found.</param>
			/// <returns>Index of the node or -1 if not found.</returns>
			public int IndexOf(TNode item)
			{
				return this.list.IndexOf(item);
			}

			/// <summary>
			/// Inserts a node at an index.
			/// </summary>
			/// <param name="index"></param>
			/// <param name="item"></param>
			public void Insert(int index, TNode item)
			{
				if (item == null)
					throw new ArgumentNullException("item");
				if (this.IsReadOnly)
					throw new InvalidOperationException("Cannot add a node to a locked collection");
				if (item.Parent == this.owner)
					throw new ArgumentException("item is already in the collection.", "item");

				if (item.parent != null)
					item.parent.Children.Remove(item);
				item.parent = this.owner;
				this.list.Insert(index, item);
			}

			/// <summary>
			/// Removes a node at the index.
			/// </summary>
			/// <param name="index">Index of the item.</param>
			public void RemoveAt(int index)
			{
				if (this.IsReadOnly)
					throw new InvalidOperationException("Cannot remove a node from a locked collection");
				this.Remove(this.list[index]);
			}

			/// <summary>
			/// Gets an enumerator over the nodes in the collection.
			/// </summary>
			/// <returns>Enumerator</returns>
			public IEnumerator<TNode> GetEnumerator()
			{
				return list.GetEnumerator();
			}

			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			/// <summary>
			/// Gets the count of nodess in the collection.
			/// </summary>
			public int Count
			{
				get { return this.list.Count; }
			}

			/// <summary>
			/// Gets a value that indicates that this collection is read only.
			/// </summary>
			public bool IsReadOnly
			{
				get { return this.isLocked; }
			}

			/// <summary>
			/// Gets the parent node of the contained nodes.
			/// </summary>
			public TNode Owner
			{
				get { return owner; }
			}

			/// <summary>
			/// Gets or sets the node at index.
			/// </summary>
			/// <param name="index">Index in the collection.</param>
			/// <returns>Element at index.</returns>
			public TNode this[int index]
			{
				get { return this.list[index]; }
				set
				{

					if (value.Parent == this.owner)
						return; // Nothing to do here

					if (this.list[index] != null)
						this.list[index].parent = null;

					if (value.parent != null)
						value.parent.Children.Remove(value);
					value.parent = this.owner;
					this.list[index] = value;
				}
			}
		}
	}
}
