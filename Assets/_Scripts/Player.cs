using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {

	public float moveSpeed;
	//private Animator anim;

	public float x;
	public float y;

	public int score;
	public int lives;

	//public Text testing;
	//public Text scoreText;

	// Use this for initialization
	void Start () {
		//anim = gameObject.GetComponent<Animator>();
		lives = 3;

		//anim.speed = 0;
		//printTestText();
		//printText();

	}
	
	// Update is called once per frame
	void Update () {
		x = Input.GetAxisRaw("Horizontal");
		y = Input.GetAxisRaw("Vertical");

		//anim.speed = x != 0 || y != 0 ? 1 : 0;

		//printTestText();
		//printText();

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

	/*void printTestText() 
	{
		testing.text = "x: " + x.ToString() + '\n';
		testing.text += "y: " + y.ToString() + '\n';
	}

	void printText()
	{
		scoreText.text = "Score: " + score.ToString();
	}*/

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag("Pick Up"))
		{
			score += 10;
			other.gameObject.SetActive(false);
		}
	}
}


