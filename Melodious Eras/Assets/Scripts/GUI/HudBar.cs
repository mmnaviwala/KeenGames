using UnityEngine;
using System.Collections;


public enum Position2 { Position1, Position2, Position3, Position4 }

[System.Serializable]
public class HudBar
{
    [SerializeField]
    protected Material bar;
    public Texture bottomTexture, topTexture, barTexture;
    public Position2 position;


    public GUIStyle bigFont;
    protected GUIStyle smallFont;
    protected GUITexture guiTexture;

    protected PlayerStats player;
    protected Rect rectSize, numberLabelSize, stringLabelSize;
    protected float maxNumber = 100;
    protected float currentNumber = 100;
    protected string textToDisplay;

    public virtual void Initialize(PlayerStats playerP)
    {
        this.player = playerP;

        float xx = Screen.width / 10;
        float yy = Screen.height / 10;

        switch (position)
        {
            case Position2.Position1:
                rectSize = new Rect(xx * .3f, yy * 8.1f, xx + 10, xx + 10);
                numberLabelSize = new Rect(xx * .3f, yy * 7.8f, xx + 10, xx + 10);
                stringLabelSize = new Rect(xx * .3f, yy * 8.4f, xx + 10, xx + 10);
                break;
            case Position2.Position2:
                rectSize = new Rect(xx * 1.55f, yy * 8.1f, xx + 10, xx + 10);
                numberLabelSize = new Rect(xx * 1.55f, yy * 7.8f, xx + 10, xx + 10);
                stringLabelSize = new Rect(xx * 1.55f, yy * 8.4f, xx + 10, xx + 10);
                break;
            case Position2.Position3:
                rectSize = new Rect(xx * 7.55f, yy * 8.1f, xx + 10, xx + 10);
                numberLabelSize = new Rect(xx * 7.55f, yy * 7.8f, xx + 10, xx + 10);
                stringLabelSize = new Rect(xx * 7.55f, yy * 8.4f, xx + 10, xx + 10);
                break;
            case Position2.Position4:
                rectSize = new Rect(xx * 8.80f, yy * 8.1f, xx + 10, xx + 10);
                numberLabelSize = new Rect(xx * 8.80f, yy * 7.8f, xx + 10, xx + 10);
                stringLabelSize = new Rect(xx * 8.80f, yy * 8.4f, xx + 10, xx + 10);
                break;
        }

        bigFont.fontSize = System.Convert.ToInt32(Screen.height * 0.05f);
        smallFont = new GUIStyle();
        smallFont.fontSize = System.Convert.ToInt32(Screen.height * 0.03f);
        smallFont.alignment = TextAnchor.MiddleCenter;
        smallFont.normal.textColor = bigFont.normal.textColor;
        smallFont.font = bigFont.font;
    }

    public virtual void Display()
    {

        GUI.DrawTexture(rectSize, bottomTexture);
        bar.SetFloat("_Cutoff", Mathf.Clamp(Mathf.InverseLerp(0f, maxNumber, maxNumber - currentNumber), .01f, 1));
        //renderer.material.color = Color.Lerp(Color.clear, Color.white, currentNumber / maxNumber);
        Graphics.DrawTexture(rectSize, barTexture, bar);

        GUI.DrawTexture(rectSize, topTexture);


        GUI.Label(stringLabelSize, textToDisplay, smallFont);
        GUI.Label(numberLabelSize, string.Format("{0:f1}", currentNumber), bigFont);
    }
}

[System.Serializable]
public class HealthBar : HudBar
{
    public override void Initialize(PlayerStats playerP)
    {
        base.Initialize(playerP);
        textToDisplay = "Health";
    }
    public override void Display()
    {
        this.maxNumber = player.maxHealth;
        this.currentNumber = player.health;

        base.Display();
    }
}


[System.Serializable]
public class ArmorBar : HudBar
{
    public override void Initialize(PlayerStats playerP)
    {
        base.Initialize(playerP);
        textToDisplay = "Armor";
    }
    public override void Display()
    {
        this.maxNumber = player.suit.maxArmor;
        this.currentNumber = player.suit.armor;

        base.Display();
    }
}

[System.Serializable]
public class AmmoBar : HudBar
{
    public override void Display()
    {
        this.maxNumber = player.equippedWeapon.maxAmmo;
        this.currentNumber = player.equippedWeapon.ammoInClip + player.equippedWeapon.extraAmmo;
        textToDisplay = player.equippedWeapon.weaponName; //needs to be in each update, for when player switches weapons

        base.Display();
    }
}
[System.Serializable]
public class BatteryBar : HudBar
{
    public override void Initialize(PlayerStats playerP)
    {
        base.Initialize(playerP);
        textToDisplay = "Battery";
    }
    public override void Display()
    {
        this.maxNumber = player.suit.maxBatteryLife;
        this.currentNumber = player.suit.batteryLife;

        base.Display();
    }
}