<#
.SYNOPSIS
Generates a C# client SDK using Kiota from an OpenAPI specification.

.DESCRIPTION
This script automates the process of generating a strongly typed C# client SDK
from an OpenAPI JSON/YAML document using the Kiota CLI tool. It validates the
environment, ensures that .NET SDK and Kiota are installed, and installs required
NuGet dependencies. If the target project path does not exist, the script can
create either a standard Class Library or a .NET MAUI Class Library project.

.PARAMETER OpenApiUrl
The URL pointing to the OpenAPI JSON/YAML document.

.PARAMETER ClientPath
The path to the target project directory where the SDK should be generated.
If the directory does not exist, the script will prompt to create a new project.

.EXAMPLE
PS> .\Generate-KiotaClient.ps1 -OpenApiUrl "https://localhost:5001/swagger/v1.json" -ClientPath "./src/mobile/ayllu.mobile.client"

This example generates the SDK from the OpenAPI document served by the WebAPI
and places the generated code inside the specified project path.

.NOTES
Author: Copilot
Requires: PowerShell 5.1 or later, .NET SDK, Kiota CLI
#>

param(
    [Parameter(Mandatory = $true)]
    [string]$OpenApiUrl,

    [Parameter(Mandatory = $true)]
    [string]$ClientPath
)

function Write-Info($msg) { Write-Host $msg -ForegroundColor Cyan }
function Write-Success($msg) { Write-Host $msg -ForegroundColor Green }
function Write-WarningMsg($msg) { Write-Host $msg -ForegroundColor Yellow }
function Write-ErrorMsg($msg) { Write-Host $msg -ForegroundColor Red }

Write-Info "=== Environment validation ==="

# Check if dotnet SDK is installed
if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) {
    Write-ErrorMsg "The .NET SDK is not installed. Please install it before running this script."
    exit 1
}
Write-Success ".NET SDK found."

# Check if Kiota CLI is installed
$kiotaInstalled = dotnet tool list -g | Select-String "Microsoft.Kiota.Cli"
if (-not $kiotaInstalled) {
    Write-WarningMsg "Kiota CLI not found. Installing..."
    dotnet tool install --global Microsoft.Kiota.Cli
    Write-Success "Kiota CLI installed successfully."
} else {
    Write-Success "Kiota CLI is already installed."
}

Write-Info "=== Validating OpenAPI URL ==="
try {
    $response = Invoke-WebRequest -Uri $OpenApiUrl -UseBasicParsing
    if ($response.StatusCode -ne 200) {
        Write-ErrorMsg "Unable to access the provided OpenAPI URL."
        exit 1
    }
    $json = $response.Content | ConvertFrom-Json -ErrorAction SilentlyContinue
    if (-not $json.openapi) {
        Write-ErrorMsg "The document does not appear to be a valid OpenAPI specification."
        exit 1
    }
    Write-Success "Valid OpenAPI document detected."
} catch {
    Write-ErrorMsg "Error while validating the OpenAPI URL."
    exit 1
}

Write-Info "=== Preparing client project ==="
if (-not (Test-Path $ClientPath)) {
    Write-WarningMsg "The path $ClientPath does not exist."
    $choice = Read-Host "Do you want to create a Class Library (1) or a MAUI Class Library (2)?"
    if ($choice -eq "1") {
        dotnet new classlib -o $ClientPath
        Write-Success "Class Library project created at $ClientPath"
    } elseif ($choice -eq "2") {
        dotnet new maui-lib -o $ClientPath
        Write-Success "MAUI Class Library project created at $ClientPath"
    } else {
        Write-ErrorMsg "Invalid option selected."
        exit 1
    }
} else {
    Write-Success "Project already exists at $ClientPath"
}

Write-Info "=== Generating SDK with Kiota ==="
kiota generate `
    --openapi $OpenApiUrl `
    --language csharp `
    --output $ClientPath `
    --namespace-name Ayllu.Mobile.Client

Write-Success "SDK successfully generated at $ClientPath"

Write-Info "=== Installing required dependencies ==="
dotnet add $ClientPath package Microsoft.Kiota.Abstractions
dotnet add $ClientPath package Microsoft.Kiota.Http.HttpClientLibrary
dotnet add $ClientPath package Microsoft.Kiota.Serialization.Json
dotnet add $ClientPath package Microsoft.Kiota.Serialization.Text
dotnet add $ClientPath package Microsoft.Kiota.Serialization.Form
dotnet add $ClientPath package Microsoft.Kiota.Serialization.Multipart

Write-Success "Dependencies installed successfully."
Write-Info "=== Process completed ==="