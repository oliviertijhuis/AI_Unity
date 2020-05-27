using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Bar_loading : MonoBehaviour
{
	public Text inputWeight;

	[SerializeField]
	private GameObject redPlate_Prefab;

	[SerializeField]
	private GameObject bluePlate_Prefab;

	[SerializeField]
	private GameObject yellowPlate_Prefab;

	[SerializeField]
	private GameObject greenPlate_Prefab;

	[SerializeField]
	private GameObject whitePlate_Prefab;

	[SerializeField]
	private GameObject blackPlate_Prefab;

	[SerializeField]
	private GameObject silverPlate_Prefab;

	[SerializeField]
	private GameObject closers_Prefab;

	[SerializeField]
	private GameObject onekg_Prefab;

	[SerializeField]
	private GameObject halfKilo_Prefab;

	[SerializeField]
	private GameObject kwartKilo_Prefab;

	private int xPosition;

	private int yPosition = 1;

	private int zPosition;

	private float totalWeight;

	private const float BARANDCLOSERS = 25f;

	private const float RED_PLATES = 25f;

	private const float BLUE_PLATES = 20f;

	private const float YELLOW_PLATES = 15f;

	private const float GREEN_PLATES = 10f;

	private const float WHITE_PLATES = 5f;

	private const float BLACK_PLATES = 2.5f;

	private const float SILVER_PLATES = 1.25f;

	private const float ONEKG_PLATE = 1f;

	private const float HALFKILO_PLATE = 0.5f;

	private const float KWARTKILO_PLATE = 0.25f;

	private float checkIfWasTotal;

	private float red_amountPlates;

	private float blue_amountPlates;

	private float yellow_amountPlates;

	private float green_amountPlates;

	private float white_amountPlates;

	private float black_amountPlates;

	private float silver_amountPlates;

	private float oneKilo_amountPlates;

	private float halfKilo_amountPlates;

	private float kwartKilo_amountPlates;

	private List<GameObject> InstantiatedPlates;

	private void Start()
	{
		InstantiatedPlates = new List<GameObject>();
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKey(KeyCode.KeypadEnter) || UnityEngine.Input.GetKey(KeyCode.Return))
		{
			ClearScreen();
			ClearArray();
			string text = inputWeight.text;
			Vector3 vector = default(Vector3);
			vector.x = (float)double.Parse(text, NumberStyles.AllowDecimalPoint);
			totalWeight = vector.x;
			CalculatingWeightAlgorithm();
		}
	}

	private void ClearScreen()
	{
		for (int i = 0; i < InstantiatedPlates.Count; i++)
		{
			GameObject obj = InstantiatedPlates[i];
			UnityEngine.Object.Destroy(obj);
		}
	}

	private void ClearArray()
	{
		InstantiatedPlates.Clear();
	}

	private float AmountPlates(float _totalWeight, float _plateWeight)
	{
		return (int)(_totalWeight / _plateWeight);
	}

	private void CheckEquelToTotal(float _amountPlates, float _plateWeight)
	{
		if (totalWeight == _amountPlates * _plateWeight)
		{
			totalWeight -= _amountPlates * _plateWeight;
		}
	}

	private void CalculatingWeightAlgorithm()
	{
		RestartAmountPlates();
		totalWeight -= BARANDCLOSERS;
		totalWeight /= 2f;
		red_amountPlates = AmountPlates(totalWeight, RED_PLATES);
		totalWeight -= red_amountPlates * RED_PLATES;
		CheckEquelToTotal(red_amountPlates, RED_PLATES);
		blue_amountPlates = AmountPlates(totalWeight, BLUE_PLATES);
		totalWeight -= blue_amountPlates * BLUE_PLATES;
		CheckEquelToTotal(blue_amountPlates, BLUE_PLATES);
		yellow_amountPlates = AmountPlates(totalWeight, YELLOW_PLATES);
		totalWeight -= yellow_amountPlates * YELLOW_PLATES;
		CheckEquelToTotal(yellow_amountPlates, YELLOW_PLATES);
		green_amountPlates = AmountPlates(totalWeight, GREEN_PLATES);
		totalWeight -= green_amountPlates * GREEN_PLATES;
		CheckEquelToTotal(green_amountPlates, GREEN_PLATES);
		white_amountPlates = AmountPlates(totalWeight, WHITE_PLATES);
		totalWeight -= white_amountPlates * WHITE_PLATES;
		CheckEquelToTotal(white_amountPlates, WHITE_PLATES);
		black_amountPlates = AmountPlates(totalWeight, BLACK_PLATES);
		totalWeight -= black_amountPlates * BLACK_PLATES;
		CheckEquelToTotal(black_amountPlates, BLACK_PLATES);
		silver_amountPlates = AmountPlates(totalWeight, SILVER_PLATES);
		totalWeight -= silver_amountPlates * SILVER_PLATES;
		CheckEquelToTotal(silver_amountPlates, SILVER_PLATES);
		oneKilo_amountPlates = AmountPlates(totalWeight, ONEKG_PLATE);
		totalWeight -= oneKilo_amountPlates * ONEKG_PLATE;
		CheckEquelToTotal(oneKilo_amountPlates, ONEKG_PLATE);
		halfKilo_amountPlates = AmountPlates(totalWeight, HALFKILO_PLATE);
		totalWeight -= halfKilo_amountPlates * HALFKILO_PLATE;
		CheckEquelToTotal(halfKilo_amountPlates, HALFKILO_PLATE);
		kwartKilo_amountPlates = AmountPlates(totalWeight, KWARTKILO_PLATE);
		totalWeight -= kwartKilo_amountPlates * KWARTKILO_PLATE;
		CheckEquelToTotal(kwartKilo_amountPlates, KWARTKILO_PLATE);
		InstantiatePlates();
		DebugComment();
	}

	private void RestartAmountPlates()
	{
		red_amountPlates = 0f;
		blue_amountPlates = 0f;
		yellow_amountPlates = 0f;
		green_amountPlates = 0f;
		white_amountPlates = 0f;
		black_amountPlates = 0f;
		silver_amountPlates = 0f;
		oneKilo_amountPlates = 0f;
		halfKilo_amountPlates = 0f;
		kwartKilo_amountPlates = 0f;
		xPosition = 0;
	}

	private void InstantiatePlates()
	{
		LoopPlates(red_amountPlates, redPlate_Prefab);
		LoopPlates(blue_amountPlates, bluePlate_Prefab);
		LoopPlates(yellow_amountPlates, yellowPlate_Prefab);
		LoopPlates(green_amountPlates, greenPlate_Prefab);
		LoopPlates(white_amountPlates, whitePlate_Prefab);
		LoopPlates(black_amountPlates, blackPlate_Prefab);
		LoopPlates(silver_amountPlates, silverPlate_Prefab);
		LoopPlates(oneKilo_amountPlates, onekg_Prefab);
		LoopPlates(halfKilo_amountPlates, halfKilo_Prefab);
		LoopPlates(kwartKilo_amountPlates, kwartKilo_Prefab);
		xPosition += 2;
		GameObject item = UnityEngine.Object.Instantiate(closers_Prefab, new Vector3(xPosition, yPosition, zPosition), Quaternion.identity);
		InstantiatedPlates.Add(item);
	}

	private void LoopPlates(float amountPlates, GameObject preFab)
	{
		for (int i = 0; (float)i < amountPlates; i++)
		{
			xPosition++;
			GameObject item = UnityEngine.Object.Instantiate(preFab, new Vector3(xPosition, yPosition, zPosition), Quaternion.identity);
			InstantiatedPlates.Add(item);
		}
	}

	private void DebugComment()
	{
		UnityEngine.Debug.Log("Weight: " + totalWeight);
		UnityEngine.Debug.Log("25: " + red_amountPlates);
		UnityEngine.Debug.Log("20: " + blue_amountPlates);
		UnityEngine.Debug.Log("15: " + yellow_amountPlates);
		UnityEngine.Debug.Log("10: " + green_amountPlates);
		UnityEngine.Debug.Log("5: " + white_amountPlates);
		UnityEngine.Debug.Log("2.5: " + black_amountPlates);
		UnityEngine.Debug.Log("1.25: " + silver_amountPlates);
		UnityEngine.Debug.Log("1.0: " + oneKilo_amountPlates);
		UnityEngine.Debug.Log("0.5: " + halfKilo_amountPlates);
		UnityEngine.Debug.Log("0.25: " + kwartKilo_amountPlates);
	}
}
