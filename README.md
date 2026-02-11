# F1ManagementStudio


# install process 

## Software needed

1.VS Code (Purple).
1.Visual studio (Blue).
1.Microsoft SQL Server Management studio.

# Bug
1.Facing Issue during SSMS install.

1.PowerShell Command:

# Powershell command 1
New-ItemProperty -Path "HKLM:\SYSTEM\CurrentControlSet\Services\stornvme\Parameters\Device" -Name "ForcedPhysicalSectorSizeInBytes" -PropertyType MultiString -Force -Value "* 4095"

# Powershell command 2
Get-ItemProperty -Path "HKLM:\SYSTEM\CurrentControlSet\Services\stornvme\Parameters\Device" -Name "ForcedPhysicalSectorSizeInBytes"


1.Restart Your machine it will be work.