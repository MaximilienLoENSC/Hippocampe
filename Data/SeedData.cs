public static class SeedData
{
    // Test data for part 1 and 2
    public static void Init()
    {
        using var context = new FilmContext();

        Genre drame = new() { Nom = "Drame" };
        context.Genres.AddRange(drame);

        Genre romance = new() { Nom = "Romance" };
        context.Genres.AddRange(romance);

        Pays pays = new() { Nom = "Nouvelle Zélande" };

        Realisateur realisateur = new() { Nom = "Jane Campion" };
        context.Realisateurs.AddRange(realisateur);

        Acteur acteur = new() { Nom = "Holly Hunter" };
        context.Acteurs.AddRange(acteur);

        Compositeur compositeur = new() { Nom = "Mickael Nyman" };
        context.Compositeurs.AddRange(compositeur);

        Film film = new()
        {
            Titre = "La Leçon de Piano",
            DateDeSortie = new DateTime(1995, 5, 15),
            Genres = { drame, romance },
            Pays = { pays },
            Realisateurs = { realisateur },
            Acteurs = { acteur },
            Compositeurs = { compositeur },
            Commentaire = "Pas mal le film",
        };

        context.Films.Add(film);

        context.SaveChanges();
    }
}
