
cls
echo "deploying..."

nuget pack .\Implementation\Demote\Demote.csproj -Verbosity detailed -Properties Configuration=Release -Prop Platform=x64
nuget pack .\Implementation\Remote\Remote.csproj -Verbosity detailed -Properties Configuration=Release -Prop Platform=x64

move .\moniüs.*.nupkg C:\NuGet\G4 -Force

echo "deploy completed,"

ls C:\NuGet\G4\moniüs.*.nupkg