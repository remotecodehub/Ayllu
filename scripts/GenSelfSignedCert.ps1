<#
.SYNOPSIS
    Gera um certificado autoassinado, exporta para PFX e adiciona ao repositório Root.

.DESCRIPTION
    - Recebe parâmetros obrigatórios (DnsName, SAN, Senha, Caminho).
    - Valida se está rodando como administrador (solicita elevação se possível).
    - Converte caminho relativo para absoluto.
    - Cria certificado com New-SelfSignedCertificate.
    - Exporta para PFX.
    - Adiciona ao repositório Root.
    - Exibe informações do certificado.

.PARAMETER DnsName
    Nome DNS ou IP para o CN do certificado.

.PARAMETER SAN
    Lista de Subject Alternative Names (DNS ou IP).

.PARAMETER Password
    Senha para exportar o PFX.

.PARAMETER OutputPath
    Caminho para salvar o arquivo PFX (relativo ou absoluto).
#>

param(
    [Parameter(Mandatory = $true)]
    [string]$DnsName,

    [Parameter(Mandatory = $true)]
    [string[]]$SAN,
 
    [Parameter(Mandatory = $true)]
    [string]$OutputPath,

    [Parameter(Mandatory = $true)]
    [int]$Years
)

function Test-Admin {
    $currentUser = [Security.Principal.WindowsIdentity]::GetCurrent()
    $principal = New-Object Security.Principal.WindowsPrincipal($currentUser)
    return $principal.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)
}

# Solicitar elevação se não estiver como admin
if (-not (Test-Admin)) {
    Write-Error "Este script precisa ser executado como Administrador."
    Start-Process powershell.exe "-File `"$PSCommandPath`" $($MyInvocation.UnboundArguments)" -Verb RunAs
    exit
}

# Converter caminho relativo para absoluto
if (-not [System.IO.Path]::IsPathRooted($OutputPath)) {
    $OutputPath = Join-Path -Path (Get-Location) -ChildPath $OutputPath
}
$OutputPath = [System.IO.Path]::GetFullPath($OutputPath)

$Password = Read-Host "Digite a senha do PFX" -AsSecureString
# Criar certificado
Write-Host "Gerando certificado autoassinado para CN=$DnsName e SAN=$($SAN -join ', ')..."
$cert = New-SelfSignedCertificate `
    -DnsName $DnsName,$SAN `
    -CertStoreLocation "cert:\LocalMachine\My" `
    -KeyExportPolicy Exportable `
    -FriendlyName "API Local Cert" `
    -NotAfter (Get-Date).AddYears($Years)

# Exportar para PFX
Export-PfxCertificate -Cert $cert -FilePath $OutputPath -Password $Password

# Adicionar ao repositório Root
Write-Host "Adicionando certificado ao repositório Root..."
$store = New-Object System.Security.Cryptography.X509Certificates.X509Store("Root","LocalMachine")
$store.Open("ReadWrite")
$store.Add($cert)
$store.Close()

# Exibir informações
Write-Host "✅ Certificado gerado e instalado com sucesso!"
Write-Host "Subject: $($cert.Subject)"
Write-Host "Thumbprint: $($cert.Thumbprint)"
Write-Host "FriendlyName: $($cert.FriendlyName)"
Write-Host "Validade: $($cert.NotBefore) até $($cert.NotAfter)"
Write-Host "Arquivo PFX: $OutputPath"
Write-Host "Senha: (oculta por segurança)"