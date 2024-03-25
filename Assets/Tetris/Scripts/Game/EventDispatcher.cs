using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDispatcher
{
	public delegate void EventDispatcherDelegate(object evtData);
	Dictionary<string, List<EventDispatcherDelegate>> m_listeners = new Dictionary<string, List<EventDispatcherDelegate>>();

	public void addListener(string evtName, EventDispatcherDelegate callback)
	{
		List<EventDispatcherDelegate> evtListeners = null;
		if (m_listeners.TryGetValue(evtName, out evtListeners))
		{
			evtListeners.Remove(callback); //make sure we dont add duplicate
			evtListeners.Add(callback);
		}
		else
		{
			evtListeners = new List<EventDispatcherDelegate>();
			evtListeners.Add(callback);

			m_listeners.Add(evtName, evtListeners);
		}
	}

	public void dropListener(string evtName, EventDispatcherDelegate callback)
	{
		List<EventDispatcherDelegate> evtListeners = null;
		if (m_listeners.TryGetValue(evtName, out evtListeners))
		{
			for (int i = 0; i < evtListeners.Count; i++)
			{
				evtListeners.Remove(callback);
			}
		}
	}
	public void dispatch(string evtName, object evt)
	{
		//FIXME: might need to COPY the list<dispatchers> here so that an 
		//	event listener that results in adding/removing listeners does 
		//	not invalidate this for loop

		List<EventDispatcherDelegate> evtListeners = null;
		if (m_listeners.TryGetValue(evtName, out evtListeners))
		{
			for (int i = 0; i < evtListeners.Count; i++)
			{
				evtListeners[i](evt);
			}
		}
	}

}
