using UnityEngine;
using System.Collections;

public class UnityPoolManager  
{
    private static UnityPoolManager instance;

    protected static int maxInstanceCount = 100;

    protected static PoolManager<string, UnityPoolObject> poolManager;

    static UnityPoolManager()
    {
       poolManager = new PoolManager<string, UnityPoolObject>(maxInstanceCount);
    }

    public static UnityPoolManager Instance
    {
        get
        {
            if (instance == null)
            {
              //  Debug.Log("UnityPoolManager create");

                instance = new UnityPoolManager();


            }

            return instance;
        }
    }


    public void ClearPool()
    {
        poolManager.ClearPool();
    }
	
    public virtual bool Push(string groupKey , UnityPoolObject poolObject)
	{
		return poolManager.Push (groupKey , poolObject);
	}

    public virtual bool PushStartScene(string groupKey, UnityPoolObject poolObject)
    {
        return poolManager.Push(groupKey, poolObject, false);
    }

    public virtual bool Push(System.Enum e, UnityPoolObject poolObject)
    {
        return poolManager.Push(e.ToString(), poolObject);
    }

    public virtual  UnityPoolObject Pop(string groupKey)
	{
  		return poolManager.Pop<UnityPoolObject> (groupKey);
	}

    public virtual UnityPoolObject Pop(System.Enum e)
    {
        return poolManager.Pop<UnityPoolObject>(e.ToString());
    }

    public virtual UnityPoolObject GetUnityPoolObject(System.Enum e)
    {
        return poolManager.GetBaseWindowObject<UnityPoolObject>(e.ToString());
    }

    public virtual WindowViewBase GetBaseWindowObject(System.Enum e)
    {
        return poolManager.GetBaseWindowObject<WindowViewBase>(e.ToString());
    }

}
