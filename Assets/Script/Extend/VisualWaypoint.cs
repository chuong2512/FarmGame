using UnityEngine;

public class VisualWaypoint : MonoBehaviour
{
	[SerializeField] Color color;
	public Transform[] node;
	void OnDrawGizmos ()
	{
		node = new Transform[transform.childCount];
		for (int i = 0; i < node.Length; i++) node [i] = transform.GetChild (i).GetComponent<Transform> ();
		Gizmos.color = color;
		for (int i = 0; i < node.Length - 1; i++)
		{
			Vector3 startPosition = node [i].position;
			Vector3 endPosition = node [i + 1].position;
			Gizmos.DrawLine (startPosition, endPosition);
		}
	}
}