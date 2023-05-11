
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public enum DoAnimationType
{
    Move,
    Rotate,
    Scale,
    Jump,
    Text,
    TextFade,   
    SpriteFade,
    SpriteColor,
    RectTransformMove,
    RectTransformRotate,
    RectTransformScale,
    ImageFade,
    ImageColor,
    ImageFill

}


[AddComponentMenu("DOTween/DotweenAnimation")]
public class DotweenAnimation : MonoBehaviour
{
    public delegate void OnStepComplete();
    public event OnStepComplete onStepComplete;

    [SerializeField] DoAnimationType AnimationType;
    public Vector3 target;
    public Vector3 targetRandom = Vector3.zero;
    [SerializeField] float float1 = 0f;
    [SerializeField] Color color1;  
    [SerializeField] float duration = 2f;
    [SerializeField] float durationMax = -1f;
    [SerializeField] float jumpPower = 2f;
    [SerializeField] Ease easeType = Ease.Linear;   
    [SerializeField] bool isToFrom;
    [SerializeField] bool isLocal;
    [SerializeField] int Loops = -1;
    [SerializeField] float wait = 0f;
    [SerializeField] float waitMax = -1f;
    [SerializeField] LoopType loopType = LoopType.Yoyo;
    [SerializeField] RotateMode rotateMode = RotateMode.LocalAxisAdd;
    [SerializeField] bool isTextMeshPro = false;
    
    [SerializeField] bool isQuaternion = false;
    [SerializeField] bool isWaitEveryLoopStep = false;
    [SerializeField] bool isUnScaledTime = false;
    [SerializeField] bool isScaleZeroOnAwake = false;
    [SerializeField] bool isScaleYZeroOnAwake = false;


    Vector3 toPos;
    public Tween t=null;

    Vector3 pos;
    Vector3 rotation;
    Vector3 scale;

    float ImageAlpha = 0f;
    Color ImgColor;
    float ImageFill = 0f;

    private void Awake()
    {
        if (!this.enabled) return;

        if (isScaleZeroOnAwake)
            transform.localScale = Vector3.zero;

        if (isScaleYZeroOnAwake)
            transform.localScale = new Vector3(transform.localScale.x, 0f,transform.localScale.z);

        if (isLocal)
        {
            pos = transform.localPosition;
            rotation = transform.localEulerAngles;
           
        }
        else
        {
            pos = transform.position;
            rotation = transform.eulerAngles;
        }

        scale = transform.localScale;

        if (durationMax != -1) duration = Random.Range(duration, durationMax);
        if(waitMax != -1) wait = Random.Range(wait, waitMax);

        if (targetRandom != Vector3.zero)
        {
          target = target.Random(target,targetRandom);
        }


        if (AnimationType == DoAnimationType.ImageFade)
            ImageAlpha = GetComponent<Image>().color.a;

        if (AnimationType == DoAnimationType.ImageFill)
            ImageFill = GetComponent<Image>().fillAmount;

        if (AnimationType == DoAnimationType.ImageColor)
            ImgColor = GetComponent<Image>().color;

        if (gameObject.activeInHierarchy) SetTween();
    }

    void Start()
    {
        

    }

    private void OnEnable()
    {
       if(t == null)
        SetTween();
        else
        {
            Pause();
            Kill();
            t = null;
            sequence = null;
            SetTween();
        }
        //Restart();
    }
    bool quitting = false;
    void OnApplicationQuit()
    {
        quitting = true;
    }

    private void OnDisable()
    {
        if (quitting)
            return;

        Pause();
        Kill();
        t = null;
        sequence = null;
        if (isLocal)
        {
            transform.localPosition =    pos ;
            transform.localEulerAngles = rotation  ;

        }
        else
        {
            transform.position = pos  ;
            transform.eulerAngles = rotation  ;
        }

        transform.localScale = scale ;


        if (AnimationType == DoAnimationType.ImageFade)
            GetComponent<Image>().UIChangeAlpha(ImageAlpha);

        if (AnimationType == DoAnimationType.ImageFill)
            GetComponent<Image>().fillAmount = ImageFill;


        if (AnimationType == DoAnimationType.ImageColor)
            GetComponent<Image>().color = ImgColor;
    }

    void SetTween()
    {
        //return;
        // print(gameObject.name);


        if (!GetComponent<DotweenAnimation>().enabled) return;

        if (AnimationType == DoAnimationType.Move)
        {
            if (isToFrom)
            {
                toPos = transform.localPosition;
                transform.localPosition = target;
                target = toPos;
            }

            if (isLocal)
            {
                t = transform.DOLocalMove(target, duration).SetEase(easeType);
                  //  SetLoops(Loops, loopType);//.OnStepComplete(() => onStepComplete());
            }
            else
            {
                t = transform.DOMove(target, duration).SetEase(easeType);
            }



        }


        if (AnimationType == DoAnimationType.Rotate)
        {
            if (!isLocal)
            {
                t = transform.DORotate(target, duration, rotateMode).SetEase(easeType);
            }
            else
            {
                if (!isQuaternion)
                    t = transform.DOLocalRotate(target, duration, rotateMode).SetEase(easeType);
                else
                    t = transform.DOLocalRotateQuaternion(Quaternion.Euler(target), duration).SetEase(easeType);

            }


            if (wait > 0f)
            {
                //t.SetDelay(wait).OnComplete(() => {
                //    if (Loops == -1)
                //    {
                //        RotateLoopWithDelay();
                //    }

                //});
            }


        }


        if (AnimationType == DoAnimationType.Scale)
        {
            t = transform.DOScale(target, duration).SetEase(easeType);
        }


        if (AnimationType == DoAnimationType.Jump)
        {
            if (isLocal)
            {
                t = transform.DOLocalJump(target, jumpPower, 1, duration).SetEase(easeType);
            }
            else
            {
                t = transform.DOJump(target, jumpPower, 1, duration).SetEase(easeType);
            }
        }

        if (AnimationType == DoAnimationType.Text)
        {

            var txt = transform.GetComponent<Text>().text;
            transform.GetComponent<Text>().text = "";
            t = transform.GetComponent<Text>().DOText(txt, duration).SetEase(easeType);
        }

        if (AnimationType == DoAnimationType.TextFade)
        {
            if (isTextMeshPro)
                t = transform.GetComponent<TextMeshProUGUI>().DOFade(float1, duration).SetEase(easeType);
            else
                t = transform.GetComponent<Text>().DOFade(float1, duration).SetEase(easeType);
        }      

        if (AnimationType == DoAnimationType.SpriteFade)
        {

            t = transform.GetComponent<SpriteRenderer>().DOFade(float1, duration).SetEase(easeType);
        }

        if (AnimationType == DoAnimationType.SpriteColor)
        {

            t = transform.GetComponent<SpriteRenderer>().DOColor(color1, duration).SetEase(easeType);
        }

        if (AnimationType == DoAnimationType.RectTransformMove)
        {

            t = transform.GetComponent<RectTransform>().DOLocalMove(target, duration).SetEase(easeType).SetUpdate(true);
        }

        if (AnimationType == DoAnimationType.RectTransformRotate)
        {

            t = transform.GetComponent<RectTransform>().DOLocalRotate(target, duration).
                SetEase(easeType).SetUpdate(true);
        }



        if (AnimationType == DoAnimationType.RectTransformScale)
        {

            t = transform.GetComponent<RectTransform>().DOScale(target, duration).SetEase(easeType)
              .SetUpdate(true);
        }


        if (AnimationType == DoAnimationType.ImageFade)
        {

            t = transform.GetComponent<Image>().DOFade(float1, duration).SetEase(easeType);
        }

        if (AnimationType == DoAnimationType.ImageFill)
        {

            t = transform.GetComponent<Image>().DOFillAmount(float1, duration).SetEase(easeType);
        }

        if (AnimationType == DoAnimationType.ImageColor)
        {

            t = transform.GetComponent<Image>().DOColor(color1, duration).SetEase(easeType);
        }

        if (isUnScaledTime) t.SetUpdate(true);


        if (!isWaitEveryLoopStep)
        {
            t.SetLoops(Loops, loopType);
            t.SetDelay(wait);

        }           
        else
        {
             sequence = DOTween.Sequence();
             sequence.AppendInterval(wait).SetLoops(Loops, loopType).Append(t);
        }

      

        
    }

    Sequence sequence=null;
    void RotateLoopWithDelay()
    {
        t = transform.DORotate(target, duration, rotateMode).SetEase(easeType).SetLoops(Loops, loopType);

        t.SetDelay(wait).OnComplete(() => {

            RotateLoopWithDelay();
        });

    }

    private void Reset()
    {
        target = transform.localPosition;

        if (AnimationType == DoAnimationType.TextFade)
        {
            transform.GetComponent<Text>().DOFade(1f, 0).SetEase(easeType);
        }
    }

    public void Pause()
    {
        
        if (sequence != null) sequence.Pause();
        if(t != null)
        t.Pause();
    }
    public void Resume()
    {
        if (sequence != null) sequence.Play();
        if (t != null)
            t.Play();


    }

    public void Kill()
    {
       // print(gameObject.name);
        

        if (sequence != null) sequence.Kill();
        if (t != null)
            t.Kill();
    }

    public void Restart()
    {
        if (sequence != null) sequence.Kill();
        t.Kill();
        transform.localPosition = Vector3.zero;

        if (AnimationType == DoAnimationType.TextFade)
        {

            transform.GetComponent<Text>().DOFade(1f, 0).SetEase(easeType);
        }

        SetTween();
    
    }
}
