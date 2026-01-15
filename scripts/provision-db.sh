#!/bin/bash
set -e
set +H
echo "🔍 APP_DB_HOST=$APP_DB_HOST"
echo "🔍 APP_DB_NAME=$APP_DB_NAME"
echo "🔍 APP_DB_USER=$APP_DB_USER"
echo "🔍 APP_DB_PASSWORD=$(if [ -z "$APP_DB_PASSWORD" ]; then echo '***'; else echo ''; fi)"

 
echo "🔄 Aguardando SQL '${APP_DB_HOST}' aceitar conexões..."
for i in {1..30}; do
  /opt/mssql-tools/bin/sqlcmd -S "${APP_DB_HOST}" -U sa -P "${SA_PASSWORD}" -Q "SELECT 1" >/dev/null 2>&1 && break
  echo "Tentativa $i: SQL ainda não respondeu..."
  sleep 2
done

echo "🛠️ Provisionando banco de dados '${APP_DB_NAME}'..."

/opt/mssql-tools/bin/sqlcmd -S "${APP_DB_HOST}" -U sa -P "${SA_PASSWORD}" -Q "
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'${APP_DB_NAME}')
BEGIN
    CREATE DATABASE [${APP_DB_NAME}];
    PRINT '✔️ Banco criado.';
END
ELSE
BEGIN
    PRINT 'ℹ️ Banco já existe.';
END
"

echo "⏳ Aguardando banco '${APP_DB_NAME}' estar acessível..."
for i in {1..30}; do
  /opt/mssql-tools/bin/sqlcmd -S "${APP_DB_HOST}" -U sa -P "${SA_PASSWORD}" -d "${APP_DB_NAME}" -Q "SELECT 1" && break
  sleep 2
done

echo "👤 Provisionando login '${APP_DB_USER}'..."

/opt/mssql-tools/bin/sqlcmd -S "${APP_DB_HOST}" -U sa -P "${SA_PASSWORD}" -Q "
IF NOT EXISTS (SELECT name FROM sys.server_principals WHERE name = N'${APP_DB_USER}')
BEGIN
    CREATE LOGIN [${APP_DB_USER}]
    WITH PASSWORD = N'${APP_DB_PASSWORD}', CHECK_POLICY = OFF;
    PRINT '✔️ Login criado.';
END
ELSE
BEGIN
    PRINT 'ℹ️ Login já existe.';
END
"

echo "📌 Provisionando usuário no banco '${APP_DB_NAME}'..."

/opt/mssql-tools/bin/sqlcmd -S "${APP_DB_HOST}" -U sa -P "${SA_PASSWORD}" -d "${APP_DB_NAME}" -Q "
IF NOT EXISTS (SELECT name FROM sys.database_principals WHERE name = N'${APP_DB_USER}')
BEGIN
    CREATE USER [${APP_DB_USER}] FOR LOGIN [${APP_DB_USER}];
    PRINT '✔️ Usuário criado.';
END
ELSE
BEGIN
    PRINT 'ℹ️ Usuário já existe.';
END
"

echo "🔐 Concedendo db_owner se necessário..."

/opt/mssql-tools/bin/sqlcmd -S "${APP_DB_HOST}" -U sa -P "${SA_PASSWORD}" -d "${APP_DB_NAME}" -Q "
IF NOT EXISTS (
    SELECT dp.name
    FROM sys.database_principals dp
    JOIN sys.database_role_members drm ON dp.principal_id = drm.member_principal_id
    JOIN sys.database_principals roles ON drm.role_principal_id = roles.principal_id
    WHERE dp.name = N'${APP_DB_USER}' AND roles.name = 'db_owner'
)
BEGIN
    EXEC sp_addrolemember N'db_owner', N'${APP_DB_USER}';
    PRINT '✔️ Permissão db_owner atribuída.';
END
ELSE
BEGIN
    PRINT 'ℹ️ Usuário já possui db_owner.';
END
"

echo "✅ Provisionamento concluído com sucesso!"
sleep 90