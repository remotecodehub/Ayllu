# Solicita os dados do certificado
$subjectName = Read-Host "Digite o nome do certificado (ex: CN=ayllu.local)"
$validYears = Read-Host "Digite a validade em anos (ex: 2)"
$pfxPassword = Read-Host "Digite a senha para proteger o arquivo PFX"
$certName = Read-Host "Digite o nome do arquivo a ser gerado sem extensão de arquivo"
# Define caminhos de saída
$rootFolder = Resolve-Path "$PSScriptRoot\..\"
$certFolder = Join-Path "$rootFolder\certs"
New-Item -ItemType Directory -Path $certFolder -Force | Out-Null
$cerPath = "$certFolder\$certName.cer"
$pfxPath = "$certFolder\$certName.pfx"

# Cria o certificado autoassinado
$cert = New-SelfSignedCertificate `
    -Subject $subjectName `
    -CertStoreLocation "Cert:\LocalMachine\My" `
    -KeyExportPolicy Exportable `
    -KeySpec Signature `
    -KeyLength 2048 `
    -HashAlgorithm sha256 `
    -NotAfter (Get-Date).AddYears([int]$validYears)

# Exporta o certificado público (.cer)
Export-Certificate -Cert $cert -FilePath $cerPath

# Exporta o certificado completo (.pfx)
Export-PfxCertificate -Cert $cert -FilePath $pfxPath -Password (ConvertTo-SecureString -String $pfxPassword -Force -AsPlainText)

# Adiciona o certificado à loja de Autoridades Confiáveis
$store = New-Object System.Security.Cryptography.X509Certificates.X509Store("Root","LocalMachine")
$store.Open("ReadWrite")
$store.Add($cert)
$store.Close()

Write-Host "Certificado gerado e registrado com sucesso!"
Write-Host "Arquivos salvos em: $certFolder"
Write-Host "Use o arquivo PFX em sua aplicação ASP.NET com a senha fornecida."
