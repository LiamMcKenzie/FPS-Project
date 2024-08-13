using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    [HideInInspector] public Transform targetObject;
    public Vector3 offset;
    [HideInInspector] public Canvas canvas;
    public Vector3 screenPosition;
    private TMP_Text textObject;
    [HideInInspector] public int damage;
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        
        textObject = transform.GetChild(0).GetComponent<TMP_Text>();
        textObject.text = damage.ToString();

        //float randomRotation = Random.Range(-15f, 15f);

        float randomRotation = Random.Range(0f, 360f);

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, randomRotation);
    }

    private void Update()
    {
        if(targetObject == null) //the enemy has been killed or isn't there 
        {
            Destroy(gameObject);
            return;
        }

        Vector3 objectPosition = targetObject.position + offset;

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