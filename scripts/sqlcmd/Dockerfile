FROM mcr.microsoft.com/mssql-tools

# Instala dos2unix no container
RUN apt-get update && apt-get install -y dos2unix
WORKDIR /
# Copia arquivos de provisionamento 
COPY ./run-provision.sh ./run-provision.sh

COPY ./tables_sqlServer.sql ./tables_sqlServer.sql
 
# Converte e aplica permissões ao script
RUN dos2unix run-provision.sh && chmod +x run-provision.sh

# Cria um link simbólico para o sqlcmd
RUN ln -s /opt/mssql-tools/bin/sqlcmd /usr/bin/sqlcmd

ENTRYPOINT ["/run-provision.sh"]
