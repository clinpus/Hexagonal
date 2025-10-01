﻿
using System.ComponentModel.DataAnnotations;


namespace Application
{
    public class ClientDto
    {
        // Pour l'UPDATE et le GET (peut être null ou 0 pour le CREATE)
        public int Id { get; set; }

        // Obligatoire pour la création/mise à jour
        [Required(ErrorMessage = "Le nom du client est obligatoire.")]
        public string Nom { get; set; }

        public string Adresse { get; set; }

        public string Email { get; set; }

        public string NumeroSiret { get; set; }

        public string Ville { get; set; }

        // Optionnel : Ajout d'une propriété pour le lien de l'objet de Domaine
        public int NombreDeFactures { get; set; }
    }
}
