#!/bin/bash
# release.sh
VERSION=$1  # optional: pass version tag

dotnet publish -c Release -r linux-x64   --self-contained -p:PublishSingleFile=true -o ./builds/linux
dotnet publish -c Release -r win-x64     --self-contained -p:PublishSingleFile=true -o ./builds/windows
dotnet publish -c Release -r osx-x64     --self-contained -p:PublishSingleFile=true -o ./builds/mac-intel
dotnet publish -c Release -r osx-arm64   --self-contained -p:PublishSingleFile=true -o ./builds/mac-arm

butler push ./builds/linux      iamllcoolray/pong:linux --userversion "${VERSION:-untagged}"
butler push ./builds/windows    iamllcoolray/pong:windows --userversion "${VERSION:-untagged}"
butler push ./builds/mac-intel  iamllcoolray/pong:mac-intel --userversion "${VERSION:-untagged}"
butler push ./builds/mac-arm    iamllcoolray/pong:mac-arm --userversion "${VERSION:-untagged}"

echo "Done! Version: ${VERSION:-untagged}"