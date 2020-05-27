using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFunctionality : MonoBehaviour
{
    public GameObject ui;
    public World world;

    // to show current goal
    public Text composite_1;
    public Text composite_2;
    public Text composite_3;
    public Text composite_4;

    // to show current sub goal
    public Text text_atomic_1;
    public Text text_atomic_2;
    public Text text_atomic_3;
    public Text text_atomic_4;

    // Show amount of resources
    public Text amountWood_text;
    public Text amountStone_text;
    public Text amountPlacesAvailable_text;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            ToggleUI();
            ToggleColours();
        }
    }

    public void ToggleColours()
    {
        List<GameObject> listOfObjects = world.GetListofEntities();

        if(ui.active == true)
        {
            SetColours(listOfObjects);
        }
        else if(ui.active == false)
        {
            ColourToWhite(listOfObjects);
        }
    }

    private void ColourToWhite(List<GameObject> listOfObjects)
    {
        for (int i = 0; i < listOfObjects.Count; i++)
        {
            listOfObjects[i].GetComponent<Renderer>().material.color = Color.white;
        }
    }

    private void SetColours(List<GameObject> listOfObjects)
    {
        for (int i = 0; i < listOfObjects.Count; i++)
        {
            switch (listOfObjects[i].GetComponent<MovingEntity>().id)
            {
                case 1:
                    listOfObjects[i].GetComponent<Renderer>().material.color = Color.blue;
                    break;
                case 2:
                    listOfObjects[i].GetComponent<Renderer>().material.color = Color.red;
                    break;
                case 3:
                    listOfObjects[i].GetComponent<Renderer>().material.color = Color.magenta;
                    break;
                case 4:
                    listOfObjects[i].GetComponent<Renderer>().material.color = Color.black;
                    break;
                default:
                    listOfObjects[i].GetComponent<Renderer>().material.color = Color.white;
                    break;
            }
        }
    }
        
    private void ToggleUI()
    {
        if (ui.active == true)
        {
            ui.active = false;
        }
        else if (ui.active == false)
        {
            ui.active = true;
        }
    }

    public void SetTextBasedOnId(int id, Goal currentGoal)
    {
        switch (id)
        {
            case 1:
                composite_1.text = currentGoal.ToString();
                SetAtomicGoalText(text_atomic_1, currentGoal);
                break;
            case 2:
                composite_2.text = currentGoal.ToString();
                SetAtomicGoalText(text_atomic_2, currentGoal);
                break;
            case 3:
                composite_3.text = currentGoal.ToString();
                SetAtomicGoalText(text_atomic_3, currentGoal);
                break;
            case 4:
                composite_4.text = currentGoal.ToString();
                SetAtomicGoalText(text_atomic_4, currentGoal);
                break;
            default:
                break;
        }
    }

    public void SetResourcesText(int amountWood, int amountStone, int amountLocations)
    {
        amountWood_text.text = "Wood: " + amountWood.ToString();
        amountStone_text.text = "Stone: " + amountStone.ToString();
        amountPlacesAvailable_text.text = "Locations avaialble: " + amountLocations.ToString();
    }

    private void SetAtomicGoalText(Text text, Goal currentGoal)
    {
        if (currentGoal.GetCurrentSubGoal() != null)
        {
            text.text = currentGoal.GetCurrentSubGoal().ToString();
        }
    }

} // class
