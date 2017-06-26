using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PoolManager<K , V> where V :IPoolObject<K> 
{
	public virtual int MaxInstances{ get; protected set;}
	public virtual int InstancesCount{ get; protected set;}
	public virtual int CacheInstances{ get; protected set;}
 
	protected Dictionary<K , List<V>> objects;
	protected Dictionary<K , List<V>> cache;

	public PoolManager(int maxInstance)
	{
		MaxInstances = maxInstance;

		objects = new Dictionary<K, List<V>> ();
		cache = new Dictionary<K, List<V>> ();
	}

	public virtual bool CanPush()
	{
		return InstancesCount + 1 < MaxInstances;
	}

	public virtual bool Push(K groupKey , V value , bool onPush = true)
	{
		bool result = true;

       // Debug.Log("groupKey " + groupKey);

		if (CanPush ()) 
		{
            if(onPush)
            value.OnPush ();

            if (!objects.ContainsKey(groupKey))
            {
                objects.Add(groupKey, new List<V>());
            }        

			objects [groupKey].Add (value);
			Type type = value.GetType ();

			if (!cache.ContainsKey (groupKey)) {

               // Debug.Log("type " + type);

				cache.Add (groupKey, new List<V> ()); 
			}

            cache[groupKey].Add(value);

        }

        return result;
	}
 
	public virtual T Pop<T>(K groupKey) where T : V
	{
		T result = default(T);

        if (objects.ContainsKey(groupKey) && objects[groupKey].Count > 0)
        {
            for (int i = 0; i < objects[groupKey].Count; i++)
            {
                if (objects[groupKey][i] is T)
                {
                    result = (T)objects[groupKey][i];
                    Type type = result.GetType();
                    RemoveObject(groupKey, i);
                    //RemoveFromCache(result, type);
                    result.Create();
                    break;
                }
            }
        }
        else
        {

			Debug.LogError("no window " + groupKey);
        }

		return result;
	}

    public virtual T GetBaseWindowObject<T>(K groupKey) where T : V
    {
        T result = default(T);
        if (cache.ContainsKey(groupKey) && cache[groupKey].Count > 0)
        {
            for (int i = 0; i < cache[groupKey].Count; i++)
            {
                if (cache[groupKey][i] is T)
                {
                    result = (T)cache[groupKey][i];
                    Type type = result.GetType();
                   // RemoveObject(groupKey, i);
                    //RemoveFromCache(result, type);
                     break;
                }
            }
        }

        return result;
    }

    protected virtual bool ValidateForPop(K type)
	{
		return cache.ContainsKey (type) && cache [type].Count > 0;
	}

	protected virtual void RemoveObject(K groupKey , int idx)
	{
		if (idx >= 0 && idx < objects [groupKey].Count) 
		{
			objects [groupKey].RemoveAt (idx);

			if (objects [groupKey].Count == 0) 
			{
				objects.Remove (groupKey);
			}
		}
	}

	protected void RemoveFromCache(V value, K type)
	{
		if (cache.ContainsKey(type))
		{
			cache[type].Remove(value);

			if (cache[type].Count == 0)
			{
				cache.Remove(type);
			}
		}
	}

    //testCose
    public void ClearPool()
    {
        cache.Clear();
        objects.Clear();
    }

}
