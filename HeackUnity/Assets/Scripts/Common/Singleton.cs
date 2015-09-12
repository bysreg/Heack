using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{

	public static T Instance { get; private set; }

	protected virtual void Awake()
	{
		if (Instance == null)
		{
			Instance = this as T;
		}
		else
		{
			Debug.LogError("There can only be one instance of " + typeof(T).Name);

			gameObject.SetActive(false);
		}
	}
}
