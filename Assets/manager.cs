using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public static class ButtonExtension
{
	public static void AddEventListener<T> (this Button button, T param, Action<T> OnClick)
	{
		button.onClick.AddListener (delegate() {
			OnClick (param);
		});
	}
}

public class manager : MonoBehaviour
{

	[Serializable]
	public struct Game
	{
		public string Name;
		public string Description;
		public string Price;
		public Sprite Icon;
	}

	[SerializeField] Game[] allGames;
	[SerializeField] Game[] PlayerItems;

	void Start ()
	{
		GameObject buttonTemplate = transform.GetChild (0).gameObject;
		GameObject g;

		int N = allGames.Length;

		for (int i = 0; i < N; i++) {
			g = Instantiate (buttonTemplate, transform);
			g.transform.GetChild (0).GetComponent <TMP_Text> ().text = allGames [i].Name; 
			g.transform.GetChild (1).GetComponent <TMP_Text> ().text = allGames [i].Description;
			g.transform.GetChild (2).GetComponent <TMP_Text> ().text = allGames [i].Price;
			g.transform.GetChild (3).GetComponent <Image> ().sprite = allGames [i].Icon;

			g.GetComponent <Button> ().AddEventListener (i, ItemClicked);
		}

		Destroy (buttonTemplate);
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			{
				AddItem();
			}
	}
	
	public void AddItem()
	{
		Game newGame = new Game
		{
			Name = "New Game",
			Description = "This is a new game",
			Price = "0",
			Icon = null
		};

		Game[] updatedGames = new Game[allGames.Length + 1];

		for (int i = 0; i < allGames.Length; i++)
		{
			updatedGames[i] = allGames[i];
		}

		updatedGames[allGames.Length] = newGame;

		allGames = updatedGames;

		GameObject newButton = Instantiate(transform.GetChild(0).gameObject, transform);
		newButton.transform.GetChild(0).GetComponent<TMP_Text>().text = newGame.Name;
		newButton.transform.GetChild(1).GetComponent<TMP_Text>().text = newGame.Description;
		newButton.transform.GetChild(2).GetComponent<TMP_Text>().text = newGame.Price;
		newButton.transform.GetChild(3).GetComponent<Image>().sprite = newGame.Icon;

		newButton.GetComponent<Button>().AddEventListener(allGames.Length - 1, ItemClicked);
	}

	void ItemClicked(int itemIndex)
	{
		Debug.Log("------------item " + itemIndex + " clicked---------------");
		Debug.Log("name " + allGames[itemIndex].Name);
		Debug.Log("desc " + allGames[itemIndex].Description);
		Debug.Log(allGames[itemIndex]);

		// Add the clicked item to the PlayerItems array
		AddToPlayerItems(allGames[itemIndex]);

		// Remove the clicked item from the allGames array
		RemoveFromAllGames(itemIndex);

	
	}

	void UpdateUI()
	{
		// Clear the current UI elements
		foreach (Transform child in transform)
		{
			Destroy(child.gameObject);
		}

		// Instantiate new UI elements for allGames
		GameObject buttonTemplate = transform.GetChild(0).gameObject;
		GameObject g;

		for (int i = 0; i < allGames.Length; i++)
		{
			g = Instantiate(buttonTemplate, transform);
			g.transform.GetChild(0).GetComponent<TMP_Text>().text = allGames[i].Name;
			g.transform.GetChild(1).GetComponent<TMP_Text>().text = allGames[i].Description;
			g.transform.GetChild(2).GetComponent<TMP_Text>().text = allGames[i].Price;
			g.transform.GetChild(3).GetComponent<Image>().sprite = allGames[i].Icon;
			g.GetComponent<Button>().AddEventListener(i, ItemClicked);
		}

		// Destroy the button template
		Destroy(buttonTemplate);
	}

	void AddToPlayerItems(Game game)
	{
		// Create a new array with increased size to accommodate the new game in PlayerItems
		Game[] updatedPlayerItems = new Game[PlayerItems.Length + 1];

		// Copy the existing items to the updatedPlayerItems array
		for (int i = 0; i < PlayerItems.Length; i++)
		{
			updatedPlayerItems[i] = PlayerItems[i];
		}

		// Add the new game to the last index of the updatedPlayerItems array
		updatedPlayerItems[PlayerItems.Length] = game;

		// Update the PlayerItems array with the updated array
		PlayerItems = updatedPlayerItems;
	}

	void RemoveFromAllGames(int itemIndex)
	{
		// Create a new array with decreased size to remove the clicked game from allGames
		Game[] updatedAllGames = new Game[allGames.Length - 1];

		// Copy the existing games before the clicked index
		for (int i = 0; i < itemIndex; i++)
		{
			updatedAllGames[i] = allGames[i];
		}

		// Copy the existing games after the clicked index
		for (int i = itemIndex + 1; i < allGames.Length; i++)
		{
			updatedAllGames[i - 1] = allGames[i];
		}

		// Update the allGames array with the updated array
		allGames = updatedAllGames;
	}

}