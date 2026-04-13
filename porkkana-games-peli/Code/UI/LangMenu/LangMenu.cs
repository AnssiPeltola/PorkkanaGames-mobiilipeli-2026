using Godot;
using System;

public partial class LangMenu : Control
{
	public override void _Ready()
	{
		GetNode<TextureButton>("LangButtons/LangFi").Pressed += OnLangFiPressed;
		GetNode<TextureButton>("LangButtons/LangEng").Pressed += OnLangEngPressed;
		GetNode<TextureButton>("LangButtons/LangSwe").Pressed += OnLangSwePressed;
	}

	private void OnLangEngPressed()
	{
		TranslationServer.SetLocale("en");
		GD.Print("Current Locale: " + TranslationServer.GetLocale());
	}

	private void OnLangFiPressed()
	{
		TranslationServer.SetLocale("fi");
		GD.Print("Current Locale: " + TranslationServer.GetLocale());
	}

	private void OnLangSwePressed()
	{
		TranslationServer.SetLocale("swe");
		GD.Print("Current Locale: " + TranslationServer.GetLocale());
	}
}
