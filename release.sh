#!/bin/bash
# release.sh
VERSION=$1  # optional: pass version tag

dotnet publish -c Release -r linux-x64   --self-contained -o ./builds/linux
dotnet publish -c Release -r win-x64     --self-contained -o ./builds/windows
dotnet publish -c Release -r osx-x64     --self-contained -o ./builds/mac-intel
dotnet publish -c Release -r osx-arm64   --self-contained -o ./builds/mac-arm

butler push ./builds/linux      iamllcoolray/pong:linux
butler push ./builds/windows    iamllcoolray/pong:windows
butler push ./builds/mac-intel  iamllcoolray/pong:mac-intel
butler push ./builds/mac-arm    iamllcoolray/pong:mac-arm

echo "Done! Version: ${VERSION:-untagged}"