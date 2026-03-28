using Godot;
using System;

public partial class Plate : Area2D
{
	private Area2D _pastaDetectionZone;
	private Area2D _sauceDetectionZone;
	private Area2D _basilDetectionZone;
	private Area2D _saltDetectionZone;
	private Area2D _pepperDetectionZone;
	private Sprite2D _plateSprite;
	private Texture2D _platePasta;
	private Texture2D _platePastaSauce;
	private Texture2D _platePastaSauceSalt;
	private Texture2D _platePastaSaucePepper;
	private Texture2D _platePastaSauceSaltPepper;
	private Texture2D _platePastaSauceSaltPepperBasil;
	private bool pastaPlaced = false;
	private bool saucePlaced = false;
	private bool saltPlaced = false;
	private bool pepperPlaced = false;

	public override void _Ready()
	{
		// Load all different collisions and textures
		_pastaDetectionZone = GetNode<Area2D>("PastaZone");
		_sauceDetectionZone = GetNode<Area2D>("SauceZone");
		_basilDetectionZone = GetNode<Area2D>("BasilZone");
		_saltDetectionZone = GetNode<Area2D>("SaltZone");
		_pepperDetectionZone = GetNode<Area2D>("PepperZone");
		_plateSprite = GetNode<Sprite2D>("PlateSprite2D");

		_platePasta = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Pasta/spaghetti-plate-v1.png");
		_platePastaSauce = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Pasta/spaghetti-sauce-v1.png");
		_platePastaSauceSalt = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Pasta/spaghetti-sauce-s-v1.png");
		_platePastaSaucePepper = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Pasta/spaghetti-sauce-p-v1.png");
		_platePastaSauceSaltPepper = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Pasta/spaghetti-sauce-sp-v1.png");
		_platePastaSauceSaltPepperBasil = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Pasta/complete-dish-v1.png");

		// Open signals
		_pastaDetectionZone.BodyEntered += OnBodyEnteredPastaZone;
		_sauceDetectionZone.BodyEntered += OnBodyEnteredSauceZone;
		_saltDetectionZone.BodyEntered += OnBodyEnteredSaltZone;
		_pepperDetectionZone.BodyEntered += OnBodyEnteredPepperZone;
		_basilDetectionZone.BodyEntered += OnBodyEnteredBasilZone;
	}

	// Add Pasta into plate
	private void OnBodyEnteredPastaZone(Node2D body)
	{
		if (body is CookedIngredient ingredient)
		{
			if (ingredient.IngredientKind == CookedIngredient.CookedIngredientKind.Pasta)
			{
				ingredient.QueueFree();
				GD.Print("Pasta here!");
				ChangePlateSprite(_platePasta);
				pastaPlaced = true;
				// Add +1 Score
				GameManager.Instance.AddScore();
			}
		}
	}

	// Add Sauce ontop of pasta
	private void OnBodyEnteredSauceZone(Node2D body)
	{
		if (body is CookedIngredient ingredient)
		{
			if (ingredient.IngredientKind == CookedIngredient.CookedIngredientKind.Sauce && pastaPlaced)
			{
				ingredient.QueueFree();
				GD.Print("Sauce here");
				ChangePlateSprite(_platePastaSauce);
				saucePlaced = true;
				// Add +1 Score
				GameManager.Instance.AddScore();
			}
		}
	}

	// Add salt
	private void OnBodyEnteredSaltZone(Node2D body)
	{
		if (body is SeasoningIngredientScene ingredient)
		{
			if (ingredient.SeasonKind == SeasoningIngredientScene.SeasoningKind.Salt && pepperPlaced && !saltPlaced)
			{
				GD.Print("Salt here");
				ChangePlateSprite(_platePastaSauceSaltPepper);
				saltPlaced = true;
				// Add +1 Score
				GameManager.Instance.AddScore();
			}

			else if (ingredient.SeasonKind == SeasoningIngredientScene.SeasoningKind.Salt && saucePlaced && !pepperPlaced && !saltPlaced)
			{
				GD.Print("Salt here");
				ChangePlateSprite(_platePastaSauceSalt);
				saltPlaced = true;
				// Add +1 Score
				GameManager.Instance.AddScore();
			}
		}
	}

	// Add Pepper
	private void OnBodyEnteredPepperZone(Node2D body)
	{
		if (body is SeasoningIngredientScene ingredient)
		{
			if (ingredient.SeasonKind == SeasoningIngredientScene.SeasoningKind.Pepper && saltPlaced && !pepperPlaced)
			{
				GD.Print("Pepper here");
				ChangePlateSprite(_platePastaSauceSaltPepper);
				pepperPlaced = true;
				// Add +1 Score
				GameManager.Instance.AddScore();
			}

			else if (ingredient.SeasonKind == SeasoningIngredientScene.SeasoningKind.Pepper && saucePlaced && !saltPlaced && !pepperPlaced)
			{
				GD.Print("Pepper here");
				ChangePlateSprite(_platePastaSaucePepper);
				pepperPlaced = true;
				// Add +1 Score
				GameManager.Instance.AddScore();
			}
		}
	}

	// Finish with basil on top of the dish
	private void OnBodyEnteredBasilZone(Node2D body)
	{
		if (body is SeasoningIngredientScene ingredient)
		{
			if (ingredient.SeasonKind == SeasoningIngredientScene.SeasoningKind.Basil && pepperPlaced && saltPlaced)
			{
				ingredient.QueueFree();
				GD.Print("Basil here! All done!");
				ChangePlateSprite(_platePastaSauceSaltPepperBasil);
				// Add +1 Score - Or just only +1 Points here, since this can be done last.
				GameManager.Instance.AddScore();
			}
		}
	}

	// Change plates Sprite2D texture. Used when new stuff is added into Collision zones
	public void ChangePlateSprite(Texture2D newTexture)
	{
		_plateSprite.Texture = newTexture;
	}
}
