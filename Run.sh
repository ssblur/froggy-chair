#!/bin/bash
# move to the script directory
cd "$(dirname "$0")"
# build the mod
dotnet build || exit
# copy the mod to the mods folder
mv "$HOME/.local/share/Tiny Life/Mods" "$HOME/.local/share/Tiny Life/ModsBackup"
cp -r ./bin/Debug/net6.0/* "$HOME/.local/share/Tiny Life/Mods"
# run the game
dir=$(<"$HOME/.local/share/Tiny Life/GameDir")
cd "$dir"
"./Tiny Life" -v --skip-splash --skip-preloads
rm -Rf "$HOME/.local/share/Tiny Life/Mods"
mv "$HOME/.local/share/Tiny Life/ModsBackup" "$HOME/.local/share/Tiny Life/Mods"