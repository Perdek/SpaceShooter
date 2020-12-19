using System.Collections.Generic;
using UnityEngine;

public abstract class NotifiableValueVisualization<T, U> where T : NotifiableValue<U>
{
	#region MEMBERS

	[SerializeField]
	private Transform iconsParentTransform = null;
	[SerializeField]
	private GameObject visualizationPrefab = null;

	#endregion

	#region PROPERTIES

	private Transform IconsParentTransform => iconsParentTransform;
	private GameObject VisualizationPrefab => visualizationPrefab;

	public List<GameObject> SpawnedVisualizationElements {
		get;
		set;
	} = new List<GameObject>();

	public T ValueReference {
		get;
		set;
	}

	#endregion

	#region METHODS

	public abstract void UpdateVisualization(U value);

	public void RegisterValue(T newValue)
	{
		DetachEvents();

		ValueReference = newValue;

		UpdateVisualization(ValueReference.Value);

		AttachEvents();
	}

	public void UnregisterValue()
	{
		DetachEvents();

		ValueReference = null;
	}

	public virtual void AttachEvents()
	{
		ValueReference.OnValueSet += UpdateVisualization;
	}

	public virtual void DetachEvents()
	{
		ValueReference.OnValueSet -= UpdateVisualization;
	}

	public void AddElement(int value)
    {
        for (int i = 0; i < value; i++)
        {
			AddElement();
		}
    }

	public void AddElement()
	{
		GameObject newUIElement = GameObject.Instantiate(VisualizationPrefab, IconsParentTransform);
		newUIElement.SetActive(true);
		SpawnedVisualizationElements.Add(newUIElement);
	}

	public void RemoveElement(GameObject uiElement)
	{
		GameObject.Destroy(uiElement.gameObject);
		SpawnedVisualizationElements.Remove(uiElement);
	}

	public void RemoveLastElement(int value)
	{
		for (int i = 0; i < value; i++)
		{
			RemoveLastElement();
		}
	}

	public void RemoveLastElement()
	{
		RemoveElement(SpawnedVisualizationElements[SpawnedVisualizationElements.Count - 1].gameObject);
	}

	public void ClearPanel()
	{
		for (int i = SpawnedVisualizationElements.Count - 1; i >= 0; i--)
		{
			RemoveElement(SpawnedVisualizationElements[i]);
		}
	}

	#endregion

	#region CLASS ENUMS

	#endregion
}