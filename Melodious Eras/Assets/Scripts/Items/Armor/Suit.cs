using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Items/Armor/Suit")]
public class Suit : Equipment 
{
    public ArmorMod[] armorMods;
    public int armor = 50, maxArmor = 50;
	public float batteryLife = 100, maxBatteryLife = 100;
    bool isCloaked = false;

    public bool IsCloaked { get { return isCloaked; } }

	// Use this for initialization
	void Start () 
    {
		//TODO: add IEquippable interface, move "wielder" to it and make Weapon and Suit inherit from it.
		//Also move Weapon.Equip() method to it, and determine the wielder from there
		wielder = this.GetComponent<CharacterStats> ();
	}
	
	// Update is called once per frame
	void Update () 
    {
	}


    public void Cloak()
    {
        this.StopCoroutine("cloak");
        if (isCloaked)
            StartCoroutine(cloak(0f));
        else
            StartCoroutine(cloak(1f));
        isCloaked = !isCloaked;
    }
    IEnumerator cloak(float endAlpha)
    {
        YieldInstruction eof = new WaitForEndOfFrame();
        var _renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        Material mat = _renderer.materials[0]; //main material
        float startAlpha = Mathf.Abs(1f - endAlpha);

        foreach (var r in GetComponentsInChildren<Renderer>())
            Debug.Log(r.name);

        float startTime = Time.time;
        while (Mathf.Abs(endAlpha - mat.GetFloat("_AlphaTestRef")) > 0.01f)
        {
            mat.SetFloat("_AlphaTestRef", Mathf.Lerp(startAlpha, endAlpha, (Time.time - startTime) * 2));
            yield return eof;
        }

        mat.SetFloat("_AlphaTestRef", endAlpha);
        _renderer.materials[1].SetFloat("_AlphaTestRef", endAlpha);
    }
}
