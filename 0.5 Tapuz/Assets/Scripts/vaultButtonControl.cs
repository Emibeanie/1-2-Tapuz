using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vaultButtonControl : MonoBehaviour
{
    [SerializeField] GameObject[] symbols;
    private int currentSymbolIndex = 0;

    private void OnCollisionEnter2D(Collision2D other)
    {
        foreach(ContactPoint2D hitPos in other.contacts)
        {

            if (hitPos.normal.x == 0.00 && hitPos.normal.y == -1.00)
            {
                Debug.Log("button pressed");
                //turn prev off
                symbols[currentSymbolIndex].SetActive(false);

                currentSymbolIndex = (currentSymbolIndex + 1) % symbols.Length;
                //turn next on
                symbols[currentSymbolIndex].SetActive(true);

            }

        }
    }
}
