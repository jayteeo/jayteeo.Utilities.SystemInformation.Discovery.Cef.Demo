using jayteeo.Utilities.SystemInformation.Discovery.Cef.Demo.Core.Abstractions.Services;
using System.Management;

namespace jayteeo.Utilities.SystemInformation.Discovery.Cef.Core.Implementation.Services
{
    public class SystemDiscoveryService : ISystemDiscoveryService
    {
        public Demo.Core.DomainModels.SystemInformation Execute()
        {
            return new Demo.Core.DomainModels.SystemInformation
            {
                SystemName = GetSystemName(),
                MachineManufacturer = GetMachineManufacturer(),
                OperatingSystemNameAndVersion = GetOperatingSystemName()
            };
        }

        private string GetSystemName()
        {
            return System.Environment.MachineName;
        }

        private string GetMachineManufacturer()
        {
            var manufacturer = string.Empty;
            var computerSystemManagementClass = new ManagementClass("Win32_ComputerSystem");
            var computerSystemManagementClassInstances = computerSystemManagementClass.GetInstances();

            foreach (var mo in computerSystemManagementClassInstances)
            {
                manufacturer = mo["Manufacturer"].ToString();
                break;
            }

            return manufacturer;
        }

        private string GetOperatingSystemName()
        {
            var operatingSystemName = string.Empty;
            var operatingSystemManagementClass = new ManagementClass("Win32_OperatingSystem");
            var operatingSystemManagementClassInstances = operatingSystemManagementClass.GetInstances();
            foreach (var mo in operatingSystemManagementClassInstances)
            {
                operatingSystemName = mo["Caption"].ToString();
                break;
            }
            return operatingSystemName;
        }
    }
}
