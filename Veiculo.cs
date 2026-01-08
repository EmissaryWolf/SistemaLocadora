namespace SistemaLocadora
{
    public class Veiculo
    {
        public int Id { get; set; }
        public string Placa { get; set; } = "";
        public string Modelo { get; set; } = "";
        public int KmAtual { get; set; }
        public int KmAlvoOleo { get; set; }
        public int KmAlvoPneu { get; set; }

        public string GetStatusOleo()
        {
            // Corrigido: usando KmAlvoOleo que é o nome da sua propriedade
            int restante = KmAlvoOleo - KmAtual;
            if (restante <= 0) return "VERMELHO";
            if (restante <= 500) return "AMARELO";
            return "VERDE";
        }
    }
}