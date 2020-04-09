using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SistemaCadastro
{
    public partial class Form1 : Form
    {
        struct tdado
        {
            public string nome, cpf;
            //public int idade;
        };
        int qtd = 0;
        Queue<tdado> fila = new Queue<tdado>();
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void mostrar()
        {
            txtFila.Clear();
            foreach(tdado x in fila)
                txtFila.AppendText(x.nome + "-" + x.cpf + "\r\n");        
        }
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            tdado x;
            x.nome = txtNome.Text;
            x.cpf = txtCPF.Text;
            fila.Enqueue(x);
            mostrar();
            // limpando os campos
            txtNome.Clear();
            txtCPF.Clear();
            txtNome.Focus();
        }
        void salvar()
        {
            BinaryWriter arq = new BinaryWriter(File.Open("fila.txt", FileMode.Create));
            arq.Write(fila.Count());
            foreach (tdado b in fila)
            {
                arq.Write(b.nome);
                arq.Write(b.cpf);
            }
            arq.Close();
            MessageBox.Show("Dados Salvos com Sucesso", "Mensagem");
        }
        void carregar()
        {

            BinaryReader arq = new BinaryReader(
                File.Open("fila.txt", FileMode.Open));
            if (File.Exists("fila.txt"))
            {
                qtd = arq.ReadInt32();
                for (int i = 0; i < qtd; i++)
                {
                    tdado b;
                    b.nome = arq.ReadString();
                    b.cpf = arq.ReadString();
                    fila.Enqueue(b);                   
                }// fim for
                arq.Close();
                mostrar();
            }// fim if 
            else
                MessageBox.Show("Arquivo não encontrado", "Erro");
        }

        private void btnCarregar_Click(object sender, EventArgs e)
        {
            carregar();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            salvar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            int i = 1;
            foreach (tdado x in fila)
            {
                if(x.cpf.Equals(txtBuscar.Text))
                {
                    // achou fala a posicao i
                    //"x.nome esta na posicao i"
                    break;
                }
                i++;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            tdado r;
            if (fila.Count > 0)
            {
                r = fila.Dequeue();
                MessageBox.Show("Removido:" + r.nome);
                lblAtendimento.Text = "Proximo:\n" + r.nome;
            }
            mostrar();
        }

        private void btnAtender_Click(object sender, EventArgs e)
        {
           
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
