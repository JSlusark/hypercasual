using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Health, MaxHealth;
    [SerializeField]
    private LikesBarUI likesBarUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        likesBarUI.setMaxLikes(MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("t"))
            SetLikes(-10f);
        if (Input.GetKeyDown("g"))
            SetLikes(10f);
    }

    public void SetLikes(float amount)
    {
        Health += amount;
        Health = Mathf.Clamp(Health, 0f, MaxHealth);
        likesBarUI.updateLikes(Health);
    }
}
