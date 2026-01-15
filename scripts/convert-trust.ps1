param (
    [Parameter(Mandatory = $true)]
    [string]$PfxDirectory
)

# Verifica se o diretório existe
if (-not (Test-Path $PfxDirectory)) {
    Write-Error "O diretório '$PfxDirectory' não existe."
    exit 1
}

# Lista todos os arquivos .pfx no diretório
$pfxFiles = Get-ChildItem -Path $PfxDirectory -Filter *.pfx

if ($pfxFiles.Count -eq 0) {
    Write-Host "Nenhum arquivo .pfx encontrado em '$PfxDirectory'."
    exit 0
}

foreach ($pfxFile in $pfxFiles) {
    $pfxPath = $pfxFile.FullName
    $crtPath = Join-Path $PfxDirectory ($pfxFile.BaseName + ".crt")

    Write-Host "Digite a senha para '$($pfxFile.Name)':"
    $securePassword = Read-Host -AsSecureString
    $plainPassword = [System.Runtime.InteropServices.Marshal]::PtrToStringAuto(
        [System.Runtime.InteropServices.Marshal]::SecureStringToBSTR($securePassword)
    )

    Write-Host "Convertendo '$($pfxFile.Name)' para '$($crtPath)'..."

    # Executa o comando OpenSSL com a senha fornecida
    $opensslCommand = "openssl pkcs12 -in `"$pfxPath`" -clcerts -nokeys -out `"$crtPath`" -passin pass:$plainPassword"
    $result = & cmd /c $opensslCommand

    if (-not (Test-Path $crtPath)) {
        Write-Error "Falha ao gerar '$crtPath'."
        continue
    }

    Write-Host "Certificado gerado: $crtPath"

    # Importa o certificado na store raiz confiável do Windows
    try {
        $cert = New-Object System.Security.Cryptography.X509Certificates.X509Certificate2($crtPath)
        $store = New-Object System.Security.Cryptography.X509Certificates.X509Store("Root", "LocalMachine")
        $store.Open("ReadWrite")
        $store.Add($cert)
        $store.Close()

        Write-Host "Certificado '$($pfxFile.BaseName)' registrado na store raiz confiável do Windows."
    } catch {
        Write-Error "Erro ao registrar '$($crtPath)' na store: $_"
    }
}
