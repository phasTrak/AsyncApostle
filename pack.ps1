$ErrorActionPreference = "Stop"

$buildProps = [xml](Get-Content .\Directory.Build.props)
$version = @($buildProps.Project.PropertyGroup)[0].Version
Write-Host "-- Creating AsyncApostle $version" -ForegroundColor Green

$solutionDir = (Get-Location).Path
$artifactsDir = Join-Path $solutionDir "packages"
$riderOutDir = Join-Path $solutionDir "Rider\AsyncApostle.Rider"
$riderNupkg = Join-Path $riderOutDir "AsyncApostle.Rider.$version.nupkg"
$riderExtract = Join-Path $artifactsDir "AsyncApostle.Rider.$version"
$riderRoot = Join-Path $artifactsDir "Rider"
$riderPlugin = Join-Path $riderRoot "AsyncApostle.Rider"
$riderLib = Join-Path $riderPlugin "lib"
$riderJar = Join-Path $riderLib "AsyncApostle.Rider-$version.jar"
$riderZip = Join-Path $artifactsDir "AsyncApostle.Rider.zip"

Write-Host "-- Clean: removing bin, obj, and packages directories" -ForegroundColor Green
Get-ChildItem -Include bin, obj, packages -Recurse | Remove-Item -Force -Recurse

Write-Host "-- Restore: restoring packages" -ForegroundColor Green
dotnet restore

Write-Host "-- Build: compiling projects" -ForegroundColor Green
dotnet build -c Release

Write-Host "-- Pack: creating ReSharper extension" -ForegroundColor Green
New-Item -ItemType Directory -Path $artifactsDir -Force | Out-Null
dotnet pack "AsyncApostle/AsyncApostle.csproj" -o $artifactsDir --no-build

Write-Host "-- Pack: creating Rider extension" -ForegroundColor Green
dotnet pack "AsyncApostle/AsyncApostle.Rider.csproj" -o $riderOutDir --no-build

# Repackage the Rider plugin as .zip for distribution
Write-Debug "   Unzip '$riderNupkg' to '$riderExtract'"
Expand-Archive -Path $riderNupkg -DestinationPath $riderExtract -Force

Write-Debug "   Delete '$riderNupkg'"
Remove-Item $riderNupkg -Force

Write-Debug "   Create '$riderRoot'"
New-Item -ItemType Directory -Path $riderRoot -Force | Out-Null

Write-Debug "   Move '$($riderExtract)\lib' to '$riderPlugin'"
Move-Item -Path (Join-Path $riderExtract "lib") -Destination $riderPlugin

Write-Debug "   Rename '$($riderPlugin)\net472' to '$($riderPlugin)\dotnet'"
Move-Item -Path (Join-Path $riderPlugin "net472") -Destination (Join-Path $riderPlugin "dotnet")

Write-Debug "   Delete '$riderExtract'"
Remove-Item $riderExtract -Recurse -Force

Write-Debug "   Create '$riderLib'"
New-Item -ItemType Directory -Path $riderLib -Force | Out-Null

Write-Debug "   Zip '$riderOutDir' into '$riderJar'"
if (Test-Path $riderJar) { Remove-Item $riderJar -Force }
Compress-Archive -Path (Join-Path $riderOutDir '*') -DestinationPath $riderJar -Force

Write-Debug "   Zip '$riderRoot' into '$riderZip'"
if (Test-Path $riderZip) { Remove-Item $riderZip -Force }
Compress-Archive -Path (Join-Path $riderRoot '*') -DestinationPath $riderZip -Force

Write-Debug "   Delete '$riderRoot'"
Remove-Item $riderRoot -Recurse -Force

Write-Host "Packing complete. Packages available at '$artifactsDir'" -ForegroundColor Green
