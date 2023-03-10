using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimatorScript : MonoBehaviour
{
    public GameObject target;

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
    [HideInInspector]
    public bool methodPlayAnimation = true;

    private int currentFrame = 0;
    private float animationSpeed = 0;
    private bool animationLoop = true;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
        {
            target = gameObject;
        }

        ChangeAnimation(0, 12, true);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        PlayingAnimation();
    }

    public void ChangeAnimation(int spriteTargetID, float speed, bool loop)
    {
        if (currentAnimation != spriteTargetID)
        {
            currentSpriteList = animationList[spriteTargetID].spriteList;

            currentFrame = 0;
            timer = 0;
            target.GetComponent<SpriteRenderer>().sprite = currentSpriteList[currentFrame];

            methodPlayAnimation = true;
            currentAnimation = spriteTargetID;
        }

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
                    target.GetComponent<SpriteRenderer>().sprite = currentSpriteList[currentSpriteList.Count - 1];
                    methodPlayAnimation = false;
                    return;
                }
            }

            timer -= addFrames;
        }

        target.GetComponent<SpriteRenderer>().sprite = currentSpriteList[currentFrame];
    }
}
