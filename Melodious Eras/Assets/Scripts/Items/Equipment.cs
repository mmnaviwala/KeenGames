using UnityEngine;
using System.Collections;

public class Equipment : Item 
{
	public CharacterStats wielder;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void Equip(CharacterStats wielder) { }
}
