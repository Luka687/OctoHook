using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private Vector3 scaleChange;
    private bool plateCollected;
    private bool entered;
    private string state;
    public GameObject _goPlate;

    public List<Sprite> cakeSprites = new List<Sprite>();
    public List<string> fishOrder = new List<string>();
    public GameObject _goFishOrder;
    private List<GameObject> fishOrderImages = new List<GameObject>();
    private int brojSlojeva = 0;
    private GameObject selectedFish;

    private void Start()
    {
        foreach (Transform child in _goFishOrder.transform)
        {
            fishOrderImages.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }
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
        if (Input.GetButtonDown("E") && entered)
        {
            switch (state)
            {
                case "Plate":
                    togglePlate(true);
                    break;
                case "Banana":
                    dodajSloj(0, "Banana");
                    break;
                case "Kiwi":
                    dodajSloj(1, "Kiwi");
                    break;
                case "Jagoda":
                    dodajSloj(3, "Jagoda");
                    break;
                case "Cokolada":
                    dodajSloj(4, "Cokolada");
                    break;
                case "Borovnica":
                    dodajSloj(2, "Borovnica");
                    break;
                case "Recycle":
                    togglePlate(false);
                    recycle();
                    break;
                case "Riba":
                    selectedFish.GetComponent<Fish>().compareOrder(fishOrder);
                    togglePlate(false);
                    recycle();
                    break;
                default:
                    break;
            }
        }
    }

    public void recycle()
    {
        fishOrder.Clear();
        brojSlojeva = 0;
        foreach(GameObject go in fishOrderImages)
        {
            go.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Plate"))
        {
            state = "Plate";
        }
        if (collision.tag.Equals("Recycle"))
        {
            state = "Recycle";
        }
        if (collision.tag.Equals("Kiwi"))
        {
            state = "Kiwi";
        }
        if (collision.tag.Equals("Jagoda"))
        {
            state = "Jagoda";
        }
        if (collision.tag.Equals("Cokolada"))
        {
            state = "Cokolada";
        }
        if (collision.tag.Equals("Borovnica"))
        {
            state = "Borovnica";
        }
        if (collision.tag.Equals("Banana"))
        {
            state = "Banana";
        }
        if (collision.tag.Equals("Riba"))
        {
            state = "Riba";
            selectedFish = collision.transform.parent.gameObject;
        }

        entered = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        entered = false;
    }

    public void togglePlate(bool boolVar)
    {
        plateCollected = boolVar;
        _goPlate.SetActive(plateCollected);
        
    }

    public void dodajSloj(int brojSprite, string s)
    {
        if (brojSlojeva < 3 && plateCollected)
        {
            fishOrder.Add(s);
            fishOrderImages[brojSlojeva].SetActive(true);
            fishOrderImages[brojSlojeva].GetComponent<Image>().sprite = cakeSprites[brojSprite];
            brojSlojeva++;
        }
    }

}
