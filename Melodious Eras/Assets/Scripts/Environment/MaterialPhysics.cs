using UnityEngine;
using System.Collections;

public enum MaterialType { Grass, Carpet, Wood, Concrete, Stone, Marble, LightMetal, HeavyMetal, Glass, BrokenGlass, AbsorbAllSound, AbsorbNoSound, Custom };

[AddComponentMenu("Scripts/Environment/Material Physics")]
public class MaterialPhysics : MonoBehaviour 
{
    public MaterialType surfaceMaterial, coreMaterial;
	public bool canClimb = true;


    //1 = normal refraction/amplification, 0 means it absorbs all sound
    private float soundRefraction = 1,    //sound going THROUGH object (raycasting) per unit of thickness
                  soundAmplification = 1; //sound amplification

    [SerializeField]
	private float visibilityMultiplier = 1; //affects how well the object obscures the player from the enemy
    [SerializeField]
    float thickness; //in decimeters
    [SerializeField]
    bool overrideDefaultThickness = false;

    public float Thickness { get { return thickness; } }
    PhysicMaterial mat;

	// Use this for initialization
	void Awake ()
    {
        Vector3 dimensions = this.GetComponent<Collider>().bounds.size;
        if (!overrideDefaultThickness)
            thickness = Mathf.Min(dimensions.x, dimensions.y, dimensions.z) * 10f; //assuming walls, shelves, etc.

        CalculateSoundProperties();
	}


    void CalculateSoundProperties()
    {
        switch (surfaceMaterial)
        {
            case MaterialType.Grass:        soundAmplification = .3f;   break;
            case MaterialType.Carpet:       soundAmplification = .3f;   break;
            case MaterialType.Wood:         
                soundAmplification = 1f;
                this.GetComponent<Collider>().material = new PhysicMaterial("Wood");
                break;
            case MaterialType.Concrete:     soundAmplification = .9f;   break;
            case MaterialType.Stone:        soundAmplification = 1f;    break;
            case MaterialType.Marble:       soundAmplification = 1.05f; break;
            case MaterialType.LightMetal:   soundAmplification = 1.5f;
                                            this.GetComponent<Collider>().material = new PhysicMaterial("Metal"); 
                                            break;

            case MaterialType.HeavyMetal: soundAmplification = 1.15f;
                                          this.GetComponent<Collider>().material = new PhysicMaterial("Metal");
                                          break;
            case MaterialType.Glass:          soundAmplification = 1f;  break;
            case MaterialType.BrokenGlass:    soundAmplification = 2f;  break;
            case MaterialType.AbsorbAllSound: soundAmplification = 0;   break;
            case MaterialType.AbsorbNoSound:  soundRefraction = 1;      break;
        }
        if( coreMaterial == MaterialType.AbsorbAllSound || thickness > 20)
        {
            soundRefraction = 0f;
            return;
        }
        else
        {
            switch (coreMaterial)
            {
                case MaterialType.Grass:        
                    soundRefraction = Mathf.Pow( .5f, thickness);
                    break;
                case MaterialType.Wood:         
                    soundRefraction = Mathf.Pow(.5f, thickness);
                    break;
                case MaterialType.Concrete:     
                    soundRefraction = Mathf.Pow(.2f, thickness);
                    break;
                case MaterialType.Stone:        
                    soundRefraction = Mathf.Pow(.2f, thickness);  
                    break;
                case MaterialType.Marble:       
                    soundRefraction = Mathf.Pow(.2f, thickness);  
                    break;
                case MaterialType.LightMetal:   
                    soundRefraction = Mathf.Pow(.5f, thickness);  
                    break;
                case MaterialType.HeavyMetal:   
                    soundRefraction = Mathf.Pow(.2f, thickness);  
                    break;
                case MaterialType.Glass:        
                    soundRefraction = Mathf.Pow(.8f, thickness*10f);  
                    break;
                default: 
                    soundRefraction = 1f; 
                    break;
            }
        }
    }
}
