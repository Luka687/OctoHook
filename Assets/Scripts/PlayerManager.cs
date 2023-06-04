using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Vector3 scaleChange;
    private bool plateCollected;
    public GameObject _goPlate;

    private void Start()
    {
        scaleChange = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
    }
    private float movementSpeed = 50f;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.position = transform.position + new Vector3(horizontalInput * movementSpeed * Time.deltaTime, verticalInput * movementSpeed * Time.deltaTime, 0);
        if (horizontalInput > 0)
        {
            this.transform.localScale= new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
        if (horizontalInput < 0)
        {
            this.transform.localScale = new Vector3(-1*Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);

        }
        if (Input.GetButtonDown("E"))
        {

        }
    }

    public void togglePlate(bool boolVar)
    {
        plateCollected = boolVar;

    }
}
