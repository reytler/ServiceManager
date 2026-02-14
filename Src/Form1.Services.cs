using System;
using System.Linq;
using System.ServiceProcess;

namespace TaskManager
{
    public partial class Form1
    {
        private ServiceController[] _allServices;
        private ServiceController[] _displayedServices;
        private string _selectedServiceName = "";

        private void limparServicoSelecionado()
        {
            _selectedServiceName = "";
        }

        private void listarServicos()
        {
            _allServices = ServiceController.GetServices()
                .OrderBy(service => service.ServiceName)
                .ToArray();

            _displayedServices = GetDisplayedServices(_allServices, txtServiceName.Text);

            lstChekedBoxServices.Items.Clear();
            foreach (ServiceController service in _displayedServices)
            {
                lstChekedBoxServices.Items.Add(FormatServiceListItem(service));
            }
        }

        private static ServiceController[] GetDisplayedServices(ServiceController[] allServices, string filterText)
        {
            if (allServices == null)
            {
                return Array.Empty<ServiceController>();
            }

            string filter = (filterText ?? "").Trim();
            if (string.IsNullOrEmpty(filter))
            {
                return allServices;
            }

            return allServices
                .Where(service => service.ServiceName.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToArray();
        }

        private static string FormatServiceListItem(ServiceController service)
        {
            return $"{service.ServiceName} - {service.DisplayName} - {service.Status}";
        }

        private bool TrySetSelectedServiceByIndex(int selectedIndex)
        {
            if (selectedIndex < 0)
            {
                return false;
            }

            if (_displayedServices == null)
            {
                return false;
            }

            if (selectedIndex >= _displayedServices.Length)
            {
                return false;
            }

            _selectedServiceName = _displayedServices[selectedIndex].ServiceName;
            return true;
        }
    }
}
