#!/usr/bin/env pwsh
Param (
	[Parameter(Mandatory=$false)]
	[string]$Framework = 'net10.0',
	[Parameter(Mandatory)]
	[string]$version
)

$profiles = 'framework-dependent-win-x64', 'framework-dependent-linux-x64'

Function Publish-Proj
{
	Param (
		[Parameter(Mandatory)]
		[string]$DirName
	)

	try
	{
		Push-Location "./src/$DirName"
		$publishDir = './bin/Publish'

		foreach ($publishProfile in $profiles) {
			Write-Host "Publishing $publishProfile"
			if ($publishProfile -eq 'framework-dependent') {
				Write-Host "Publishing $DirName framework dependent"
				dotnet publish -o "$publishDir/$publishProfile/" --no-self-contained -c 'Release' -f $Framework -p:PublishSingleFile=false
			}
			elseif ($publishProfile -like 'framework-dependent-*') {
				Write-Host "Publishing $DirName framework dependent specific"
				dotnet publish -o "$publishDir/$publishProfile/" --no-self-contained -c 'Release' -f $Framework -r $publishProfile.Substring('framework-dependent-'.Length) -p:PublishSingleFile=false
			}
			else {
				Write-Host "Publishing $DirName self contained partially trimmed"
				dotnet publish -o "$publishDir/$publishProfile/" --sc -c 'Release' -f $Framework -r $publishProfile -p:PublishTrimmed=true -p:TrimMode=partial
			}
		}

		Write-Host "Published $($profiles.Count) profile(s)"
	}
	finally
	{
		Pop-Location
	}
}

Publish-Proj 'MCeToJava.Cli'

$publishDir = 'Publish'

if (Test-Path -LiteralPath $publishDir) {
	Write-Host Cleaning
	Remove-Item -Path $publishDir -Recurse -Force > $null
}

Write-Host 'Copying files'

New-Item -Path . -Name $publishDir -ItemType "directory" -Force > $null

foreach ($publishProfile in $profiles) {
	New-Item -Path $publishDir -Name $publishProfile -ItemType "directory" -Force
	$outDir = "./$publishDir/$publishProfile"

	Copy-Item -Path "./src/MCeToJava.Cli/bin/Publish/$publishProfile/*" -Destination $outDir -Recurse
}

Write-Host 'Compressing folders'

foreach ($publishProfile in $profiles) {
	$fromDir = "./$publishDir/$publishProfile/*"
	$toFile = "./$publishDir/MCeToJava_$($publishProfile)_$version.zip"
	Write-Host "Comressing $fromDir to $toFile"
	
	Compress-Archive -Path $fromDir -CompressionLevel 'Optimal' -DestinationPath $toFile -Force
}

Write-Host 'Done'