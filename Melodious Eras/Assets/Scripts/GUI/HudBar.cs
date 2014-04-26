using UnityEngine;
using System.Collections;


public enum Position2 { Position1, Position2, Position3, Position4 }

[System.Serializable]
public class HudBar
{
    protected PlayerStats player;
    public Texture bottomTexture, topTexture, barTexture;
    public Position2 position;
    public GUIStyle bigFont;
    protected GUIStyle smallFont;

    protected Rect rectSize, numberLabelSize, stringLabelSize;
    protected float maxNumber = 100;
    protected float currentNumber = 100;
    protected string textToDisplay;

    public virtual void Initialize()
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<PlayerStats>(); 

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
        
    }
}

[System.Serializable]
public class HealthBar : HudBar
{
    public override void Initialize()
    {
        base.Initialize();
        textToDisplay = "Health";
    }
    public override void Display()
    {
        
    }
}


[System.Serializable]
public class ArmorBar : HudBar
{
    public override void Initialize()
    {
        base.Initialize();
        textToDisplay = "Armor";
    }
    public override void Display()
    {
        base.Display();
    }
    
}

[System.Serializable]
public class AmmoBar : HudBar
{
    public override void Display()
    {
        textToDisplay = player.equippedWeapon.weaponName; //needs to be in each update, for when player switches weapons
        base.Display();
    }

}
[System.Serializable]
public class BatteryBar : HudBar
{
    public override void Initialize()
    {
        base.Initialize();
        textToDisplay = "Battery";
    }
    public override void Display()
    {
        base.Display();
    }
    
}