using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notas
{
    public partial class Form1 : Form
    {
        //Mover programa
        
        private bool mouse_is_down = false;
        private System.Drawing.Point mouse_pos;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_pos.X = e.X;
            mouse_pos.Y = e.Y;
            mouse_is_down = true;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouse_is_down)
            {
                System.Drawing.Point current_pos = Control.MousePosition;
                current_pos.X = current_pos.X - mouse_pos.X;
                current_pos.Y = current_pos.Y - mouse_pos.Y;
                this.Location = current_pos;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouse_is_down = false;
        }

        //Caminho do arquivo
        String path = "";

        //Instância de variáveis
        String nome = "";
        String formato = "";

        //Formato de arquivo
        private string RetornarMensagemDeFormato(string tipo)
        {
            switch (tipo)
            {
                case "ino":
                    return "Arduino";
                    break;
                case "cs":
                    return "C#";
                    break;
                case "cpp":
                    return "C++";
                    break;
                case "c":
                    return "C";
                    break;
                case "btwo":
                    return "Braintwo";
                    break;
                case "json":
                    return "Json";
                    break;
                case "xml":
                    return "Xml";
                    break;
                case "html":
                    return "HTML";
                    break;
                case "css":
                    return "CSS";
                    break;
                case "js":
                    return "JavaScript";
                    break;
                default:
                    return "Texto";
                    break;

            }
        }

        public Form1()
        {
            InitializeComponent();
        }
        //Fechar programa

        private void sair() 
        {

            if (!String.IsNullOrWhiteSpace(richTextBox1.Text))
            {
                exitPrompt();

                if (DialogResult == DialogResult.Yes)
                {
                    salvarComo();
                    System.Windows.Forms.Application.Exit();
                }
                else if (DialogResult == DialogResult.No)
                {
                    System.Windows.Forms.Application.Exit();
                }
            }
            else
            {
                System.Windows.Forms.Application.Exit();
            }
        }

        private void sairCtrlQToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sair();
        }
        
        //Perguntar ao fechar
        private void exitPrompt()
        {
            DialogResult = MessageBox.Show("Você gostaria de salvar o arquivo atual?",
                "Bloco de Notas",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
        }
        //Teclas de Atalho
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Q))
            {
                sair();
                return true;
            }
            if (keyData == (Keys.Control | Keys.O))
            {
                abrir();
                return true;
            }
            if (keyData == (Keys.Control | Keys.S))
            {
                salvar();
                return true;
            }
            if (keyData == (Keys.Control | Keys.C))
            {
                richTextBox1.Copy();
            }
            if (keyData == (Keys.Control | Keys.X))
            {
                richTextBox1.Cut();
            }
            if (keyData == (Keys.Control | Keys.V))
            {
                //richTextBox1.Paste();
            }
            if (keyData == (Keys.Control | Keys.A))
            {
                richTextBox1.SelectAll();
            }
            if (keyData == (Keys.Control | Keys.F))
            {
                fonte();
            }
            if (keyData == (Keys.Delete))
            {
                richTextBox1.SelectedText = String.Empty;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        //Abrir arquivo
        private void abrir()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Text = File.ReadAllText(path = openFileDialog1.FileName);
                string[] SplitExtension = openFileDialog1.FileName.Split('.');
                string[] nomedividido = openFileDialog1.FileName.Split('\\');
                nome = nomedividido[nomedividido.Length - 1];
                formato = RetornarMensagemDeFormato(SplitExtension[SplitExtension.Length - 1]);
                this.Text = "Bloco de Notas - " + formato + " - " + nome;
                titulo.Text = "Bloco de Notas - " + formato + " - " + nome;
            }
        }
        private void abrirCtrlOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            abrir();
        }
        //Salvar arquivo

        private void salvar() 
        {
            if (!String.IsNullOrWhiteSpace(path))
            {
                File.WriteAllText(path, richTextBox1.Text);
            }
            else
            {
                salvarComo();
            }
        }

        private void salvarComo()
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                saveFileDialog1.Filter = "Arquivo de texto (*.txt)|*.txt|Todos os arquivos (*.*)|*.*";
                File.WriteAllText(path = saveFileDialog1.FileName, richTextBox1.Text);
            }
        }

        private void salvarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            salvarComo();
        }

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            salvar();
        }
        //Criar novo documento

        private void criarNovo() 
        {
            if (!String.IsNullOrWhiteSpace(richTextBox1.Text))
            {
                exitPrompt();

                if (DialogResult == DialogResult.Yes)
                {
                    salvarComo();
                    richTextBox1.Text = String.Empty;
                    path = String.Empty; ;
                }
                else if (DialogResult == DialogResult.No)
                {
                    richTextBox1.Text = String.Empty; ;
                    path = String.Empty; ;
                }
            }
        }
        private void novoCtrlNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            criarNovo(); 
        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form aboutForm = new Form2();
            aboutForm.ShowDialog();
        }

        private void colarCtrlVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void recortarCtrlXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }
        private void copiarCtrlCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void selecionarTudoCtrlAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void apagarDelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = String.Empty;
        }
        //Mudar Fonte
        private void fonte()
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Font = richTextBox1.Font = new Font(fontDialog1.Font, fontDialog1.Font.Style);
                richTextBox1.ForeColor = fontDialog1.Color;
            }
        }

        private void fonteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fonte();
        }

        //Mudar tema

        private void padrãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.ForeColor = Color.Black;
            richTextBox1.BackColor = Color.White;
            this.BackColor = Color.White;
            menuStrip1.BackColor = Color.White;
            menuStrip1.ForeColor = Color.Black;
            titulo.ForeColor = Color.Black;
        }

        private void pretoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.ForeColor = Color.White;
            richTextBox1.BackColor = Color.Black;
            this.BackColor = Color.Black;
            menuStrip1.BackColor = Color.Black;
            menuStrip1.ForeColor = Color.White;
            titulo.ForeColor = Color.White;
        }

        private void cinzaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.ForeColor = Color.Black;
            richTextBox1.BackColor = Color.Gray;
            this.BackColor = Color.Gray;
            menuStrip1.BackColor = Color.Gray;
            titulo.ForeColor = Color.Black;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            sair();
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}