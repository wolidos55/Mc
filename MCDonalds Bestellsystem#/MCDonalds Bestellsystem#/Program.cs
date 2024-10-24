using System;
using System.Collections.Generic;
using System.IO;

namespace McDonaldsBestellsystem
{
	class BestellSystem
	{
		static void Main(string[] args)
		{
			// Menü-Liste erstellen
			Dictionary<string, double> liste = new Dictionary<string, double>()
			{
				{"Burger", 5.99},
				{"Cheeseburger", 6.99},
				{"Cola", 1.99}
			};

			// Initialisierung
			List<string> wahlListe = new List<string>();
			double gesamt = 0;

			Console.WriteLine("Willkommen im McDonalds Bestellsystem!");
			bool weiter = true;

			while (weiter)
			{
				// Auswahl zeigen
				Console.WriteLine("Was möchten Sie bestellen?");
				foreach (var item in liste)
				{
					Console.WriteLine($"{item.Key} kostet {item.Value} Euro");
				}

				string auswahl = Console.ReadLine();

				// Prüfung der Auswahl
				if (liste.ContainsKey(auswahl))
				{
					wahlListe.Add(auswahl);
					gesamt += liste[auswahl]; // Dynamisch den Preis abrufen
					Console.WriteLine($"{auswahl} hinzugefügt.");
				}
				else
				{
					Console.WriteLine("Ungültige Auswahl.");
				}

				// Nochmal bestellen
				Console.WriteLine("Möchten Sie weitermachen? (j/n)");
				string fortsetzen = Console.ReadLine();
				if (fortsetzen.ToLower() != "j")
				{
					weiter = false;
				}
			}

			// Rabatt anwenden
			gesamt = AnwendenRabatt(gesamt);

			// Bestellung speichern
			string dateipfad = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Bestellung.txt");
			File.WriteAllText(dateipfad, $"Ihre Bestellung: {string.Join(", ", wahlListe)}\nGesamtpreis: {gesamt} Euro");

			Console.WriteLine($"Bestellung gespeichert. Gesamt: {gesamt} Euro.");
		}

		static double AnwendenRabatt(double gesamt)
		{
			Console.WriteLine("Haben Sie einen Rabattcode? (j/n)");
			string rabatt = Console.ReadLine();
			if (rabatt.ToLower() == "j")
			{
				Console.WriteLine("Bitte Rabattcode eingeben:");
				string code = Console.ReadLine();
				if (code == "Rabatt10")
				{
					gesamt -= gesamt * 0.1;
					Console.WriteLine("10% Rabatt angewendet.");
				}
				else
				{
					Console.WriteLine("Ungültiger Code.");
				}
			}
			return gesamt;
		}
	}
}