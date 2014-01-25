using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Items/Armor/Suit")]
public class Suit : Item 
{
    public ArmorMod[] armorMods;
    public int armor = 50, maxArmor = 50;
	public float batteryLife = 100, maxBatteryLife = 100;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
