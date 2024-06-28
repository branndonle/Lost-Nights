using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Temp : MonoBehaviour
{
    public GameObject obj;
    public bool useOldInputSystem;

    public float timeBetweenSpawns = 0.25f;
    float currentTime;

    PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (useOldInputSystem)
        {
            OldInputSystem();

        } else
        {
        }
    }

    void OldInputSystem()
    {
        currentTime += Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            print("Old input System: Key is down");

            if (currentTime > timeBetweenSpawns)
            {
                Destroy(Instantiate(obj, transform.position, Quaternion.identity), 30f);
                currentTime = 0;
            }
        }
    }

    


}
