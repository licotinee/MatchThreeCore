using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class txtMovingUp : MonoBehaviour
{
    [SerializeField] float _lifeTime, _speed;
    float _countDown;

    Text _currentText;

    private void Start()
    {
        _currentText = this.GetComponent<Text>();
    }


    private void OnEnable()
    {
        _countDown = _lifeTime; 
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector2.up * _speed * Time.deltaTime);
        _countDown -= Time.deltaTime;
        if (_countDown <= 0)
        {
            this.gameObject.SetActive(false);
        }

        Color c = _currentText.color;
        c.a = _countDown / _lifeTime;

        _currentText.color = c;
    }
}
