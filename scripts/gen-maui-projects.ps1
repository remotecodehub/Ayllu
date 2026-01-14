<#
.SYNOPSIS
Creates a .NET MAUI 10.0 mobile solution structure following Clean Architecture principles.

.DESCRIPTION
This script creates five MAUI projects (Presentation, Application, Composition, Domain, Infrastructure),
adds them to an existing solution, installs required NuGet packages per layer,
and configures project references.

The script outputs colored status messages and exits with success or failure.

.PARAMETER None
All parameters are requested interactively.

.EXAMPLE
PS> .\gen-maui-projects.ps1

.NOTES
Author: Weslley Luiz
Requires: .NET SDK 10.0+, PowerShell 7+
#>

#region Helper Functions

function Write-Step {
    param ($Message)
    Write-Host "[STEP] $Message" -ForegroundColor Cyan
}

function Write-Success {
    param ($Message)
    Write-Host "[SUCCESS] $Message" -ForegroundColor Green
}

function Write-Warning {
    param ($Message)
    Write-Host "[WARNING] $Message" -ForegroundColor Yellow
}

function Write-ErrorMsg {
    param ($Message)
    Write-Host "[ERROR] $Message" -ForegroundColor Red
}

#endregion

try {
    Write-Step "Requesting user inputs"

    $baseName = Read-Host "Enter the application base name (e.g. Ayllu)"
    $solutionPath = Read-Host "Enter the ABSOLUTE path to the existing .sln file"
    $rootPath = Read-Host "Enter the directory where projects should be created"

    if (!(Test-Path $solutionPath)) {
        throw "Solution file not found."
    }

    $layers = @(
        "Presentation",
        "Application",
        "Composition",
        "Domain",
        "Infrastructure"
    )

    Write-Step "Creating project directories"

    New-Item -ItemType Directory -Path "$rootPath" -Force | Out-Null

    Write-Step "Creating MAUI projects"

    dotnet new maui -n "$baseName.Presentation" -o "$rootPath\$baseName.Presentation"
    dotnet new mauilib -n "$baseName.Application" -o "$rootPath\$baseName.Application"
    dotnet new mauilib -n "$baseName.Composition" -o "$rootPath\$baseName.Composition"
    dotnet new mauilib -n "$baseName.Domain" -o "$rootPath\$baseName.Domain"
    dotnet new mauilib -n "$baseName.Infrastructure" -o "$rootPath\$baseName.Infrastructure"

    Write-Step "Adding projects to solution"

    foreach ($layer in $layers) {
        dotnet sln $solutionPath add "$rootPath\$baseName.$layer\$baseName.$layer.csproj"
    }

    Write-Step "Configuring project references"

    dotnet add "$rootPath\$baseName.Presentation" reference `
        "$rootPath\$baseName.Application" `
        "$rootPath\$baseName.Composition"

    dotnet add "$rootPath\$baseName.Application" reference `
        "$rootPath\$baseName.Domain"

    dotnet add "$rootPath\$baseName.Infrastructure" reference `
        "$rootPath\$baseName.Application" `
        "$rootPath\$baseName.Domain"

    dotnet add "$rootPath\$baseName.Composition" reference `
        "$rootPath\$baseName.Application" `
        "$rootPath\$baseName.Infrastructure"
    
    Write-Step "Installing NuGet packages"

    # Presentation
    dotnet add "$rootPath\$baseName.Presentation" package CommunityToolkit.Maui
    dotnet add "$rootPath\$baseName.Presentation" package CommunityToolkit.Maui.Extensions
    dotnet add "$rootPath\$baseName.Presentation" package UraniumUI
    dotnet add "$rootPath\$baseName.Presentation" package UraniumUI.Material
    dotnet add "$rootPath\$baseName.Presentation" package MediatR
    dotnet add "$rootPath\$baseName.Presentation" package FluentValidation

    # Application
    dotnet add "$rootPath\$baseName.Application" package CommunityToolkit.Mvvm
    dotnet add "$rootPath\$baseName.Application" package MediatR -v 12.5.0
    dotnet add "$rootPath\$baseName.Application" package FluentValidation

    # Infrastructure
    dotnet add "$rootPath\$baseName.Infrastructure" package Microsoft.EntityFrameworkCore
    dotnet add "$rootPath\$baseName.Infrastructure" package Microsoft.EntityFrameworkCore.Sqlite
    dotnet add "$rootPath\$baseName.Infrastructure" package Microsoft.EntityFrameworkCore.Tools
    dotnet add "$rootPath\$baseName.Infrastructure" package MediatR

    Write-Success "All projects created and configured successfully."
}
catch {
    Write-ErrorMsg $_
    Write-ErrorMsg "Process failed."
    exit 1
}

Write-Success "Ayllu MAUI solution setup completed."
exit 0