using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace SistemaLocadora
{
    public partial class Form1 : Form
    {
        // DECLARAÇÃO DOS COMPONENTES (Isso faz os erros sumirem)
        private TextBox txtPlaca;
        private TextBox txtModelo;
        private TextBox txtKm;
        private Button btnSalvar;
        private DataGridView gridVeiculos;

        public Form1()
        {
            InitializeComponent();
            CriarInterfaceManualmente(); // Cria os campos na tela
            AtualizarGrade();
        }

        private void CriarInterfaceManualmente()
        {
            this.Text = "DriveControl - Demo";
            this.Size = new Size(800, 600);

            txtPlaca = new TextBox { Location = new Point(20, 20), PlaceholderText = "Placa" };
            txtModelo = new TextBox { Location = new Point(20, 50), PlaceholderText = "Modelo" };
            txtKm = new TextBox { Location = new Point(20, 80), PlaceholderText = "KM Atual" };
            
            btnSalvar = new Button { 
                Text = "Salvar Veículo", 
                Location = new Point(20, 110), 
                Size = new Size(100, 30) 
            };
            btnSalvar.Click += btnSalvar_Click;

            gridVeiculos = new DataGridView { 
                Location = new Point(20, 160), 
                Size = new Size(740, 380),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill 
            };

            this.Controls.Add(txtPlaca);
            this.Controls.Add(txtModelo);
            this.Controls.Add(txtKm);
            this.Controls.Add(btnSalvar);
            this.Controls.Add(gridVeiculos);
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try 
            {
                using (var db = new AppDbContext())
                {
                    var novo = new Veiculo
                    {
                        Placa = txtPlaca.Text.ToUpper(),
                        Modelo = txtModelo.Text,
                        KmAtual = int.Parse(txtKm.Text),
                        KmAlvoOleo = int.Parse(txtKm.Text) + 10000,
                        KmAlvoPneu = int.Parse(txtKm.Text) + 40000
                    };

                    db.Veiculos.Add(novo);
                    db.SaveChanges();
                    
                    AtualizarGrade();
                    MessageBox.Show("Veículo salvo com sucesso!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: Verifique se o KM é um número válido.");
            }
        }

        private void AtualizarGrade()
        {
            using (var db = new AppDbContext())
            {
                var lista = db.Veiculos.ToList();
                gridVeiculos.DataSource = lista;

                foreach (DataGridViewRow row in gridVeiculos.Rows)
                {
                    if (row.DataBoundItem is Veiculo v)
                    {
                        int restante = v.KmAlvoOleo - v.KmAtual;
                        if (restante <= 0) row.DefaultCellStyle.BackColor = Color.Red;
                        else if (restante <= 500) row.DefaultCellStyle.BackColor = Color.Yellow;
                        else row.DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                }
            }
        }
    }
}