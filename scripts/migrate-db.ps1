param (
    [Parameter(Mandatory = $true)]
    [string]$MigrationName,
    [Parameter(Mandatory = $true)]
    [string]$ModuleRootPath,
    [Parameter(Mandatory = $true)]
    [string]$ModuleName
)

Set-Location $ModuleRootPath
dotnet ef migrations add "$MigrationName" -s "$ModuleRootPath\$ModuleName.Web" -p "$ModuleRootPath\$ModuleName.Web.Infrastructure"
Set-Location $PSScriptRoot\..\
