using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vaultButtonControl : MonoBehaviour
{
    [SerializeField] GameObject[] symbols;
    [SerializeField] SpriteRenderer activeGlow;
    [SerializeField] AudioSource audioSource;
    private int currentSymbolIndex = 0;

    private void OnCollisionEnter2D(Collision2D other)
    {
        foreach(ContactPoint2D hitPos in other.contacts)
        {

            if (hitPos.normal.x == 0.00 && hitPos.normal.y == -1.00)
            {
                audioSource.Play();

                //turn prev off
                symbols[currentSymbolIndex].SetActive(false);

                //glow in n out
                StartCoroutine(glowControl());

                //turn next on
                currentSymbolIndex = (currentSymbolIndex + 1) % symbols.Length;
                symbols[currentSymbolIndex].SetActive(true);
            }
        }
    }

    IEnumerator glowControl()
    {
        float duration = 0.2f;
        float targetAlpha = 0f;
        float currentTime = 0f;
        Color startColor = activeGlow.color;

        //fade out
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startColor.a, targetAlpha, currentTime / duration);
            activeGlow.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        currentTime = 0f;
        targetAlpha = 0f;

        //fade in
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(targetAlpha, startColor.a, currentTime / duration);
            activeGlow.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
            yield return null;
        }
    }
}
