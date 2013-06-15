using UnityEngine;

public class Rotation : MonoBehaviour
{
	Vector3 dir;
	void Awake()
	{
		dir = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
	}

	void Update()
	{
		transform.Rotate(dir, Time.deltaTime * 50);
	}
}