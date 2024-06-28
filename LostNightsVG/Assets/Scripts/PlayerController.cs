using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour {
	
	public float speed;
	private GameObject playerObject;
	public float jumpForce = 5f;
	public TextMeshProUGUI countText;
	public GameObject winTextObject;

    private float movementX;
    private float movementY;

	private Rigidbody rb;
	private int count;
	public GameObject player;
	private bool isCrouched = false;
	private float timer = 0f;
	private bool speedUp;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		count = 0;

		SetCountText ();
                winTextObject.SetActive(false);
	}

	void FixedUpdate ()
	{
		Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
		rb.AddForce(movement * speed);
		if (Input.GetKeyDown(KeyCode.Space))
        {
            // rb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
			if (!isCrouched) {
				player.transform.localScale = new Vector3(.5f,.5f,.5f);
				isCrouched = true;
			}
			else {
				player.transform.localScale = new Vector3(1f,1f,1f);
				isCrouched = false;
			}
		}
	}

	void OnTriggerEnter(Collider other) 
	{
		Debug.Log(other.gameObject.tag);
		if (other.gameObject.CompareTag ("PickUp"))
		{
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		}
		if (other.gameObject.CompareTag ("2Pointer"))
		{
			other.gameObject.SetActive (false);
			count = count + 2;
			SetCountText ();
		}
		if (other.gameObject.CompareTag("Speed"))
        {
			this.speed = 2 * this.speed;
			speedUp = true;
			timer = 0;
        }
	}
    private void Update()
    {
        if(speedUp)
        {
			timer += Time.deltaTime;
			Debug.Log(timer);
			if(timer > 60)
            {
				this.speed = this.speed / 2;
				speedUp = false;
            }
        }
    }

    void OnMove(InputValue value)
        {
        	Vector2 v = value.Get<Vector2>();
        	movementX = v.x;
        	movementY = v.y;
        }

        void SetCountText()
	{
		countText.text = "Count: " + count.ToString();

		if (count >= 12) 
		{
                    winTextObject.SetActive(true);
		}
	}
}
