#!/usr/bin/bash

if [ -z "$1" ]; then
	echo "Migration name is requried"
	exit -1
fi

NAME="$1"

echo "Migration name $NAME"
echo $(cd . && pwd)

dotnet ef  migrations add $NAME -o Migrations/App -c K.Wms.Data.Context.WmsAppContext -p K.Wms.Data --startup-project K.Wms.Server

# dotnet ef database update -c K.Wms.Data.Context.WmsAppContext -p K.Wms.Data --startup-project K.Wms.Server
