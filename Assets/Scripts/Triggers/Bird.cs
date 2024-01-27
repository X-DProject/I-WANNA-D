using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bird : MonoBehaviour, IPointerClickHandler
{
    public KeyCode key;
    public AudioClip tone;
    public int toneNum;
    private SpriteRenderer spriteRenderer;
    public Sprite normalSprite;
    public Sprite singSprite;

    public bool canChirping = true;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canChirping && Input.GetKey(key))
        {
            Chirping();
        }
    }

    public void Chirping()
    {
        StartCoroutine(OnChirping());
        GameInstance.Signal("fx.play", tone);
        GameInstance.Signal("tone.update", toneNum);
    }

    private IEnumerator OnChirping()
    {
        spriteRenderer.sprite = singSprite;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.sprite = normalSprite;
        yield break;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(canChirping)
        {
            Chirping();
        }
    }
}
