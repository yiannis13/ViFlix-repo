#!/bin/sh
set -e
echo "The Dockerfile ENTRYPOINT started. . ."
/opt/mssql-tools/bin/sqlcmd -S "localhost" -U "sa" -P "Passw0rd!" -i "migration.sql"
echo "The Dockerfile ENTRYPOINT is executing. . ."
exec "$@"