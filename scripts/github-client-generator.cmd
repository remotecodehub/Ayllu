@echo off
SETLOCAL ENABLEDELAYEDEXPANSION

REM Caminho da solução
SET ROOT_DIR=%~dp0\..
SET CLIENT_OUTPUT=%ROOT_DIR%\src\Ayllu.Github.Client
SET SPEC_URL=https://raw.githubusercontent.com/github/rest-api-description/refs/heads/main/descriptions/api.github.com/api.github.com.json

REM Cria diretórios caso não existam
IF NOT EXIST "%CLIENT_OUTPUT%" mkdir "%CLIENT_OUTPUT%"

echo Gerando cliente C# do GitHub API...
  
docker run --rm -v %CLIENT_OUTPUT%\Github\Client:/app/output mcr.microsoft.com/openapi/kiota generate --language csharp --output /app/output --openapi %SPEC_URL% --class-name AylluGithubClient --namespace-name Ayllu.Github.Client

echo Cliente gerado em %CLIENT_OUTPUT%
