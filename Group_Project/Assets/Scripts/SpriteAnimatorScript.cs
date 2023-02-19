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
    public List<Sprite> currentSpriteList;

    [Space]

    public bool useUnscaledTime = false;

    [HideInInspector]
    public int currentAnimation = -1;

    private int currentFrame = 0;
    private float animationSpeed = 0;
    private bool animationLoop = true;
    private float timer = 0;
    private bool methodPlayAnimation = true;

    // Start is called before the first frame update
    void Start()
    {
        ChangeAnimation(0);
        SetAnimationSpeed(12, true);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        PlayingAnimation();
    }

    public void ChangeAnimation(int spriteTargetID)
    {
        if (currentAnimation != spriteTargetID)
        {
            currentSpriteList = animationList[spriteTargetID].spriteList;

            currentFrame = 0;
            timer = 0;
            GetComponent<SpriteRenderer>().sprite = currentSpriteList[currentFrame];

            methodPlayAnimation = true;
            currentAnimation = spriteTargetID;
        }
    }

    public void SetAnimationSpeed(float speed, bool loop)
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
            timer += Time.fixedUnscaledDeltaTime * animationSpeed;
        }
        else
        {
            timer += Time.fixedDeltaTime * animationSpeed;
        }

        if (timer > 1 || timer < 0)
        {
            int addFrames = Mathf.FloorToInt(timer);
            currentFrame += addFrames;

            if ((timer > 1 && currentFrame >= currentSpriteList.Count) || (timer < 0 && currentFrame < 0))
            {
                if (animationLoop)
                {
                    int removeFrames = Mathf.FloorToInt(((float)currentFrame) / currentSpriteList.Count);
                    currentFrame -= removeFrames * currentSpriteList.Count;
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

        GetComponent<SpriteRenderer>().sprite = currentSpriteList[currentFrame];
    }
}
