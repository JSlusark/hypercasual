using Unity.VisualScripting;
using UnityEngine;

/*
    Level: keeps track of what is required at every level.
*/

public class Level : MonoBehaviour
{
    private float levelScore = 0f; // starting point of likes, is always 0 unless some special bonus carried over from previous level or booster
    private float levelTarget = 100f; // this should become higher at every level


    [SerializeField]
    private LikesBarUI likesBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        likesBar.SetStart(levelScore, levelTarget);
        // when likes bar reaches target, level complete (adds 1 video)
    }

    // Update is called once per frame
    void Update()
    {
    /*     if (Input.GetKeyDown("t"))
        {
            likesBar.UpdateScore(-1f); // - 1 or multiplier if combo chain active
            // Debug.Log("💔 -1  | Score:" + likesBar.score);
        }
        if (Input.GetKeyDown("g"))
        {
            likesBar.UpdateScore(1f); // + 1 or multiplier if combo chain active
            // Debug.Log("❤️ + 1 Score:" + likesBar.score);
        } */
    }

    // public void SetLikes(float amount)
    // {
    //     likes += amount;
    //     likes = Mathf.Clamp(likes, 0f, levelTarget);
    //     likesBar.updateLikes(likes);
    // }
}
