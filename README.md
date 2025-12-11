# Play.Common

Play.Common é uma biblioteca de código compartilhado nuget, escrita em dotnet core 8.0, que fornece suporte as operações de microserviços da aplicação Play. 

## Ambiente de Desenvolvimento

| TOOL                                                      | DESCRIPTION                                     |
| :-------------------------------------------------------- | :---------------------------------------------- |
| [Ubuntu 24.04.3 LTS](https://ubuntu.com/download/desktop) | Sistema operacional desktop                     |
| [Visual Studio Code](https://aka.ms/vscode)               | IDE para desenvolvimento                        |
| [.Net 8.0 SDK](https://dotnet.microsoft.com/download)     | Kit para desenvolvimento do software            |
| [Docker](https://docs.docker.com/get-started/)            | Serviço provedor de container de infraestrutura |

[Back](#playcommon)

## Adicionando Documentação do Pacote

Para adicionar documentação da biblioteca é necessário incluir no projeto metadados com as informações e instruções.como por exemplo, assim:

```xml
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
    <PackageId>Play.Common</PackageId>
    <Version>1.0.0</Version>
    <EnablePackageValidation>true</EnablePackageValidation>
    <PackageTags>play, microservice, common, shared, library</PackageTags>
    <PackageReadmeFile>README.md</PackageReadmeFile>
    <PackRelease>true</PackRelease>
    <Title>Play.Common.Domain, desenvolvida por Bigai Software.</Title>
    <Authors>Marcos Cruz</Authors>
    <Company>Bigai Software, Santo André, SP, Brazil.</Company>
    <Description>Play.Common é uma biblioteca de código compartilhado nuget, escrita em dotnet core 8.0, que fornece suporte as operações de microserviços da aplicação Play.</Description>
    <RepositoryType>git</RepositoryType>
    <RepositoryUrl>https://github.com/marcos-cruz/Play.Common.git</RepositoryUrl>
    <CurrentYear>$([System.DateTime]::Now.ToString(yyyy))</CurrentYear>
    <Copyright>Copyright ©2021-$(CurrentYear) Bigai Software, All rights reserved.</Copyright>
    <NoWarn>$(NoWarn);1591</NoWarn>
  </PropertyGroup>

  <ItemGroup>
    <None Update="README.md">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
      <Pack>true</Pack>
      <PackagePath>\</PackagePath>
    </None>
  </ItemGroup>
```


## Gerando Pacote Nuget

Abrir o terminal na pasta do projeto e executar o comando: `dotnet pack -o /home/marcos-cruz/Projects/Play/Packages`

```powershell
dotnet pack -o /home/marcos-cruz/Projects/Play/Packages
MSBuild version 17.8.43+f0cbb1397 for .NET
  Determining projects to restore...
  All projects are up-to-date for restore.
  Play.Common -> /home/marcos-cruz/Projects/Play/Play.Common/src/Play.Common/bin/Release/net8.0/Play.Common.dll
  Successfully created package '/home/marcos-cruz/Projects/Play/Packages/Play.Common.1.0.0.nupkg'.
```

## Definindo a Localização do Pacote

Para que um pacote nuget gerado possa ser usado, é ncessário definir a localização do pacote. Podemos fazer isto executando o seguinte comando: `dotnet nuget add source /home/marcos-cruz/Projects/Play/Packages -n PlayEconomy`

```powershell
dotnet nuget add source /home/marcos-cruz/Projects/Play/Packages -n PlayEconomy
Package source with Name: PlayEconomy added successfully.
```

## Listando Origens de Pacotes Configurados

Para listar as origens de pacotes configuradas, basta executar no terminal o seguinte comando: `dotnet nuget list source`

```powershell
dotnet nuget list source
Registered Sources:
  1.  nuget.org [Enabled]
      https://api.nuget.org/v3/index.json
  2.  PlayEconomy [Enabled]
      /home/marcos-cruz/Projects/Play/Packages
```