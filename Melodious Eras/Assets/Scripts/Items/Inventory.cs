using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory
{
	private List<Item> items;
	private bool unlimitedCapacity = false;

	/// <summary>
	/// Inventory with unlimited capacity
	/// </summary>
	public Inventory()
	{
		items = new List<Item>();
		unlimitedCapacity = true;
	}

	/// <summary>
	/// Inventory with set capacity
	/// </summary>
	/// <param name="capacityP">Capacity p.</param>
	public Inventory(int capacityP)
	{
		items = new List<Item>(capacityP);
	}

	/// <summary>
	/// Returns true if the inventory has capacity
	/// </summary>
	/// <returns><c>true</c>, if item was added, <c>false</c> otherwise.</returns>
	/// <param name="item">Item.</param>
	public bool AddItem(Item item)
	{
		if(unlimitedCapacity || items.Count < items.Capacity)
		{
			items.Add(item);
			return true;
		}
		else
			return false;
	}


	public bool DropItem(int index)
	{
		if(items.Count > index)
		{
			items.RemoveAt(index);
			return true;
		}
		else
			return false;
	}
}
