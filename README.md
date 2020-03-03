# Integration with Azure Dev-Ops

The purpose of this application is to present the capabilities of Azure DevOps connected with ARM templates.

## Azure pipelines

The main benefit of this approach is that the continuous integration process is living together with the source code. A developer is able to change the system, for example, by adding a new library with tests and update the CI process in the same Pull Request. Imagine that no more conjunction of spheres between developer and operations.

The file `azure-pipelines.yml` contains a simple definition of a simple build and test pipeline, which could be easily moved and adapted to another project. Below example pipeline:

```yaml
trigger:
- master

pool:
  vmImage: 'ubuntu-latest'

variables:
  buildConfiguration: 'Release'

steps:

- task: DotNetCoreCLI@2
  displayName: 'Build application'
  inputs:
    command: 'build'
    arguments: '--configuration $(buildConfiguration)'
    workingDirectory: 'src/'

- task: DotNetCoreCLI@2
  displayName: 'Unit tests'
  inputs:
    command: 'test'
    arguments: '--configuration $(buildConfiguration)'
    workingDirectory: 'src/'


- task: DotNetCoreCLI@2
  displayName: 'Publishing project'
  inputs:
    command: 'publish'
    projects: 'src/AiDO.WebApp/AiDO.WebApp.csproj'
    publishWebProjects: True
    arguments: '--configuration $(buildConfiguration) --output $(Build.ArtifactStagingDirectory)'
    zipAfterPublish: True


- task: PublishBuildArtifacts@1
  displayName: 'Publishing artifacts'
```

---

## ARM Template

`Azure Resource Manager templates` a commonly used to describe infrastructure in the Microsoft Azure ecosystem. The ARM template is used to describe the `infrastructure as code`. That enables us to create in a repeated and straightforward way our environments: no more connection sting and JSON configuration hell.

The listing below show main comopnents of the configuration file. The detailed configuration could be found in the `/src/AiDO.WebApp/azuredeploy.json` file. `/src/AiDO.WebApp/azuredeploy.parameters.json` contains parameters which could be used to introduce differences between environments like: dev,qa,staging,production etc.

```json
{
   "$schema":"https://schema.management.azure.com/schemas/2019-04-01/deploymentTemplate.json#",
   "contentVersion":"1.0.0.0",
   "parameters":{
       (...)
      }
   },
   "variables":{
      (...)
   },
   "resources":[
       {
         "type": "Microsoft.Storage/storageAccounts",
         (...)         
       },
       {
         "type":"Microsoft.Web/serverfarms",
         (...)
      },
      {
         "type":"Microsoft.Web/sites",
         (...)
      },
      {
         "type": "microsoft.insights/components",
         (...)
      }
   ],
   "outputs":{},
   "functions":[]
}
```
---

## Useful links

[ARM Templates](https://docs.microsoft.com/en-us/azure/azure-resource-manager/templates/overview)

[App Service](https://docs.microsoft.com/en-us/azure/app-service/)

[Azure DevOps](https://azure.microsoft.com/en-us/services/devops/)

[Application Insights](https://docs.microsoft.com/en-us/azure/azure-monitor/app/app-insights-overview)

[Azure Blob Storage](https://docs.microsoft.com/en-us/azure/storage/blobs/storage-blobs-introduction)