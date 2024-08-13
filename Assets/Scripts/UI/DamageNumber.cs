using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    public Transform targetObject;
    public Canvas canvas;
    public Vector3 screenPosition;
    private TMP_Text textObject;
    [HideInInspector] public int damage;
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        textObject = gameObject.GetComponent<TMP_Text>();
        textObject.text = damage.ToString();
    }

    private void Update()
    {
        if(targetObject == null) //the enemy has been killed or isn't there 
        {
            Destroy(gameObject);
            return;
        }

        Vector3 objectPosition = targetObject.position;

        screenPosition = Camera.main.WorldToScreenPoint(objectPosition);

        gameObject.transform.position = screenPosition;

        if(screenPosition.z < 0) //when the object is off screen it sometimes shows up on screen.
        {
            textObject.enabled = false;
        }
        else
        {
            textObject.enabled = true;
        }
    }
}