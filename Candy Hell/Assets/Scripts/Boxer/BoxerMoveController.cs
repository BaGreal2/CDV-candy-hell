using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxerMoveController : MonoBehaviour
{
	public float moveSpeed = 2f;
	private float minX, maxX, minY, maxY;
	private bool facingRight = true;
	// Start is called before the first frame update
	void Start()
	{
		Camera mainCamera = Camera.main;
		float halfPlayerWidth = transform.localScale.x / 2f;
		float halfPlayerHeight = transform.localScale.y / 2f;
		minX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + halfPlayerWidth;
		maxX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - halfPlayerWidth;
		minY = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + halfPlayerHeight;
		maxY = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y / 2 - 2.3f - halfPlayerHeight;
	}

	// Update is called once per frame
	void Update()
	{
		float horizontalInput = Input.GetAxisRaw("Horizontal");
		float verticalInput = Input.GetAxisRaw("Vertical");

		Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f).normalized * moveSpeed * Time.deltaTime;

		Vector3 newPosition = transform.position + movement;

		newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
		newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

		transform.position = newPosition;
		if ((horizontalInput > 0 && !facingRight) || (horizontalInput < 0 && facingRight))
		{
			Flip();
		}
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}
