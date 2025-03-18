// Replacing Terraform script with a Bicep file for Azure resources
// This is a Bicep file equivalent to the Terraform script

targetScope = 'subscription'

@description('Name of the resource group')
param resourceGroupName string

@description('Location of the resource group')
param location string

resource resourceGroup 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: resourceGroupName
  location: location
}
