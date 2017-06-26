using UnityEngine;
using System.Collections;

public interface IPoolObject<T>
{
	T Group{ get;}
	void Create ();
	void OnPush();
}
