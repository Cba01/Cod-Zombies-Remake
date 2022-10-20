using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBox : Interactable
{
    [SerializeField]
    private GameObject mysteryBox;
    private bool mysteryBoxOpen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void Interact()
    {
        mysteryBoxOpen = !mysteryBoxOpen;
        mysteryBox.GetComponent<Animator>().SetBool("isOpen", mysteryBoxOpen);
    }
}
