using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeadboardScrollViewText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI leadboardText;

    void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Return))
        {
            leadboardText.text += "\ntest";
        }
        
    }

}
