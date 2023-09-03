using System.ComponentModel.DataAnnotations;

namespace ApiBloodyTree.Model
{
    public class Membro
    {
        [Key]
        public int IdMembro { get; set; }

        public int? IdPai { get; set; }

        public int? IdMae { get; set; }

        public int? IdIrmao { get; set; }

        public int? IdConjuge { get; set; }

        public string? Nome { get; set; }

        public string? Sobrenome { get; set; }

        public double? GrupoA { get; set; }

        public double? GrupoB { get; set; }

        public double? GrupoO { get; set; }

        public double? GrupoAB { get; set; }

        public bool? FatorRh { get; set; }

        public bool? PortadorNegativo { get; set; }
    }
}