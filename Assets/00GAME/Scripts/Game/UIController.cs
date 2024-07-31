using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : Singleton<UIController>
{
    [SerializeField] Text _textInfo;
   
    // Start is called before the first frame update
    void Start()
    {
    
    }

    public void ShowText(Vector2 pos, string content, Color c)
    {
        Text objTxt = ObjectPooling.Instant.getObjectType<Text>(_textInfo);
        objTxt.text = content;
        objTxt.transform.position = pos;
        objTxt.color = c;

        objTxt.transform.SetParent(this.transform);
        objTxt.transform.localScale = Vector3.one;

        objTxt.gameObject.SetActive(true);
    }
}
