using UnityEngine;
using System.Collections;

public class C4 : Weapon {
	public float detonationTime = 5f;
	private float timer;
	private PlayerMovementBasic player;
	YieldInstruction endOfFrame = new WaitForEndOfFrame();
	public GameObject prefab;

	public ExplosionPhysicsForce explosion;
	public float force = 4;
	public ParticleSystemMultiplier _multiplier;
	public float mult = 1;

	// Use this for initialization
	void Start () {
		timer = detonationTime;
		player = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<PlayerMovementBasic>();
		explosion.explosionForce = force;
		_multiplier.multiplier = mult;
	}

	public override bool Fire ()
	{
		//count down, then explode
		Vector3 playerChestHeight = player.transform.position + Vector3.up;
		Ray ray = new Ray(playerChestHeight, player.targetPoint - playerChestHeight);
		RaycastHit hit;
		if(Physics.Raycast (ray, out hit, 1.5f))
		{
			Debug.Log("Placed charge");
			GameObject c4_charge = Instantiate (prefab, hit.point, Quaternion.Euler(hit.normal - new Vector3(180, 0, 90))) as GameObject;
			c4_charge.GetComponent<C4>().Plant();

			return true;
		}
		else
		{
			return false;
		}
	}

	public void Plant()
	{
		StartCoroutine(PlantCharge ());
	}
	IEnumerator PlantCharge()
	{
		timer = detonationTime;
		int lastInt = (int)detonationTime;
		Debug.Log("Detonating " + this.name);
		ammoInClip--;

		while(timer > 0)
		{
			timer -= Time.deltaTime;
			if(lastInt - 1 > timer)
			{
				lastInt--;
				Debug.Log("Timer: " + lastInt);
			}
			yield return endOfFrame;
		}
		explosion.Detonate();
		_multiplier.Detonate();
		yield return new WaitForSeconds(3f);
		Destroy (this.gameObject);
	}
}
