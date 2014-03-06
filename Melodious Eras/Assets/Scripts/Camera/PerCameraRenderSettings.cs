using UnityEngine;
using System.Collections;

/// <summary>
/// Currently being used for security cameras, so they can have nightvision
/// without affecting the rest of the ambient lighting
/// </summary>
[AddComponentMenu ("Rendering/Per Camera Render Settings")]
[RequireComponent(typeof(Camera))]
public class PerCameraRenderSettings : MonoBehaviour {	
	// Public variables -- set these in the inspector
	public bool fog;// = RenderSettings.fog;
	public Color fogColor;// = RenderSettings.fogColor;
	public float fogDensity;// = RenderSettings.fogDensity;
	public Color ambientLight;// = RenderSettings.ambientLight;
	public float haloStrength;// = RenderSettings.haloStrength;
	public float flareStrength;// = RenderSettings.flareStrength;
	
	// Private variables -- used to reset the render settings after the current camera has been rendered
	private static bool _global_fog;// = RenderSettings.fog;
	private static Color _global_fogColor;// = RenderSettings.fogColor;
	private static float _global_fogDensity;// = RenderSettings.fogDensity;
	private static Color _global_ambientLight;// = RenderSettings.ambientLight;
	private static float _global_haloStrength;// = RenderSettings.haloStrength;
	private static float _global_flareStrength;// = RenderSettings.flareStrength;
	
	private bool dirty = false; // Used to flag that the render settings have been overridden and need a restore

	void Awake()
	{
		_global_fog = fog = RenderSettings.fog;
		_global_fogColor = fogColor = RenderSettings.fogColor;
		_global_fogDensity = fogDensity = RenderSettings.fogDensity;
		_global_ambientLight = ambientLight = RenderSettings.ambientLight;
		_global_haloStrength = haloStrength = RenderSettings.haloStrength;
		_global_flareStrength = flareStrength = RenderSettings.flareStrength;
	}
	
	void OnPreRender () {
		if (! enabled ) return; // If the component is disabled, use the global render settings
		
		// Save global render state:
		_global_fog = RenderSettings.fog;
		_global_fogColor = RenderSettings.fogColor;
		_global_fogDensity = RenderSettings.fogDensity;
		_global_ambientLight = RenderSettings.ambientLight;
		_global_haloStrength = RenderSettings.haloStrength;
		_global_flareStrength = RenderSettings.flareStrength;
		
		
		// Set local settings:
		RenderSettings.fog = fog;
		RenderSettings.fogColor = fogColor;
		RenderSettings.fogDensity = fogDensity;
		RenderSettings.ambientLight = ambientLight;
		RenderSettings.haloStrength = haloStrength;
		RenderSettings.flareStrength = flareStrength;
		
		dirty=true;
	}
	
	void OnPostRender () {
		if (! dirty ) return; // If the component was disabled in OnPreRender, then don't restore
		
		// Restore global settings:
		RenderSettings.fog = _global_fog;
		RenderSettings.fogColor = _global_fogColor;
		RenderSettings.fogDensity = _global_fogDensity;
		RenderSettings.ambientLight = _global_ambientLight;
		RenderSettings.haloStrength = _global_haloStrength;
		RenderSettings.flareStrength = _global_flareStrength;
		
		dirty=false;
	}
	
	// Reset the component to revert to the current global settings
	void Reset () {
		fog = RenderSettings.fog;
		fogColor = RenderSettings.fogColor;
		fogDensity = RenderSettings.fogDensity;
		ambientLight = RenderSettings.ambientLight;
		haloStrength = RenderSettings.haloStrength;
		flareStrength = RenderSettings.flareStrength;
	}
}
