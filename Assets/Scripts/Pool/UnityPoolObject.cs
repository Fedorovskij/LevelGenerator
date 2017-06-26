using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnityPoolObject : MonoBehaviour , IPoolObject<string> 
{ 
	public virtual string Group { get {return name;} }
  
    public virtual void Create()  
    { 
        gameObject.SetActive(true); 
    }

	public virtual void OnPush() 
	{
        gameObject.SetActive(false);
	}

	public virtual void Push() 
	{	
        	
 	}     
}
