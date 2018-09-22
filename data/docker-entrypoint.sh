#!/bin/sh
set -e
echo "The Dockerfile ENTRYPOINT is starting. . ."
sleep 1s
/opt/mssql-tools/bin/sqlcmd -S "localhost" -U "sa" -P "Passw0rd!" -i "migration.sql" \
& /opt/mssql/bin/sqlservr 
exec "$@"