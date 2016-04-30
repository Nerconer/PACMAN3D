using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {

	public float moveSpeed;
	//private Animator anim;

	private float x;
	private float y;

	public Text testing;

	// Use this for initialization
	void Start () {
		//anim = gameObject.GetComponent<Animator>();
		//anim.speed = 0;
		printTestText();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		x = Input.GetAxisRaw("Horizontal");
		y = Input.GetAxisRaw("Vertical");

		//anim.speed = x != 0 || y != 0 ? 1 : 0;
		printTestText();
		//if (anim.speed != 0) {
			if (x > 0) {
				transform.position += Vector3.right * Time.deltaTime * moveSpeed;
				transform.localEulerAngles = new Vector3(0, 90, 0);
			} else if (x < 0) {
				transform.position += Vector3.left * Time.deltaTime * moveSpeed;
				transform.localEulerAngles = new Vector3(0, 270, 0);
			} else if (y > 0) {
				transform.position += Vector3.forward * Time.deltaTime * moveSpeed;
				transform.localEulerAngles = new Vector3(0, 0, 0);
			} else if (y < 0) {
				transform.position += Vector3.back * Time.deltaTime * moveSpeed;
				transform.localEulerAngles = new Vector3(0, 180, 0);
			}
		//}
	}

	void printTestText() 
	{
		testing.text = "x: " + x.ToString() + '\n';
		testing.text += "y: " + y.ToString() + '\n';
	}
}


