

#region Documentation
// Sep-2020 By Muhammad Ali Safdar - muhammadalistar369@gmail.com , princlia@yahoo.com

#region Keys

// *Ex. = Extension Method

#endregion

// Return Type    Methods                                                                         Descriptions

// void           DestroyAllGameObjectsWithTag(string tag)                           -  Destroy All Gameobjects with given string Tag in current scene
// string         GetRendomStringWithSpliter(string stringArr,char spliter)          -  Get a Rendom string in a Long String with Split Character
// Rigidbody[]    GetRigidboidies(Transform transform)                               -  Get All Child Rigidbodies in given transform
// Ex. viod       KinematicOnOff(this Transform transform,bool isColliderOff=true)   -  Turn Kinenmatic On/Off Fromm All Childern's Rigidbody
// Ex. void       SetActiveAll(this GameObject[] objs, bool value)                   -  Gameobjects Array Active / Deactive
// Ex. Vector3    Random(this Vector3 value, Vector3 min, Vector3 max)               -  Randomr Between two Vectors3



using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using UnityEngine.UI;
//using static UnityEditor.Progress;

#endregion



public class MAliGMethods : MonoBehaviour
{
    #region Get Component

    public static Rigidbody[] GetRigidboidies(Transform transform)
    {
        return transform.GetComponentsInChildren<Rigidbody>();
    }

    #endregion

    #region Random

    public static string GetRendomStringWithSpliter(string stringArr,char spliter)
    {
       // UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
        var animArray = stringArr.Split(spliter);
        var randomString = animArray[UnityEngine.Random.Range(0, animArray.Length)];
        return randomString;
    }

    #endregion

    #region GameObject
    /// <summary>
    ///  Destroy All Gameobjects with given string Tag in current scene
    /// </summary>
    /// <param name="tag">Gameobject Tag</param>
    public static void DestroyAllGameObjectsWithTag(string tag)
    {
        var objs = GameObject.FindGameObjectsWithTag(tag);
        foreach (var item in objs)
        {
            Destroy(item);
        }
    }



    #endregion

    #region Coroutine
    public static Coroutine Wait(Action action , float time,MonoBehaviour instance,bool isRealTime=false,bool waitEndFrame=false)
    {
        if(waitEndFrame)
          return  instance.StartCoroutine(AfterTimeEndFrame(action, time));
        //if (delayCor != null) instance.StopCoroutine(delayCor);
        else if(!isRealTime)
        return  instance.StartCoroutine(AfterTime(action,time));
       else
            return instance.StartCoroutine(AfterTimeUnScale(action, time));
    }

    public static Coroutine Wait(Action action, float time, MonoBehaviour instance,Vector3 vec, bool isRealTime = false)
    {
        //if (delayCor != null) instance.StopCoroutine(delayCor);
        if (!isRealTime)
            return instance.StartCoroutine(AfterTime(action, time));
        else
            return instance.StartCoroutine(AfterTimeUnScale(action, time));
    }
    // static Coroutine delayCor;
    static IEnumerator AfterTime(Action action ,float time)
    {
        if (time > 0f)
            yield return new WaitForSeconds(time);
        else yield return null;
        action.Invoke();
    }
    static IEnumerator AfterTimeUnScale(Action action, float time)
    {
        yield return new WaitForSecondsRealtime(time);
        action.Invoke();
    }

    static IEnumerator AfterTimeEndFrame(Action action, float time)
    {
        yield return new WaitForEndOfFrame();
        action.Invoke();
    }

    public static void StopWaitAction(Coroutine c, MonoBehaviour instance)
    {
        instance.StopCoroutine(c);
    }
    #endregion

    #region Numbers
    public static object[] GetSmallCurrency(int n)
    {
        object[] o = new object[2];
        float num = float.Parse(n.ToString());
        if (num < 1000f)
        {
            o[0] = num.ToString("");
            o[1] = "";
        }
        else
        {
            o[0] = (num / 1000).ToString("0.0");
            o[1] = "K";
        }

        return o;
    }
    public static object[] GetSmallCurrency(float num,MidpointRounding midPoint= MidpointRounding.AwayFromZero,bool noAfterZero=false,int roundPlace=2)
    {
        object[] o = new object[2];
       
        if (num < 1000f)
        {
            o[0] = num.ToString("0.");
            o[1] = "";
        }
        else if(num < 100000)
        {
            o[0] = (System.Math.Round((decimal)(num / 1000f),!noAfterZero? 2 : 0, midPoint)); //(num / 1000).ToString(!noAfterZero? "0.0":"");
            o[1] = "K";
        }
        else
        {

            o[0] = (System.Math.Round((decimal)(num / 1000000f), roundPlace, midPoint));//.ToString(!noAfterZero ? ".00": ".00");
            o[1] = "M";

            //if (num / 1000000f < 1f) o[0] =  "<size=250>0</size>"+ o[0];
        }

        return o;
    }

    #endregion

}

public static class ExtensionMethods
{
    #region Math
    public static Vector3 Random(this Vector3 value, Vector3 min, Vector3 max,bool isSameAxis=false)
    {
        if (isSameAxis)
        {
            var r = UnityEngine.Random.Range(min.x, max.x);
            return new Vector3(r, r, r);
        }

        return new Vector3(UnityEngine.Random.Range(min.x, max.x), UnityEngine.Random.Range(min.y, max.y), UnityEngine.Random.Range(min.z, max.z));
    }

    public static float Round(this float value, int digits)
    {
        float mult = Mathf.Pow(10.0f, (float)digits);
        return Mathf.Round(value * mult) / mult;
        //
    }


    #endregion

    #region Rigidbody
    public static void ZeroVelocity(this Rigidbody rigid)
    {
       rigid.velocity = Vector3.zero;
    }
    public static void ZeroAngularVelocity(this Rigidbody rigid)
    {
        rigid.angularVelocity = Vector3.zero;
    }

    /// <summary>
    /// Turn Kinenmatic On/Off Fromm All Childern's Rigidbody
    /// </summary>
    /// <param name="transform"></param>
    public static void KinematicOnOff(this Transform transform,bool isColliderOff=true)
    {
        Rigidbody[] bodies = MAliGMethods.GetRigidboidies(transform);
        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = !rb.isKinematic;
        }

        if (isColliderOff)
        {
            var c = transform.GetComponentsInChildren<Collider>();
            foreach (var item in c)
            {
                if (!item.IsWithTag("Player"))
                {
                    item.enabled = !item.enabled;
                }
               
            }
        }
      

    }

    public static void RagDollForce(this Transform transform, Vector3 minForce, Vector3 maxForce)
    {
        Rigidbody[] bodies = MAliGMethods.GetRigidboidies(transform);
        Vector3 force = Vector3.zero;
        foreach (Rigidbody rb in bodies)
        {
            var f1 = force.Random(minForce, maxForce);
            var f2 = force.Random(minForce, maxForce);
             rb.AddForce(f1, ForceMode.Impulse);

            //rb.AddTorque(f2 * -50f,ForceMode.VelocityChange);
            //  rb.velocity = f1;
            //  rb.AddRelativeForce(f2 * 5,ForceMode.Impulse);
            if (rb.name == "Hips" || rb.name == "Spine" )
            {
              //  rb.maxAngularVelocity = float.MaxValue;
               //  rb.angularVelocity = new Vector3(UnityEngine.Random.Range(-2f,3f), UnityEngine.Random.Range(.5f,1f), 0) * UnityEngine.Random.Range(30f,50f);
               // rb.AddRelativeTorque(new Vector3(-3f,0,0), ForceMode.Impulse);
            }
            //if (rb.name == "Handgun_01" || rb.name.StartsWith("jmy_hat"))
            {


                 rb.maxAngularVelocity = float.MaxValue;
                 rb.angularVelocity = new Vector3(UnityEngine.Random.Range(25f, 55f),
                 UnityEngine.Random.Range(-15f, 15f)
                 , UnityEngine.Random.Range(-15f,15f));


                // rb.AddRelativeTorque(new Vector3(0, 0, 444));
            }
            
           

        }
    }
    public static void RagDollZeroVelocity(this Transform transform)
    {
        Rigidbody[] bodies = MAliGMethods.GetRigidboidies(transform);
        Vector3 force = Vector3.zero;
        foreach (Rigidbody rb in bodies)
        {
            rb.ZeroVelocity();
            //rb.ZeroAngularVelocity();
        }
    }



    #endregion

    #region Colliders / Triggers
    public static string GetName(this UnityEngine.Collision collision)
    {
        return collision.gameObject.name;
    }
    public static string GetName(this UnityEngine.Collider collider)
    {
        return collider.gameObject.name;
    }

    public static string GetTag(this UnityEngine.Collision collision)
    {
        return collision.gameObject.tag;
    }
    public static string GetTag(this UnityEngine.Collider collider)
    {
        return collider.gameObject.tag;
    }
    public static bool IsWithName(this UnityEngine.Collision collision,string name)
    {
        return collision.GetName().Equals(name);      
    }
    public static bool IsNameStartWith(this UnityEngine.Collision collision, string name)
    {
        return collision.GetName().StartsWith(name);
    }
    public static bool IsWithTag(this UnityEngine.Collision collision, string tag)
    {
        return collision.GetTag().Equals(tag);
    }
    public static bool IsWithName(this UnityEngine.Collider collision, string name)
    {
        return collision.GetName().Equals(name);
    }
    public static bool IsNameStartWith(this UnityEngine.Collider collision, string name)
    {
        return collision.GetName().StartsWith(name);
    }
    public static bool IsWithTag(this UnityEngine.Collider collision, string tag)
    {
        return collision.GetTag().Equals(tag);
    }
    public static Vector3 FirstContact(this UnityEngine.Collision collision)
    {
        return collision.contacts[0].normal;
    }



    #endregion

    #region Animation


    public static string SetBoolRandom(this Animator anim,string animations, bool value)
    {
        var randomAnim = MAliGMethods.GetRendomStringWithSpliter(animations, ',');
        anim.SetBool(randomAnim, value);
        return randomAnim;
    }





    #endregion

    #region GameObject

    public static void SetActiveAll(this GameObject[] objs, bool value)
    {
        foreach (var item in objs)
        {
            item.SetActive(value);
        }
    }

    public static void SetActiveAllAsync(this GameObject[] objs, bool value,MonoBehaviour m)
    {
        foreach (var item in objs)
        {
            m.WaitEndFrame(()=> { item.SetActive(value); });
           
        }
        
    }

   


    public static void DestroyAllWithTag(this MonoBehaviour m,string tag)
    {
        MAliGMethods.DestroyAllGameObjectsWithTag(tag);
    }
   

public static void MeshOnOffWithTag(this MonoBehaviour m, string tag,bool isOn=false)
{
        var objs = GameObject.FindGameObjectsWithTag(tag);
        foreach (var item in objs)
        {
            item.GetComponent<MeshRenderer>().enabled = isOn;


        }
}

public static void FxStopAllChild(this GameObject fx,MonoBehaviour m, float delay =0f,bool isDeActiveAfter=true,float deActivateTime=.25f)
    {
       
        fx.SetActive(true);  
       
        MAliGMethods.Wait(() => {

            for (int i = 0; i < fx.transform.childCount; i++)
            {
                if (fx.transform.GetChild(i).GetComponent<ParticleSystem>())
                {
                    fx.transform.GetChild(i).GetComponent<ParticleSystem>().
              Stop(true, ParticleSystemStopBehavior.StopEmitting);
                }
              
            }

            if(isDeActiveAfter)
            MAliGMethods.Wait(() => {
                fx.SetActive(false);
            }, deActivateTime, m);

        }, delay, m);
    }

    public static void FxPlayAllChild(this GameObject fx)
    {

        fx.SetActive(true);      

            for (int i = 0; i < fx.transform.childCount; i++)
            {
                fx.transform.GetChild(i).GetComponent<ParticleSystem>().
                Play(true);
            }       

       
    }

    #endregion

    #region Collections
    public static T[] AddtoArray<T>(this T[] Org, T New_Value)
    {
        T[] New = new T[Org.Length + 1];
        Org.CopyTo(New, 0);
        New[Org.Length] = New_Value;
        return New;
    }

    public static T[] RemoveAt<T>(this T[] source, int index)
    {
        T[] dest = new T[source.Length - 1];
        if (index > 0)
            Array.Copy(source, 0, dest, 0, index);

        if (index < source.Length - 1)
            Array.Copy(source, index + 1, dest, index, source.Length - index - 1);

        return dest;
    }

    public static T[] RandomArray<T>(this T[] arr)
    {
        System.Random rnd = new System.Random();
        return arr.OrderBy(x => rnd.Next()).ToArray();
    }

  
    public static void Random<T>(this IList<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static Vector3[] GoTransformsArrayToVector3ArrayPositions(this Transform[] objs)
    {
        var v = new Vector3[objs.Length];
        int index = 0;
        foreach (var item in objs)
        {
            v[index] = item.position;
            index++;
        }

        return v;
    }

    #endregion

    #region Transform

    public static Transform[] ToTransformArray(this MonoBehaviour[] array)
    {
        Transform[] arr = new Transform[array.Length];

        int index = 0;
        foreach (var item in array)
        {
            arr[index] = item.transform;
            index++;
        }

        return arr;

    }

    #endregion

    #region Debug
    public static void Print(this MonoBehaviour m,object obj)
    {
        MonoBehaviour.print("<color=#00cc00><b>"+obj+ "</b></color>");

    }




    #endregion

    #region Numbers

    public static void ToCurrency(this int n,UnityEngine.UI.Text text)
    {
        var c = MAliGMethods.GetSmallCurrency(n);
       // text.text = "<size=250>$</size>" + c[0] + "<size=250>" +c[1] +"</size>";
        text.text = c[0] + "<size=250> " + c[1] + "</size>";
    }

    public static void ToCurrency(this float n, UnityEngine.UI.Text text)
    {
        var c = MAliGMethods.GetSmallCurrency(int.Parse(n.ToString()));
        // text.text = "<size=250>$</size>" + c[0] + "<size=250>" +c[1] +"</size>";
        text.text = c[0] + "<size=250> " + c[1] + "</size>";
    }
    
    #endregion

    #region Coroutine
    public static void Wait(this MonoBehaviour m,System.Action action,float time,bool isRealTime=false)
    {
          MAliGMethods.Wait(action,time,m,isRealTime);
    }

    public static void WaitEndFrame(this MonoBehaviour m, System.Action action)
    {
        MAliGMethods.Wait(action,0f, m,false,true);
    }

    public static void Wait(this MonoBehaviour m, System.Action action, Vector3 vec, float time, bool isRealTime = false)
    {
       
        MAliGMethods.Wait(action, time, m,vec, isRealTime);
    }



    #endregion

    #region Randoms

    #region Random But Unique

    static Dictionary<string, int> randomChilds = new Dictionary<string, int>();

    public static void RandomChildClear(this MonoBehaviour m)
    {
        randomChilds.Clear();
    }
    public static void RandomChildRegister(this MonoBehaviour m,string key, int value)
    {

        int v = 0;
        if (randomChilds.TryGetValue(key, out v))
        {

            randomChilds[key] = value;
        }
        else
        {
            randomChilds.Add(key, value);
        }

    }

    public static int GetLastLevelChildIndex(this MonoBehaviour m, string key)
    {
        return randomChilds[key];
    }

    public static void AllKeys(this MonoBehaviour m)
    {
        foreach (var item in randomChilds)
        {
          MonoBehaviour .print(item.Key + " : " + item.Value);
        }
    }

    static Dictionary<string, List<int>> randomChildsIndex = new Dictionary<string, List<int>>();

    public static void RandomIndexChildClear(this MonoBehaviour m)
    {
        randomChildsIndex.Clear();
    }
    public static int GetRandomIndexChild(this MonoBehaviour m, string key, int childsCount)
    {
        List<int> v;
        int ret = 0;
        if (randomChildsIndex.TryGetValue(key, out v))
        {

            var r = randomChildsIndex[key];
            ret = UnityEngine.Random.Range(0, childsCount);
            for (int i = 0; i < 7; i++)
            {
                if (IsElementInList(ret, r))
                {
                    ret = UnityEngine.Random.Range(0, childsCount);
                }
                else break;

            }

            r.Add(ret);
            randomChildsIndex[key] = r;
        }
        else
        {
            List<int> r = new List<int>();
            ret = UnityEngine.Random.Range(0, childsCount);
            r.Add(ret);
            randomChildsIndex.Add(key, r);
        }


        return ret;
    }


    static bool IsElementInList(int e, List<int> l)
    {
        bool ret = false;
        foreach (var item in l)
        {
            if (e.Equals(item))
            {
                return true;
            }
        }
        return ret;
    }

    #endregion

    #endregion

    #region Dotween
     // Start From Tw

   static Tween tweenStartOperation;
    public static bool TwStartImageFillOperation(this MonoBehaviour m,Image imgFillStart,Action action)
    {


        if (tweenStartOperation != null)
        {
            tweenStartOperation.Kill();
            imgFillStart.fillAmount = 0;
        }

        float t = 0;

        tweenStartOperation = DOTween.To(() => t, x => t = x, 1, 1)
            .OnUpdate(() =>
            {
                imgFillStart.fillAmount = t;
            }).SetEase(Ease.InQuart)
            .OnComplete(() => {

                action();
            });

        return true;
    }

    public static void TwCancelImageFillOperation(this MonoBehaviour m, Image imgFillStart, Action action=null)
    {
        if (tweenStartOperation != null)
            tweenStartOperation.Kill();
        tweenStartOperation = imgFillStart.DOFillAmount(0, .5f).SetEase(Ease.OutQuint);

        action();
    }


    public static void TwScaleZeoroTo(this Transform t,float min,float max)
    {
       float s =UnityEngine.Random.Range(min, max);
       t.localScale = Vector3.zero;
       t.DOScale(new Vector3(s, s, s), .5f).SetEase(Ease.OutQuint);
    }

    public static Tween TwScaleZeoroToDefault(this Transform t,float time=.5f,float delay=0f,
        Ease ease= Ease.OutQuint,Action onComplete=null)
    {
        Tween r = null;
        var s = t.localScale;
        t.localScale = Vector3.zero;

        try
        {
            r = t.DOScale(s, time).SetEase(ease).SetDelay(delay).OnComplete(() =>
            {

                if (onComplete != null)
                    onComplete();
            });
        }
        catch
        {
            t.localScale = s;

        }

        return r;
    }

    #endregion


    #region UI

    // Start From UI


    public static T UIChangeAlpha<T>(this T g, float newAlpha)
         where T : Graphic
    {
        var color = g.color;
        color.a = newAlpha;
        g.color = color;
        return g;
    }


    #endregion

}


