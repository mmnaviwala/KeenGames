using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Items/Armor/Suit")]
public class Suit : Item 
{
	public CharacterStats wielder;
    public ArmorMod[] armorMods;
    public int armor = 50, maxArmor = 50;
	public float batteryLife = 100, maxBatteryLife = 100;
	// Use this for initialization
	void Start () 
    {
		//TODO: add IEquippable interface, move "wielder" to it and make Weapon and Suit inherit from it.
		//Also move Weapon.Equip() method to it, and determine the wielder from there
		wielder = this.GetComponent<CharacterStats> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
