using ApiBloodyTree.Model;

namespace ApiBloodyTree.Business
{
    public class MembroBusiness
    {
        public Dictionary<string, double> CalcularTiposSanguineos(string tipoSanguineoPai, string tipoSanguineoMae)
        {
            Dictionary<string, List<string>> combinacoes = new()
            {
                { "A", new List<string> { "A", "O" } },
                { "B", new List<string> { "B", "O" } },
                { "AB", new List<string> { "A", "B" } },
                { "O", new List<string> { "O" } }
            };

            Dictionary<string, double> probabilidades = new();

            if (!combinacoes.ContainsKey(tipoSanguineoPai) || !combinacoes.ContainsKey(tipoSanguineoMae))
            {
                throw new ArgumentException("Tipo sanguíneo do pai ou da mãe é inválido.");
            }

            List<string> tiposPai = combinacoes[tipoSanguineoPai];
            List<string> tiposMae = combinacoes[tipoSanguineoMae];
            int totalCombinacoes = tiposPai.Count * tiposMae.Count;

            foreach (var tipoPai in tiposPai)
            {
                foreach (var tipoMae in tiposMae)
                {
                    string tipoFilho = CombinarAlelos(tipoPai, tipoMae);

                    if (!probabilidades.ContainsKey(tipoFilho))
                    {
                        probabilidades[tipoFilho] = 0;
                    }

                    probabilidades[tipoFilho] += 1.0 / totalCombinacoes;
                }
            }

            return probabilidades;
        }

        private static string CombinarAlelos(string tipoSanguineoPai, string tipoSanguineoMae)
        {
            Dictionary<(string, string), string> combinacoes = new()
            {
            { ("A", "A"), "A" },
            { ("A", "B"), "AB" },
            { ("A", "O"), "A" },
            { ("B", "A"), "AB" },
            { ("B", "B"), "B" },
            { ("B", "O"), "B" },
            { ("O", "A"), "A" },
            { ("O", "B"), "B" },
            { ("O", "O"), "O" },
        };

            if (combinacoes.TryGetValue((tipoSanguineoPai, tipoSanguineoMae), out var tipoSanguineo))
            {
                return tipoSanguineo;
            }

            return "Desconhecido";
        }

        public Membro CalcularProbabilidades(string tipoSanguineoPai, string tipoSanguineoMae)
        {
            Dictionary<string, double> probabilidades = CalcularTiposSanguineos(tipoSanguineoPai, tipoSanguineoMae);

            Membro membro = new()
            {
                GrupoA = probabilidades.GetValueOrDefault("A", 0.0),
                GrupoB = probabilidades.GetValueOrDefault("B", 0.0),
                GrupoO = probabilidades.GetValueOrDefault("O", 0.0),
                GrupoAB = probabilidades.GetValueOrDefault("AB", 0.0)
            };

            return membro;
        }

        public (bool,bool) CalcularFatorRh(bool fatorRhPai, bool fatorRhMae)
        {
            if (!IsValidBoolean(fatorRhPai) || !IsValidBoolean(fatorRhMae))
            {
                throw new ArgumentException("Os parâmetros devem ser valores booleanos (true ou false).");
            }

            bool FatorRh = (fatorRhPai) || (fatorRhMae);
            bool Portador = (fatorRhPai && !fatorRhMae) || (!fatorRhPai && fatorRhMae) || (!fatorRhPai && !fatorRhMae);

            return (FatorRh,Portador);
        }

        private static bool IsValidBoolean(bool value)
        {
            return value == true || value == false;
        }
    }
}