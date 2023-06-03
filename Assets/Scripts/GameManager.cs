using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private string[] sastojci = new string[] { "cokolada","vanila","banana","pistaci","jagoda"};
    private List<string[]> narudzbina = new List<string[]>();

    // Start is called before the first frame update
    void Start()
    {
        generisiRecepte();
        foreach (string[] s in narudzbina)
        {
            foreach (string element in s)
            {
                Debug.Log(element);
            }
        }
        
    }

    public void generisiRecepte()
    {
        string[] recept = new string[3];
        for (int i = 0; i < 3; i++)
        {
            recept[i] = sastojci[Random.Range(0, sastojci.Length)];
        }

        narudzbina.Add(recept);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
