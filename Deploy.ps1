param(
    [Parameter(Mandatory = $true)][string]$SolutionDir,
    [Parameter(Mandatory = $true)][string]$Namespace,
    [Parameter(Mandatory = $false)][string]$Source = "http://localhost:81/nuget/GPS/"
)	

$ProjectFile = $SolutionDir + "\\" + $Namespace + "\\" + $Namespace + ".csproj";
$xml = [xml](Get-Content -Path $ProjectFile);
$VersionNumber = $xml.Project.PropertyGroup.Version
#$releaseNotes = [IO.File]::ReadAllText($SolutionDir + "\ReleaseNotes.txt")
$destination = $SolutionDir + "\" + $Namespace + "\bin\Debug\" + [System.String]::Format("{0}.{1}.nupkg", $Namespace, $VersionNumber);

dotnet pack $ProjectFile --include-source

if ($?) {
    Write-Host $Source
    
    if($Source.Contains("localhost"))
    {
        dotnet nuget push $destination -k $env:LocalHostNugetAPIKey -s $Source
    }
    else 
    {
        dotnet nuget push $destination -k $env:NugetAPIKey -s $Source
    }
}
