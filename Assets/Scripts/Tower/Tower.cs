using UnityEngine;

public class Tower : MonoBehaviour
{
	public Sprite towerIconMaterial;
	[SerializeField] public bool onPath = false;
	[SerializeField] public bool offPath = false;
	public int cost = 50;

	public float towerSize = 1f;

	[SerializeField] protected LayerMask targetLayer;
	[SerializeField] protected float damage = 2f;

	public TargetMode targetMode;
	public enum TargetMode
	{
		Closest,
		Healthiest,
		First,
		Last
	}

	protected bool placed = false;
	public virtual bool Placed
	{
		get { return placed; }
		set
		{
			placed = value;
		}
	}

	protected virtual void Update() {}
}
