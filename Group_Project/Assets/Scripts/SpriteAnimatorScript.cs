using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimatorScript : MonoBehaviour
{
    [System.Serializable]
    public class AnimationList
    {
        public List<Sprite> spriteList = new List<Sprite>();
    }

    [Space]

    public List<AnimationList> animationList = new List<AnimationList>();
    private List<Sprite> currentSpriteList;

    [Space]

    public bool useUnscaledTime = false;

    private float animationSpeed = 0;
    private bool animationLoop = true;
    private int currentAnimation = 0;
    private float timer = 0;
    private bool methodPlayAnimation = true;

    [Space]
    [Space]

    public int debugSetAnimation = 0;
    public float debugAnimationSpeed = 0;
    public bool debugLoop = true;

    // Start is called before the first frame update
    void Start()
    {
        ChangeAnimation(0);
        SetAnimationSpeed(12, true);
    }

    // Update is called once per frame
    void Update()
    {
        /* debugger
        if (Input.GetKeyDown(KeyCode.C))
        {
            Time.timeScale -= 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Time.timeScale += 0.1f;
        }
        */
        if (Input.GetKeyDown(KeyCode.O))
        {
            ChangeAnimation(debugSetAnimation);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            SetAnimationSpeed(debugAnimationSpeed, debugLoop);
        }

        PlayingAnimation();
    }

    private void ChangeAnimation(int spriteTargetID)
    {
        currentSpriteList = animationList[spriteTargetID].spriteList;

        currentAnimation = 0;
        timer = 0;
        GetComponent<SpriteRenderer>().sprite = currentSpriteList[currentAnimation];

        methodPlayAnimation = true;
    }

    private void SetAnimationSpeed(float speed, bool loop)
    {
        animationSpeed = speed;

        if (loop)
        {
            animationLoop = true;
            methodPlayAnimation = true;
        }
        else
        {
            animationLoop = false;
        }
    }

    private void PlayingAnimation()
    {
        if (!methodPlayAnimation)
        {
            return;
        }

        if (useUnscaledTime)
        {
            timer += Time.unscaledDeltaTime * animationSpeed;
        }
        else
        {
            timer += Time.deltaTime * animationSpeed;
        }

        if (timer > 1 || timer < 0)
        {
            int addFrames = Mathf.FloorToInt(timer);
            currentAnimation += addFrames;

            if ((timer > 1 && currentAnimation >= currentSpriteList.Count) || (timer < 0 && currentAnimation < 0))
            {
                if (animationLoop)
                {
                    int removeFrames = Mathf.FloorToInt(((float)currentAnimation) / currentSpriteList.Count);
                    currentAnimation -= removeFrames * currentSpriteList.Count;
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = currentSpriteList[currentSpriteList.Count - 1];
                    methodPlayAnimation = false;
                    return;
                }
            }

            timer -= addFrames;
        }

        GetComponent<SpriteRenderer>().sprite = currentSpriteList[currentAnimation];
    }
}
