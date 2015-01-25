using UnityEngine;
using System.Collections;

public class ParalaxBackground : MonoBehaviour
{
	public int materialIndex = 0;
	public Vector2 uvAnimationRate = new Vector2( 1.0f, 0.0f );
	
	Vector2 uvOffset = Vector2.zero;
	
	void LateUpdate() 
	{
		float x = uvAnimationRate.x + Random.Range (-0.002f, 0.002f);
		x = Mathf.Min (0.04f, Mathf.Abs(x)) * Mathf.Sign(x);

		float y = uvAnimationRate.y + Random.Range (-0.003f, 0.003f);
		y = Mathf.Min (0.14f, Mathf.Abs(y)) * Mathf.Sign(y);
		y = Mathf.Max (0.07f, Mathf.Abs(y)) * Mathf.Sign(y);

		uvAnimationRate = new Vector2 (x, y);

		uvOffset += ( uvAnimationRate * Time.deltaTime );
		uvOffset.x = uvOffset.x % 1;
		uvOffset.y = uvOffset.y % 1;
		
		if( GetComponent<Renderer>().enabled )
		{
			GetComponent<Renderer>().material.mainTextureOffset = uvOffset;
		}
	}
}