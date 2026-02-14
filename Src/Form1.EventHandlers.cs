using System;
using System.ServiceProcess;
using System.Windows.Forms;

namespace TaskManager
{
    public partial class Form1
    {
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckedListBox checkedListBox = (CheckedListBox)sender;
            int selectedIndex = checkedListBox.SelectedIndex;
            TrySetSelectedServiceByIndex(selectedIndex);
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            try
            {
                txtServiceName.Text = "";
                listarServicos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao listar serviços: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            try
            {
                listarServicos();
                limparServicoSelecionado();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao listar serviços: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnParar_Click(object sender, EventArgs e)
        {
            try
            {
                string serviceName = _selectedServiceName;
                if (string.IsNullOrEmpty(serviceName))
                {
                    MessageBox.Show("Por favor, insira o nome do serviço.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ServiceController service = new ServiceController(serviceName);

                if (service.Status == ServiceControllerStatus.Running)
                {
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped);
                    MessageBox.Show($"Serviço '{serviceName}' parado com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"O serviço '{serviceName}' não está em execução.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                listarServicos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao parar o serviço: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            try
            {
                string serviceName = _selectedServiceName;
                if (string.IsNullOrEmpty(serviceName))
                {
                    MessageBox.Show("Por favor, insira o nome do serviço.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ServiceController service = new ServiceController(serviceName);
                if (service.Status == ServiceControllerStatus.Stopped)
                {
                    service.Start();
                    service.WaitForStatus(ServiceControllerStatus.Running);
                    MessageBox.Show($"Serviço '{serviceName}' iniciado com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"O serviço '{serviceName}' já está em execução.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                listarServicos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao iniciar o serviço: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            try
            {
                string serviceName = _selectedServiceName;
                if (string.IsNullOrEmpty(serviceName))
                {
                    MessageBox.Show("Por favor, insira o nome do serviço.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ServiceController service = new ServiceController(serviceName);
                if (service.Status == ServiceControllerStatus.Running)
                {
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped);
                }

                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running);
                MessageBox.Show($"Serviço '{serviceName}' reiniciado com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao reiniciar o serviço: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
